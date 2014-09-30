using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using TAPI;
using PoroCYon.MCT.Net;
using Avalon.API.Items;
using Avalon.API.Items.MysticalTomes;
using Avalon.UI;

namespace Avalon
{
    /// <summary>
    /// Global player stuff
    /// </summary>
    public sealed class MPlayer : ModPlayer
    {
        static float rot = 0f;
        static bool
            isImmuneToSlimes = false,
            LavaMerman = false,
            goblinTB = false,
            allTB = false,
            runonce = true;
        static int
            hook = 0,
            jungleChestId,
            hellfireChestId,
            skillCD = 0;
        readonly static int[] MUSIC_BOXES = new int[] { -1, 0, 1, 2, 4, 5, -1, 6, 7, 9, 8, 11, 10, 12 };

        Item[] accessories
        {
            get
            {
                return MWorld.accessories[player.whoAmI];
            }
            set
            {
                MWorld.accessories[player.whoAmI] = value;
            }
        }

        /// <summary>
        /// Creates a new instance of the MPlayer class
        /// </summary>
        /// <remarks>Called by the mod loader</remarks>
        public MPlayer()
            : base()
        {

        }

        /// <summary>
        /// Called when the Player spawns for the first time in the world
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            jungleChestId = TileDef.type["Avalon:Jungle Chest"];
            hellfireChestId = TileDef.type["Avalon:Hellfire Chest"];
        }

        /// <summary>
        /// Called when the Player is updated
        /// </summary>
        public override void MidUpdate()
        {
            base.MidUpdate();

            U_SetChainTexture();
            // U_GetAchievements();
            U_SpawnBosses();
            U_ExtraAccs();
            U_LavaMerman();
			//U_Chests();
            //U_TileInteract();
            U_MysticalTomes();
        }
		#region MidUpdate submethods
		void U_SetChainTexture()
        {
            Item sel = player.inventory[player.selectedItem];
            if (player.itemAnimation < 0 && !player.delayUseItem) // not using it
                return;

            object attr = null;

            for (int i = 0; i < sel.modEntities.Count; i++)
            {
                object[] attrArr = sel.modEntities[i].GetType().GetCustomAttributes(typeof(ChainTextureAttribute), true);

                if (attrArr.Length > 0)
                    attr = attrArr[0]; // there should be only one
            }

            if (attr == null)
            {
                // reset
                if (Main.chainTexture != modBase.textures["Resources/Chains/Grapple Chain.png"])
                    Main.chainTexture  = modBase.textures["Resources/Chains/Grapple Chain.png"];

                if (Main.chain3Texture != modBase.textures["Resources/Chains/Blue Moon Chain.png"])
                    Main.chain3Texture  = modBase.textures["Resources/Chains/Blue Moon Chain.png"];
            }
            else
            {
                ChainTextureAttribute cta = (ChainTextureAttribute)attr;
                ModBase @base = (Mods.mods.FirstOrDefault(m => m.InternalName == cta.ModInternalName) ?? Mod.Instance.mod).modBase; // LINQ++

                string path = cta.TextureName;

                if (!path.StartsWith("/"))
                    path = "Resources/Chains/" + cta.TextureName;
                else
                    path = path.Substring(1); // remove the leading '/'

                if (!@base.textures.ContainsKey(path))
                    return; // hmmm....

                Texture2D tex = @base.textures[path];

                if (cta.ReplaceFlailChain)
                    Main.chain3Texture = tex;
                else
                    Main.chainTexture = tex;
            }
        }
        void U_GetAchievements()
        {
            // to future me/reader: method call is uncommented (in OnUpdate) for the sake of performance.

            if (player.whoAmI == Main.myPlayer)
            {
                bool get = true;

                foreach (Item i in player.armor)
                    get &= i.type > 0 && i.stack > 0;
                foreach (Item i in accessories)
                    get &= i.type > 0 && i.stack > 0;

                if (get)
                {
                    // Mod.Achieve(1, "AVALON_AS+_OVERKILL", null, player.whoAmI);
                }
            }
        }
        void U_SpawnBosses()
        {
            if (NPC.spawnRate <= 0 || !Mod.IsInSuperHardmode || MWorld.CheckForBosses() || player.townNPCs > 0 || Main.invasionType != 0)
                return;

            int rate = (NPC.spawnRate * 2) * (Main.eclipse ? 3 : (Main.bloodMoon ? 2 : 1));

            for (int i = 0; i < Mod.spawns.Count; i++)
                if (Mod.spawns[i].ShouldSpawn(rate, player))
                    Mod.spawns[i].Spawn(player.whoAmI);
        }
        void U_ExtraAccs()
        {
            if (!player.active || String.IsNullOrEmpty(player.name) || player.dead || player.ghost)
                return;

            for (int i = 0; i < Mod.ExtraSlots; i++)
            {
                Item acc = MWorld.accessories[player.whoAmI][i];

                if (acc == null || acc.IsBlank())
                    continue;

                player.statDefense += acc.defense  ;
                player.lifeRegen   += acc.lifeRegen;

                if (acc.prefix != null && acc.prefix.id > 0 /*&& !acc.prefix.Equals(Prefix.None)*/ && acc.prefix.id < Defs.prefixes.Count)
                    acc.prefix.ApplyToPlayer(player);

                acc.Effects(player);

                switch (acc.type)
                {
                    #region vanilla accs
                    case 238: //Wizard Hat
                        player.magicDamage += .15f;
                        break;
                    case 111: //Band of Starpower
                        player.statManaMax2 += 20;
                        break;
                    case 268: //Diving Helmet
                        player.accDivingHelm = true;
                        break;
                    case 15: //Copper Watch
                        if (player.accWatch < 1)
                            player.accWatch = 1;
                        break;
                    case 16: //Silver Watch
                        if (player.accWatch < 2)
                            player.accWatch = 2;
                        break;
                    case 17: //Gold Watch
                        if (player.accWatch < 3)
                            player.accWatch = 3;
                        break;
                    case 18: //Depth Meter
                        if (player.accDepthMeter < 1)
                            player.accDepthMeter = 1;
                        break;
                    case 53: //Cloud in a Bottle
                        player.doubleJump = true;
                        break;
                    case 54: //Hermes Boots
                        //if (player.baseMaxSpeed < 6f)
                        //    player.baseMaxSpeed = 6f;
                        break;
                    case 128: //Rocket Boots
                        if (player.rocketBoots == 0)
                            player.rocketBoots = 1;
                        break;
                    case 156: //Cobalt Shield
                        player.noKnockback = true;
                        break;
                    case 158: //Lucky Horseshoe
                        player.noFallDmg = true;
                        break;
                    case 159: //Shiny Red Balloon
                        player.jumpBoost = true;
                        break;
                    case 187: //Flipper
                        player.accFlipper = true;
                        break;
                    case 211: //Feral Claws
                        player.meleeSpeed += .12f;
                        break;
                    case 223: //Nature's Gift
                        player.manaCost -= .06f;
                        break;
                    case 285: //Aglet
                        player.moveSpeed += .05f;
                        break;
                    case 212: //Anklet of the Wind
                        player.moveSpeed += .1f;
                        break;
                    case 267: //Guide Voodoo Doll
                        player.killGuide = true;
                        break;
                    case 193: //Obsidian Skull
                        player.fireWalk = true;
                        break;
                    case 485: //Moon Charm
                        player.wolfAcc = true;
                        break;
                    case 486: //Ruler
                        player.rulerAcc = true;
                        break;
                    case 393: //Compass
                        player.accCompass = 1;
                        break;
                    case 394: //Diving Gear
                        player.accFlipper = player.accDivingHelm = true;
                        break;
                    case 395: //GPS
                        player.accWatch = 3;
                        player.accDepthMeter = player.accCompass = 1;
                        break;
                    case 396: //Obsidian Horseshoe
                        player.noFallDmg = player.fireWalk = true;
                        break;
                    case 397: //Obsidian Shield
                        player.noKnockback = player.fireWalk = true;
                        break;
                    case 399: //Cloud in a Balloon
                        player.jumpBoost = player.doubleJump = true;
                        break;
                    case 405: //Spectre Boots
                        //if (player.baseMaxSpeed < 6f)
                        //    player.baseMaxSpeed = 6f;
                        if (player.rocketBoots == 0)
                            player.rocketBoots = 2;
                        break;
                    case 407: //Toolbelt
                        if (player.blockRange < 1)
                            player.blockRange = 1;
                        break;
                    case 489: //Sorcerer Emblem
                        player.magicDamage += .15f;
                        break;
                    case 490: //Warrior Emblem
                        player.meleeDamage += .15f;
                        break;
                    case 491: //Ranger Emblem
                        player.rangedDamage += .15f;
                        break;
                    case 492: //Demon Wings
                        if (player.wings == 0)
                            player.wings = 1;
                        break;
                    case 493: //Angel Wings
                        if (player.wings == 0)
                            player.wings = 2;
                        break;
                    case 497: //Neptune's Shell
                        player.accMerman = true;
                        break;
                    case 535: //Philosopher's Stone
                        player.pStone = true;
                        break;
                    case 536: //Titan Glove
                        player.kbGlove = true;
                        break;
                    case 532: //Star Cloak
                        player.starCloak = true;
                        break;
                    case 554: //Cross Necklace
                        player.longInvince = true;
                        break;
                    case 555: //Mana Flower
                        player.manaFlower = true;
                        player.manaCost -= .08f;
                        break;

                        // 1.2 accs?
                    #endregion

                    default:
                        if (Main.myPlayer == player.whoAmI)
                        {
                            if (acc.type != 576 || acc.type > 603)
                                break;

                            if (acc.type == 576 && Main.rand.Next(18000) == 0 &&
                                !WavebankDef.current.StartsWith("Vanilla:") ||
                                (WavebankDef.current.StartsWith("Vanilla:") && String.IsNullOrEmpty(WavebankDef.current)))
                            {
                                int id = String.IsNullOrEmpty(WavebankDef.current) ? 0 : Convert.ToInt32(WavebankDef.current.Substring("Vanilla:".Length));

                                int mid = id <= MUSIC_BOXES.Length - 1 ? MUSIC_BOXES[id] : -1;
                                if (mid <= -1)
                                    break;

                                acc.SetDefaults(562 + mid, false);
                                BinBuffer bb = new BinBuffer(new BinBufferByte());
                                bb.Write((byte)player.whoAmI);
								acc.Save(bb);

                                bb.Pos = 0;
                                NetHelper.SendModData(modBase, NetMessages.SetMusicBox, toSend: bb.ReadBytes());
                            }
                            if (acc.type >= 562 && acc.type <= 574)
                                WavebankDef.musicBox = "Vanilla:" + (acc.type - 562);
                        }
                        break;
                }
            }
        }
        void U_LavaMerman()
        {
            if (LavaMerman)
                player.fireWalk = player.lavaImmune = player.gills = player.accFlipper = player.merman = true;

            Main.armorHeadTexture[39] = LavaMerman
                ? modBase.textures["Resources/Misc/LavaMermanHead.png"] : modBase.textures["Resources/Misc/MermanHead.png"];
            Main.armorArmTexture [22] = LavaMerman
                ? modBase.textures["Resources/Misc/LavaMermanArm.png" ] : modBase.textures["Resources/Misc/MermanArm.png" ];
            Main.armorBodyTexture[22] = LavaMerman
                ? modBase.textures["Resources/Misc/LavaMermanBody.png"] : modBase.textures["Resources/Misc/MermanBody.png"];
            Main.armorLegTexture [21] = LavaMerman
                ? modBase.textures["Resources/Misc/LavaMermanLegs.png"] : modBase.textures["Resources/Misc/MermanLegs.png"];
        }
        void U_Chests()
		{
			// to future me/reader: method call is uncommented (in MidUpdate) for the sake of performance.

			//if (player.whoAmI == Main.myPlayer && player.chest >= 0)
			//{
			//    Chest c = Main.chest[player.chest];

			//    if (Main.tile[c.x, c.y].type == 21)
			//        switch (Main.tile[c.x, c.y].frameX)
			//        {
			//            case 2 * 18:
			//                Main.chestText = "Gold Chest";
			//                break;
			//            case 6 * 18:
			//                Main.chestText = "Shadow Chest";
			//                break;
			//        }
			//    else if (Main.tile[c.x, c.y].type == jungleChestId)
			//        Main.chestText = "Jungle Chest";
			//    else if (Main.tile[c.x, c.y].type == hellfireChestId)
			//        Main.chestText = "Hellfire Chest";
			//}
		}
		void U_TileInteract()
        {
			// to future me/reader: method call is uncommented (in MidUpdate) for the sake of performance.

			TileHurtPlayer(player, TileDef.type["Avalon:Magmatic Ore"], 20, CheckHurtMagma, " got burned...");
            TileHurtPlayer(player, TileDef.type["Avalon:Dark Matter"], 30, null, " got burned...");
            TileHurtPlayer(player, TileDef.type["Avalon:Black Sand"], 20, null, " got stuck in sand...", -1);

            if (TouchesTile(player, 0, new[] { (int)TileDef.type["Avalon:Ice Block"] }))
            {
                // p.baseSlideFactor = 0.1f;
            }
        }
        void U_MysticalTomes()
        {
            if (MWorld.localTome.IsBlank() || MWorld.localManager == null)
                return;

            MWorld.localTome.Effects(player);

            if (--skillCD <= 0)
                skillCD = 0;
			
            if (KState.Down(Mod.TomeSkillHotkey) && skillCD <= 0)
            {
                MWorld.localManager.Activate(player);

                NetHelper.SendModData(modBase, NetMessages.ActivateSkill, Main.myPlayer);

                skillCD = MWorld.localManager.Cooldown;
            }
        }
        #endregion

        /// <summary>
        /// Saves data to the player file.
        /// </summary>
        /// <param name="bb">The buffer to put data in.</param>
        public override void Save(BinBuffer bb)
        {
            base.Save(bb);

            bb.Write(Main.gameMenu);

            if (!Main.gameMenu)
            {
                for (int i = 0; i < accessories.Length; i++)
                    bb.Write(accessories[i]);

                bb.Write(MWorld.localTome);
            }
        }
        /// <summary>
        /// Loads data from the player file.
        /// </summary>
        /// <param name="bb">The buffer to load data from.</param>
        public override void Load(BinBuffer bb)
        {
            base.Load(bb);

            if (!bb.ReadBool())
            {
                for (int i = 0; i < accessories.Length; i++)
                    accessories[i] = bb.ReadItem();

                MWorld.localTome = bb.ReadItem();
            }
        }

#pragma warning disable 1591
		public override void DamageNPC(NPC npc, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult)
		{
			base.DamageNPC(npc, hitDir, ref damage, ref knockback, ref crit, ref critMult);

			for (int i = 0; i < accessories.Length; i++)
				accessories[i].DamageNPC(player, npc, hitDir, ref damage, ref knockback, ref crit, ref critMult);
		}
		public override void DealtNPC(NPC npc, int hitDir, int dmgDealt, float knockback, bool crit)
		{
			base.DealtNPC(npc, hitDir, dmgDealt, knockback, crit);

			for (int i = 0; i < accessories.Length; i++)
				accessories[i].DealtNPC(player, npc, hitDir, dmgDealt, knockback, crit);
		}
        public override void DamagePlayer(NPC npc, int hitDir, ref int damage, ref bool crit, ref float critMult)
        {
            base.DamagePlayer(npc, hitDir, ref damage, ref crit, ref critMult);

            for (int i = 0; i < accessories.Length; i++)
                accessories[i].DamagePlayer(npc, player, hitDir, ref damage, ref crit, ref critMult);
        }
        public override void DealtPlayer(NPC npc, int hitDir, int dmgDealt, bool crit)
        {
            base.DealtPlayer(npc, hitDir, dmgDealt, crit);

            for (int i = 0; i < accessories.Length; i++)
                accessories[i].DealtPlayer(npc, player, hitDir, dmgDealt, crit);
        }
        public override void DamagePVP(Player p, int hitDir, ref int damage, ref bool crit, ref float critMult)
        {
            base.DamagePVP(p, hitDir, ref damage, ref crit, ref critMult);

            for (int i = 0; i < accessories.Length; i++)
                accessories[i].DamagePVP(player, p, hitDir, ref damage, ref crit, ref critMult);
        }
        public override void DealtPVP(Player p, int hitDir, int dmgDealt, bool crit)
        {
            base.DealtPVP(p, hitDir, dmgDealt, crit);

            for (int i = 0; i < accessories.Length; i++)
                accessories[i].DealtPVP(player, p, hitDir, dmgDealt, crit);
        }
#pragma warning restore 1591

        bool CheckHurtTile()
        {
            return player.armor.Count(i =>
                i.type == ItemDef.byName["Avalon:Tome of Luck"      ].type ||
                i.type == ItemDef.byName["Avalon:Kinetic Boots Gold"].type) > 0;
        }
        bool CheckHurtMagma()
        {
            return CheckHurtTile() && !player.lavaImmune;
        }

        /// <summary>
        /// Gets wether a player touches a tile or not.
        /// </summary>
        /// <param name="p">The player who touches or doesn't touch the tile.</param>
        /// <param name="r">The radius to check. 0 is touching, -1 is only in the tile.</param>
        /// <param name="types">The tile types to check.</param>
        /// <returns>true when the player touches it, false otherwise.</returns>
        public static bool TouchesTile(Player p, int r, IEnumerable<int> types)
        {
            int
                xMin = (int)((p.position.X - 2f)      / 16f) - r,
                xMax = (int)((p.position.X + p.width) / 16f) + r,
                yMin = (int)((p.position.Y - 2f)      / 16f) - r,
                yMax = (int)((p.position.Y + p.width) / 16f) + r;

            xMin = Math.Max(xMin, 0);
            xMax = Math.Min(xMax, Main.maxTilesX);
            yMin = Math.Max(yMin, 0);
            yMax = Math.Min(yMax, Main.maxTilesY);

            for (int x = xMin; x <= xMax; x++)
                for (int y = yMin; y <= yMax; y++)
                    if (Main.tile[x, y] != null && Main.tile[x, y].active() && types.Contains(Main.tile[x, y].type))
                        return true;

            return false;
        }
        /// <summary>
        /// Hurt a player when he/she touches a tile.
        /// </summary>
        /// <param name="p">The player who touches or doesn't touch the tile.</param>
        /// <param name="tileType">The type of the tile to check.</param>
        /// <param name="damage">The damage to inflict to the player.</param>
        /// <param name="canHurt">A function used to check when to hurt the player.</param>
        /// <param name="deathText">The text do display when the player dies from the damage.</param>
        /// <param name="r">The radius.</param>
        public static void TileHurtPlayer(Player p, int tileType, int damage, Func<bool> canHurt = null, string deathText = " got slain...", int r = 0)
        {
            TileHurtPlayer(p, new[] { tileType }, damage, canHurt, deathText, r);
        }
        /// <summary>
        /// Hurt a player when he/she touches a tile.
        /// </summary>
        /// <param name="p">The player who touches or doesn't touch the tile.</param>
        /// <param name="tileTypes">The types of the tiles to check.</param>
        /// <param name="damage">The damage to inflict to the player.</param>
        /// <param name="canHurt">A function used to check when to hurt the player.</param>
        /// <param name="deathText">The text do display when the player dies from the damage.</param>
        /// <param name="r">The radius.</param>
        public static void TileHurtPlayer(Player p, IEnumerable<int> tileTypes, int damage, Func<bool> canHurt = null, string deathText = " got slain...", int r = 0)
        {
            if (p.immune)
                return;

            bool hurt = TouchesTile(p, r, tileTypes);

            if (canHurt != null)
                hurt &= canHurt();

            if (hurt)
                p.Hurt(damage + (p.statDefense / 2), 0, false, false, deathText);
        }
    }
}

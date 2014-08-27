using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TAPI;
using PoroCYon.MCT.Content;
using PoroCYon.MCT.Net;

namespace Avalon
{
    /// <summary>
    /// Global world stuff
    /// </summary>
    public sealed class MWorld : ModWorld
    {
        const int spawnSpaceX = 3, spawnSpaceY = 3;

        static bool needsToLoadRecipes = true, addedWings = false, scanned = false; // no idea

        internal static bool oldNight = false;

        /// <summary>
        /// Wether UltraOblivion has already been killed or not.
        /// </summary>
        public static bool UltraOblivionDowned = false;
        /// <summary>
        /// Wether Berserker Ore is already generated in the world.
        /// </summary>
        public static bool SpawnedBerserkerOre = false;

        /// <summary>
        /// How many times Cataryst has been killed.
        /// </summary>
        public static int CatarystDownedCount = 0;
        /// <summary>
        /// How many times the Armageddon Slime has been defeated.
        /// </summary>
        public static int ArmageddonCount = 0;
        /// <summary>
        /// How many times something something something.
        /// </summary>
        public static int EverIceCount = 0;
        /// <summary>
        /// How many times a hallowed altar was broken.
        /// </summary>
        public static int HallowAltarsBroken = 0;
        /// <summary>
        /// How many times a wraith in the Wraith Invasion was killed.
        /// </summary>
        public static int WraithsDowned = 0;

        /// <summary>
        /// Gets the ID of the Golden Wings texture.
        /// </summary>
        public static int GoldenWings
        {
            get;
            private set;
        }

        static int grassCounter = 0, jungleEx = 0, gCount = 0;

        /// <summary>
        /// Gets the rectangle for the Tropics biome.
        /// </summary>
        public static Rectangle TropicsRect
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets the layer containing the extra accessory slots.
        /// </summary>
        public static InterfaceLayer AccessoryLayer
        {
            get;
            private set;
        }

        internal static Item[/* player ID */][/* item index */] accessories;
        internal static Item[] loacalAccessories
        {
            get
            {
                return accessories[Main.myPlayer];
            }
            set
            {
                accessories[Main.myPlayer] = value;
            }
        }

        /// <summary>
        /// Creates a new instance of the MWorld class
        /// </summary>
        /// <param name="base">The ModBase which belongs to the ModWorld instance</param>
        /// <remarks>Called by the mod loader</remarks>
        public MWorld(ModBase @base)
            : base(@base)
        {

        }

        /// <summary>
        /// Called when the world is opened on this client or server
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            CatarystDownedCount = EverIceCount = ArmageddonCount = HallowAltarsBroken = 0;
            Mod.IsInSuperHardmode = UltraOblivionDowned = SpawnedBerserkerOre = false;

            // insert all graphical/UI-related stuff AFTER this check!
            if (Main.dedServ)
                return;

            Texture2D gWings = modBase.textures["Wings/Golden Wings"];
            foreach (Texture2D t in Main.wingsTexture.Values)
                if (gWings == t)
                {
                    addedWings = true;
                    break;
                }

            if (!addedWings)
            {
                GoldenWings = Main.dedServ ? Main.wingsTexture.Count :  ObjectLoader.AddWingsToGame(gWings);

                addedWings = true;
            }

            InterfaceLayer.Add(InterfaceLayer.cachedList, AccessoryLayer = new AccessorySlots(), InterfaceLayer.LayerInventory, false);
        }
        /// <summary>
        /// Called after the world is updated
        /// </summary>
        public override void PostUpdate()
        {
            base.PostUpdate();

            if (!Mod.IsInSuperHardmode && !UltraOblivionDowned)
                return;

            NPC n, n2;

            for (int i = 0; i < Main.npc.Length; i++)
            {
                n = Main.npc[i];

                if (n.type == 94 && n.active && Mod.IsInSuperHardmode)
                    for (int j = 0; j < Main.npc.Length; j++)
                    {
                        n2 = Main.npc[j];

                        if (n2.type == Defs.npcs["Hallowor"].type && n2.active && n.Hitbox.Intersects(n2.Hitbox))
                        {
                            Make3x3Circle((int)n.position.X / 16, (int)n.position.Y / 16, TileDef.type["Oblivion Ore"]);

                            n.active = n2.active = false;
                            n.NPCLoot();
                            Main.PlaySound(4, (int)n.position.X, (int)n.position.Y, n.soundKilled);
                            n2.NPCLoot();
                            Main.PlaySound(4, (int)n2.position.X, (int)n2.position.Y, n2.soundKilled);

                            NetHelper.SendText("Dark and light have been obliterated...", new Color(135, 78, 0));
                        }
                    }

                if (n.type == Defs.npcs["Guardian Corruptor"].type && n.active && UltraOblivionDowned)
                    for (int j = 0; j < Main.npc.Length; j++)
                    {
                        n2 = Main.npc[j];

                        if (n2.type == Defs.npcs["Aegis Hallowor"].type && n2.active && n.Hitbox.Intersects(n2.Hitbox))
                        {
                            Make3x3Circle((int)n.position.X / 16, (int)n.position.Y / 16, TileDef.type["Berserker Ore"]);

                            n.active = n2.active = false;
                            n.NPCLoot();
                            Main.PlaySound(4, (int)n.position.X, (int)n.position.Y, n.soundKilled);
                            n2.NPCLoot();
                            Main.PlaySound(4, (int)n2.position.X, (int)n2.position.Y, n2.soundKilled);

                            NetHelper.SendText("Dark and light have been annihilated...", new Color(135, 78, 0));
                        }
                    }
            }

            if (WraithsDowned >= 200)
                Mod.Wraiths.Stop();
        }

        /// <summary>
        /// Called when a Player joins the sever
        /// </summary>
        /// <param name="index">The whoAmI of the Player who is joining</param>
        public override void PlayerConnected(int index)
        {
            base.PlayerConnected(index);


        }

        /// <summary>
        /// Writes binary data to a world save file. Called when the world is closed.
        /// </summary>
        /// <param name="bb">The buffer containing the binary data.</param>
        public override void Save(BinBuffer bb)
        {
            base.Save(bb);

            bb.WriteX(
                Mod.IsInSuperHardmode,
                UltraOblivionDowned,
                SpawnedBerserkerOre,
                scanned);

            bb.WriteX(
                CatarystDownedCount,
                HallowAltarsBroken,
                ArmageddonCount,
                EverIceCount,
                WraithsDowned);

            bb.WriteX(TropicsRect.X, TropicsRect.Y, TropicsRect.Width, TropicsRect.Height);
        }
        /// <summary>
        /// Loads binary data from a world save file. Called when the world is loaded.
        /// </summary>
        /// <param name="bb">The buffer containing the binary data.</param>
        public override void Load(BinBuffer bb)
        {
            base.Load(bb);

            accessories = new Item[Main.netMode == 0 ? 1 : Main.numPlayers][];

            for (int i = 0; i < accessories.Length; i++)
            {
                accessories[i] = new Item[Mod.ExtraSlots];

                for (int j = 0; j < Mod.ExtraSlots; j++)
                    accessories[i][j] = new Item();
            }

            Mod.IsInSuperHardmode = bb.ReadBool();
            UltraOblivionDowned = bb.ReadBool();
            SpawnedBerserkerOre = bb.ReadBool();
            scanned = bb.ReadBool();

            CatarystDownedCount = bb.ReadInt();
            HallowAltarsBroken = bb.ReadInt();
            ArmageddonCount = bb.ReadInt();
            EverIceCount = bb.ReadInt();
            WraithsDowned = bb.ReadInt();

            TropicsRect = new Rectangle(bb.ReadInt(), bb.ReadInt(), bb.ReadInt(), bb.ReadInt());
        }

        /// <summary>
        /// Called after the world is generated
        /// </summary>
        public override void WorldGenPostInit()
        {
            base.WorldGenPostInit();


        }
        /// <summary>
        /// Used to modify the worldgen task list (insert additional tasks)
        /// </summary>
        /// <param name="list">The task list to modify</param>
        public override void WorldGenModifyTaskList(List<WorldGenTask> list)
        {
            base.WorldGenModifyTaskList(list);


        }
        /// <summary>
        /// Used to modify the hardmode task list (when the WoF is defeated)
        /// </summary>
        /// <param name="list">The task list to modify</param>
        public override void WorldGenModifyHardmodeTaskList(List<WorldGenTask> list)
        {
            base.WorldGenModifyHardmodeTaskList(list);


        }

        /// <summary>
        /// Gets wether a boss is active or not.
        /// </summary>
        /// <returns>true if a boss is active, false otherwise.</returns>
        public static bool CheckForBosses()
        {
            return Main.npc.Count(n => n.boss) > 0; // LINQ++. again.
        }

        /// <summary>
        /// Creates a 3x3 circle of tiles.
        /// </summary>
        /// <param name="x">The centre of the circle's X coordinate.</param>
        /// <param name="y">The centre of the circle's Y coordinate.</param>
        /// <param name="type">The type of the tiles.</param>
        public static void Make3x3Circle(int x, int y, ushort type)
        {
            for (int xoff = x - 1; xoff <= x + 1; xoff++)
                for (int yoff = y - 1; yoff <= y + 1; yoff++)
                {
                    Main.tile[xoff, yoff].active(true);
                    Main.tile[xoff, yoff].type = type;
                    
                    WorldGen.SquareTileFrame(xoff, yoff);
                }

            Main.tile[x - 2, y].active(true);
            Main.tile[x + 2, y].active(true);
            Main.tile[x, y - 2].active(true);
            Main.tile[x, y + 2].active(true);

            Main.tile[x - 2, y].type
                = Main.tile[x + 2, y].type
                = Main.tile[x, y - 2].type
                = Main.tile[x, y + 2].type
                    = type;

            WorldGen.SquareTileFrame(x - 2, y);
            WorldGen.SquareTileFrame(x + 2, y);
            WorldGen.SquareTileFrame(x, y - 2);
            WorldGen.SquareTileFrame(x, y + 2);
        }
    }
}

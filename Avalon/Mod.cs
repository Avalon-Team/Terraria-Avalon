using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Terraria;
using TAPI;
using PoroCYon.MCT;
using PoroCYon.MCT.Net;
using Avalon.API.Items.MysticalTomes;
using Avalon.API.World;

namespace Avalon
{
    /// <summary>
    /// All Avalon NetMessages.
    /// </summary>
    public enum NetMessages
    {
        /// <summary>
        /// Start the Wraith Invasion
        /// </summary>
        StartWraithInvasion,
        /// <summary>
        /// Something not really obvious
        /// </summary>
        SetMusicBox,
        /// <summary>
        /// Request tiles when teleporting with the <see cref="Items.Other.ShadowMirror" />.
        /// </summary>
        RequestTiles,
        /// <summary>
        /// Request the content of the extra accessory slots and the tome slot.
        /// </summary>
        RequestCustomSlots,
        /// <summary>
        /// Send the content of the extra accessory slots and the tome slot. Usually sent as a response to <see cref="RequestCustomSlots" />.
        /// </summary>
        SendCustomSlots,
        /// <summary>
        /// Activates a <see cref="SkillManager" />.
        /// </summary>
        ActivateSkill
    }

    /// <summary>
    /// The entry point of the Avalon mod.
    /// </summary>
    /// <remarks>Like 'Program' but for a mod</remarks>
    public sealed class Mod : ModBase
    {
        /// <summary>
        /// Gets the singleton instance of the mod's <see cref="ModBase" />.
        /// </summary>
        public static Mod Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// The amount of extra accessory slots.
        /// </summary>
        public const int ExtraSlots = 3;

        Option
            tomeSkillHotkey = null;

        internal static List<BossSpawn> spawns = new List<BossSpawn>();
        readonly static List<int> EmptyIntList = new List<int>(); // only alloc once

        /// <summary>
        /// Gets or sets the Mystical Tomes skill hotkey.
        /// </summary>
        public static Keys TomeSkillHotkey
        {
			get
			{
				return (Keys)Instance.tomeSkillHotkey.Value;
			}
			set
			{
				Instance.tomeSkillHotkey.Value = value;
			}
		}

		/// <summary>
		/// Gets the Wraiths invasion instance.
		/// </summary>
		public static Invasion Wraiths
        {
            get;
            internal set;
        }
        /// <summary>
        /// Gets wether the game is in superhardmode or not.
        /// </summary>
        public static bool IsInSuperHardmode
        {
            get;
            internal set;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Mod" /> class.
        /// </summary>
        /// <remarks>Called by the mod loader.</remarks>
        public Mod()
            : base()
        {
            Instance = this;
        }

        /// <summary>
        /// When the mod receives data from a peer connection.
        /// </summary>
        /// <param name="msg">The message type.</param>
        /// <param name="bb">The content of the message.</param>
        public override void NetReceive(int msg, BinBuffer bb)
        {
            // commonly used vars
            int id;

            switch ((NetMessages)msg)
            {
                case NetMessages.StartWraithInvasion:
                    // todo
                    break;
                case NetMessages.SetMusicBox:
                    // todo
                    break;
                case NetMessages.RequestTiles:
                    NetMessage.SendTileSquare(bb.ReadInt(), bb.ReadInt(), bb.ReadInt(), bb.ReadInt());
                    break;
                case NetMessages.RequestCustomSlots:
                    BinBuffer itemB = new BinBuffer();

                    itemB.Write (Main.myPlayer);
                    itemB.WriteX(MWorld.localAccessories);
                    itemB.Write (MWorld.localTome);

                    itemB.Pos = 0;

                    NetHelper.SendModData(this, NetMessages.SendCustomSlots, bb.ReadInt(), -1, itemB.ReadBytes());
                    break;
                case NetMessages.SendCustomSlots:
                    id = bb.ReadInt();

                    for (int i = 0; i < ExtraSlots; i++)
                        MWorld.accessories[id][i] = bb.ReadItem();

                    MWorld.tomes   [id] = bb.ReadItem();
                    MWorld.managers[id] = SkillManager.FromItem(MWorld.tomes[id]);
                    break;
                case NetMessages.ActivateSkill:
                    id = bb.ReadInt();

                    if (MWorld.managers[id] == null)
                        MWorld.managers[id] = SkillManager.FromItem(MWorld.tomes[id]);

                    if (MWorld.managers[id] != null)
                        MWorld.managers[id].Activate(Main.player[id]); // id is both client id and player id.
                    break;
            }
        }

        /// <summary>
        /// Called when the mod is loaded.
        /// </summary>
        public override void OnLoad  ()
        {
            Invasion.LoadVanilla();

            LoadBiomes();
            LoadInvasions();
            LoadSpawns();

            base.OnLoad();
        }
        /// <summary>
        /// Called when the mod is unloaded.
        /// </summary>
        public override void OnUnload()
        {
            Instance = null;

            spawns.Clear();

            base.OnUnload();
        }

        /// <summary>
        /// Called when an option is changed.
        /// </summary>
        /// <param name="option">The option that has changed.</param>
        public override void OptionChanged(Option option)
        {
            base.OptionChanged(option);

            switch (option.name)
            {
                case "TomeSkillHotkey":
					// dirty hack so you can change the option from anywhere
					if (tomeSkillHotkey != option)
						tomeSkillHotkey = option;
					break;
            }
        }

        static void LoadBiomes()
        {
            #region edit vanilla
            //Biome.Biomes["Dungeon"].typesIncrease.Add(TileDef.type["Avalon:Dungeon Orange Brick"]);

            //Biome.Biomes["Ocean"].typesIncrease.Add(TileDef.type["Avalon:Black Sand"]);
            ////Biome.Biomes["Ocean"].TileValid = /* set! */ (x, y, pid) =>
            ////{
            ////    return NPC.areaWater && y < Main.rockLayer && (x < 250 || x > Main.maxTilesX - 250)
            ////        && Biome.Biomes["Ocean"].typesIncrease.Contains(Main.tile[x, y].type);
            ////};

            //Biome.Biomes["Overworld"].TileValid += /* add! */ (x, y, pid) =>
            //{
            //    Player p = Main.player[pid];

            //    return !p.zone["Avalon:Tropics"] && !p.zone["Avalon:Comet"] && !p.zone["Avalon:Ice Cave"]
            //        && !p.zone["Avalon:Hellcastle"] && !p.zone["Avalon:Sky Fortress"];
            //};
            #endregion

            #region custom ones
            //new Biome("Avalon:Comet", new List<int> { TileDef.type["Avalon:Ever Ice"] }, EmptyIntList, 50).AddToGame();

            //new Biome("Avalon:Tropics", new List<int>
            //{
            //    TileDef.type["Avalon:Black Sand"], TileDef.type["Avalon:Tropical Mud"], TileDef.type["Avalon:Tropical Grass"], TileDef.type["Avalon:Tropic Stone"]
            //}, new List<int> { }, 80).AddToGame();

            //new Biome("Avalon:Ice Cave", new List<int> { TileDef.type["Avalon:Ice Block"] }, EmptyIntList, 50).AddToGame();

            //new Biome("Avalon:Hellcastle", new List<int> { TileDef.type["Avalon:Impervious Brick"], TileDef.type["Avalon:Resistant Wood"] }, EmptyIntList, 100)
            //{
            //    TileValid = (x, y, pid) => y < Main.maxTilesX - 200 && Biome.Biomes["Avalon:Hellcastle"].typesIncrease.Contains(Main.tile[x, y].type)
            //}.AddToGame();

            //new Biome("Avalon:Sky Fortress", new List<int> { TileDef.type["Avalon:Reinforced Glass"], TileDef.type["Avalon:Hallowstone Block"] }, EmptyIntList, 100)
            //{
            //    TileValid = (x, y, pid) => y < 200 && Biome.Biomes["Avalon:Sky Fortress"].typesIncrease.Contains(Main.tile[x, y].type)
            //}.AddToGame();

            //new Biome("Avalon:Clouds", new List<int> { TileDef.type["Avalon:Cloud"] }, EmptyIntList, 35)
            //{
            //    TileValid = (x, y, pid) => y < 200 && Biome.Biomes["Avalon:Clouds"].typesIncrease.Contains(Main.tile[x, y].type)
            //}.AddToGame();
            #endregion
        }
        static void LoadInvasions()
        {
            Invasion.Load(Instance, "Wraiths", Wraiths = new WraithInvasion());
        }
        static void LoadSpawns()
        {
            RegisterBossSpawn(new BossSpawn()
            {
                CanSpawn = (r, p) => !Main.dayTime && Main.rand.Next(30000 * r / 5) == 0,
                Type = 4 // Eye of Ctulhu
            });
            RegisterBossSpawn(new BossSpawn()
            {
                CanSpawn = (r, p) => !Main.dayTime && Main.rand.Next(36000 * r / 5) == 0,
                Type = 134 // probably The Destroyer
            });
            RegisterBossSpawn(new BossSpawn()
            {
                CanSpawn = (r, p) => !Main.dayTime && Main.rand.Next(40000 * r / 5) == 0,
                Type = 127 // probably The Twins or Skeletron Prime
            });

            //RegisterBossSpawn(new BossSpawn()
            //{
            //    CanSpawn = (r, p) => Main.dayTime && Main.sandTiles >= 100 && Main.rand.Next(30000 * r / 5) == 0,
            //    NpcInternalName = "Avalon:Desert Beak"
            //});
            //RegisterBossSpawn(new BossSpawn()
            //{
            //    CanSpawn = (r, p) => Main.dayTime && p.zoneJungle          && Main.rand.Next(30000 * r / 5) == 0,
            //    NpcInternalName = "Avalon:King Sting"
            //});
            //RegisterBossSpawn(new BossSpawn()
            //{
            //    CanSpawn = (r, p) => Main.dayTime && p.zoneHoly            && Main.rand.Next(42000 * r / 5) == 0,
            //    NpcInternalName = "Avalon:Cataryst"
            //});

            RegisterBossSpawn(new WraithSpawn());
        }

        /// <summary>
        /// Registers a BossSpawn.
        /// </summary>
        /// <param name="bs">The BossSpawn to register.</param>
        public static void RegisterBossSpawn(BossSpawn bs)
        {
            spawns.Add(bs);
        }
    }

    class WraithInvasion : Invasion
    {
        public override string DisplayName
        {
            get
            {
                return "Wraiths";
            }
        }

        public override string ArrivedText
        {
            get
            {
                return "The " + DisplayName + " have arrived!";
            }
        }
        public override string DefeatedText
        {
            get
            {
                return "The " + DisplayName + " have been arrived!";
            }
        }

        internal WraithInvasion()
            : base()
        {
            StartText = d => DisplayName + " are coming from the " + d + "!";
        }
    }
    class WraithSpawn : BossSpawn
    {
        public override bool ShouldSpawn(int rate, Player p)
        {
            bool ret = Main.rand.Next(10) == 0 && Main.time == 0;

            if (!MWorld.oldNight || Main.dayTime)
                ret = false;

            MWorld.oldNight = !Main.dayTime;

            return ret;
        }
        public override void Spawn(int pid)
        {
            Mod.Wraiths.Start();
        }
    }
}

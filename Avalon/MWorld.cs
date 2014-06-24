using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using PoroCYon.MCT.Net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PoroCYon.MCT.Content;

namespace Avalon
{
    /// <summary>
    /// Global world stuff
    /// </summary>
    public sealed class MWorld : ModWorld
    {
        const int spawnSpaceX = 3, spawnSpaceY = 3;

        static bool needsToLoadRecipes = true;
        static bool addedWings = false;   // no idea
        static bool scanned = false; // no idea

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
        /// How many times something something wraith invasion something.
        /// </summary>
        public static int WraithInvasionCount = 0;

        /// <summary>
        /// Gets the ID of the Golden Wings texture.
        /// </summary>
        public static int GoldenWings
        {
            get;
            private set;
        }

        static int grassCounter = 0, jungleEx = 0;

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

            Texture2D gWings = modBase.textures["Wings/Golden Wings"];
            foreach (Texture2D t in Main.wingsTexture.Values)
                if (gWings == t)
                {
                    addedWings = true;
                    break;
                }

            if (!addedWings)
            {
                GoldenWings = Main.dedServ ? Main.wingsTexture.Count :  ObjectLoader.AddWingsToGame(gWings, null /* param not used */);

                addedWings = true;
            }

            if (!Main.dedServ)
                InterfaceLayer.Add(InterfaceLayer.cachedList, AccessoryLayer = AccessorySlots.GetNewLayer(), InterfaceLayer.LayerInventory, false);
        }
        /// <summary>
        /// Called after the world is updated
        /// </summary>
        public override void PostUpdate()
        {
            base.PostUpdate();


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
        /// Called when the world is loaded.
        /// </summary>
        /// <param name="bb"></param>
        public override void Load(BinBuffer bb)
        {
            base.Load(bb);

            accessories = new Item[NetHelper.CurrentMode == NetMode.Singleplayer ? 1 : Main.numPlayers][];

            for (int i = 0; i < accessories.Length; i++)
            {
                accessories[i] = new Item[Mod.ExtraSlots];

                for (int j = 0; j < Mod.ExtraSlots; j++)
                    accessories[i][j] = new Item();
            }
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
    }
}

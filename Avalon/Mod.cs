using System;
using System.Collections.Generic;
using System.Linq;
using PoroCYon.MCT;
using Terraria;
using TAPI;

namespace Avalon
{
    /// <summary>
    /// Like 'Program' but for a mod
    /// </summary>
    public sealed class Mod : ModBase
    {
        readonly static List<int> EmptyIntList = new List<int>() { }; // only alloc once

        /// <summary>
        /// The amount of extra accessory slots
        /// </summary>
        public const int ExtraSlots = 3;

        /// <summary>
        /// Creates a new instance of the Mod class
        /// </summary>
        /// <remarks>Called by the mod loader</remarks>
        public Mod()
            : base()
        {

        }

        /// <summary>
        /// Called when the mod is loaded
        /// </summary>
        public override void OnLoad()
        {
            // initializes most of the MCT features
            Mct.EnsureMct(modName);
            Mct.Init();

            LoadBiomes();

            base.OnLoad();
        }

        static void LoadBiomes()
        {
            #region edit vanilla
            Biome.Biomes["Dungeon"].typesIncrease.Add(TileDef.type["Avalon:Dungeon Orange Brick"]);

            Biome.Biomes["Ocean"].typesIncrease.Add(TileDef.type["Avalon:Black Sand"]);
            //Biome.Biomes["Ocean"].TileValid = /* set! */ (x, y, pid) =>
            //{
            //    return NPC.areaWater && y < Main.rockLayer && (x < 250 || x > Main.maxTilesX - 250)
            //        && Biome.Biomes["Ocean"].typesIncrease.Contains(Main.tile[x, y].type);
            //};

            Biome.Biomes["Overworld"].TileValid += /* add! */ (x, y, pid) =>
            {
                Player p = Main.player[pid];

                return !p.zone["Avalon:Tropics"] && !p.zone["Avalon:Comet"] && !p.zone["Avalon:Ice Cave"]
                    && !p.zone["Avalon:Hellcastle"] && !p.zone["Avalon:Sky Fortress"];
            };
            #endregion

            #region custom ones
            new Biome("Avalon:Comet", new List<int> { TileDef.type["Avalon:Ever Ice"] }, EmptyIntList, 50).AddToGame();

            new Biome("Avalon:Tropics", new List<int>
            {
                TileDef.type["Avalon:Black Sand"], TileDef.type["Avalon:Tropical Mud"], TileDef.type["Avalon:Tropical Grass"], TileDef.type["Avalon:Tropic Stone"]
            }, new List<int> { }, 80).AddToGame();

            new Biome("Avalon:Ice Cave", new List<int> { TileDef.type["Avalon:Ice Block"] }, EmptyIntList, 50).AddToGame();

            new Biome("Avalon:Hellcastle", new List<int> { TileDef.type["Avalon:Impervious Brick"], TileDef.type["Avalon:Resistant Wood"] }, EmptyIntList, 100)
            {
                TileValid = (x, y, pid) => y < Main.maxTilesX - 200 && Biome.Biomes["Avalon:Hellcastle"].typesIncrease.Contains(Main.tile[x, y].type)
            }.AddToGame();

            new Biome("Avalon:Sky Fortress", new List<int> { TileDef.type["Avalon:Reinforced Glass"], TileDef.type["Avalon:Hallowstone Block"] }, EmptyIntList, 100)
            {
                TileValid = (x, y, pid) => y < 200 && Biome.Biomes["Avalon:Sky Fortress"].typesIncrease.Contains(Main.tile[x, y].type)
            }.AddToGame();

            new Biome("Avalon:Clouds", new List<int> { TileDef.type["Avalon:Cloud"] }, EmptyIntList, 35)
            {
                TileValid = (x, y, pid) => y < 200 && Biome.Biomes["Avalon:Clouds"].typesIncrease.Contains(Main.tile[x, y].type)
            }.AddToGame();
            #endregion
        }
    }
}

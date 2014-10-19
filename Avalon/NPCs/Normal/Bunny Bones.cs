using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.NPCs.Normal
{
    /// <summary>
    /// The Bunny Bones.
    /// </summary>
    public sealed class BunnyBones : ModNPC
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="type"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool CanSpawn(int x, int y, int type, Player p)
        {
            return NPC.downedBoss3 || Main.rand.Next(9) != 0 && (Main.localPlayer.zoneBlood || Main.localPlayer.zoneEvil || Biome.Biomes["RockLayer"].Check(p));
        }
    }
}

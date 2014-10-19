using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.NPCs.Normal
{
    /// <summary>
    /// The Crawleye.
    /// </summary>
    public sealed class Crawleye : ModNPC
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
            return Main.localPlayer.zoneBlood && Main.rand.Next(4) == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void NPCLoot()
        {
            Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Avalon:Crawleye Gore 1"], 1f);
            Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Avalon:Crawleye Gore 2"], 1f);
            Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Avalon:Crawleye Gore 3"], 1f);
        }
    }
}

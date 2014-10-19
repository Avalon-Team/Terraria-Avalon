using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.NPCs.Normal
{
    /// <summary>
    /// The Armoured Crimislime.
    /// </summary>
    public sealed class ArmouredCrimslime : ModNPC
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
            Main.NewText("Working!"); // or not...

            return Main.localPlayer.zoneBlood && Main.hardMode && Main.rand.Next(7) == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void NPCLoot()
        {
            Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Avalon:Armoured Crimslime Gore 1"], 1f);
            Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Avalon:Armoured Crimslime Gore 2"], 1f);
        }
    }
}

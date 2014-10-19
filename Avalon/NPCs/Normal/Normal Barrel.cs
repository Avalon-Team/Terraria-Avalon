using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.NPCs.Normal
{
    /// <summary>
    /// The Normal Barrel.
    /// </summary>
    public sealed class NormalBarrel : ModNPC
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
            return Main.rand.Next(19) == 1;
        }
    }
}

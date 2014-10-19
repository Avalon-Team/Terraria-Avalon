using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Items.DevItems.Bullseye55
{
    /// <summary>
    /// Bullseye55's dev Warhammer.
    /// </summary>
    public sealed class Bullseye55DevWarhammer : ModItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool? UseItem(Player p)
        {
            Main.PlaySound(3, -1, -1, 13);
            return true;
        }
    }
}

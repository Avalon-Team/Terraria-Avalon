using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Items.DevItems.Bullseye55
{
    /// <summary>
    /// The Hard Swap.
    /// </summary>
	public sealed class HardSwap : ModItem
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool? UseItem(Player p) 
        {
            Main.NewText("Hardmode " + ((Main.hardMode = !Main.hardMode) ? "On" : "Off"));

            return true;
        }
	}
}

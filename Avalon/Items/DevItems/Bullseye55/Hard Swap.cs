using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Items.DevItems;

namespace Avalon.Items.DevItems.Bullseye55
{
    /// <summary>
    /// The Hard Swap.
    /// </summary>
	public sealed class HardSwap : DevItem
	{
        /// <summary>
        /// Creates a new instance of the <see cref="HardSwap" /> class.
        /// </summary>
        public HardSwap()
            : base(DEV_DATA.Bullseye55)
        {

        }

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

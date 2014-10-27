using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Items.DevItems;

namespace Avalon.Items.DevItems.Bullseye55
{
    /// <summary>
    /// Bullseye55's dev Pickaxe.
    /// </summary>
    public sealed class Bullseye55DevPickaxe : DevItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Bullseye55DevPickaxe" /> class.
        /// </summary>
        public Bullseye55DevPickaxe()
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
            Main.PlaySound(3, -1, -1, 13);
            return true;
        }
    }
}

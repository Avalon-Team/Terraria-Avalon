using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Items.DevItems.Bullseye55
{
    /// <summary>
    /// Bullseye55's dev Pickaxe.
    /// </summary>
    public sealed class Bullseye55DevPickaxe : ModItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        public override bool? UseItem(Player P)
        {
            Main.PlaySound(3, -1, -1, 13);
            return true;
        }
    }
}

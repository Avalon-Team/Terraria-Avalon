using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Items.Potions
{
    /// <summary>
    /// The Medicine Bottle.
    /// </summary>
    public sealed class MedicineBottle : ModItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool? UseItem(Player p)
        {
            int heal = Math.Max(Main.rand.Next(90, 130), p.statLifeMax2 - p.statLife);

            p.statLife += heal;

            p.HealEffect(heal);

            return true;
        }
    }
}

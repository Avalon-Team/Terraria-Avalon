using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Weapons.Ranged.Guns
{
    /// <summary>
    /// The Ichorthrower.
    /// </summary>
    public class Ichorthrower : ModItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool ConsumeAmmo(Player p)
        {
            return Main.rand.Next(6) == 0;
        }
    }
}

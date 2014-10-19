using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Items.Weapons.Ranged.Guns
{
    /// <summary>
    /// The Cursed Flamethrower.
    /// </summary>
    public sealed class CursedFlamethrower : ModItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool CanUse(Player p)
        {
            return p.inventory.Any(i => !i.IsBlank() && i.type == 522);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool ConsumeAmmo(Player p)
        {
            return Main.rand.Next(6) == 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Items.Weapons.Ranged.Cannons
{
    /// <summary>
    /// The Vertebrae Cannon.
    /// </summary>
	public sealed class VertebraeCannon : ModItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pos"></param>
        /// <param name="vel"></param>
        /// <param name="type"></param>
        /// <param name="dmg"></param>
        /// <param name="kb"></param>
        /// <returns></returns>
        public override bool PreShoot(Player p, Vector2 pos, Vector2 vel, int type, int dmg, float kb)
        {
            foreach (Item i in p.inventory)
                if (i.type == ItemDef.byName["Vanilla:Vertebrae"].type && i.stack >= 1)
                {
                    i.stack -= 1;
                    return true;
                }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool CanUse(Player p)
        {
            return p.inventory.Any(i => i.type == ItemDef.byName["Vanilla:Vertebrae"].type && !i.IsBlank());
        }
    }
}

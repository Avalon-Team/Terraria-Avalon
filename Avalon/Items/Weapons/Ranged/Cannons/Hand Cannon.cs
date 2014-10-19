using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Items.Weapons.Ranged.Cannons
{
    /// <summary>
    /// The Hand Cannon.
    /// </summary>
	public class HandCannon : ModItem
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
            for (int i = 0; i < p.inventory.Length; i++)
                if (p.inventory[i].type == ItemDef.byName["Vanilla:Cannonball"].type && p.inventory[i].stack >= 1)
                {
                    //Projectile.NewProjectile(p.Centre.X, p.Centre.Y, p.velocity.X, p.velocity.Y, "Cannonball", item.damage, item.knockBack, 1);
                    p.inventory[i].stack -= 1;

                    return true;
                }

            return false;
        }
	}
}

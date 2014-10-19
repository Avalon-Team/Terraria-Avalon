using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Items.Weapons.Ranged.Bows
{
    /// <summary>
    /// The Verbrae Bow.
    /// </summary>
    public class VertebraeBow : ModItem
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
            Projectile arr = Main.projectile[Projectile.NewProjectile(p.Centre.X, p.Centre.Y,vel.X, vel.Y, 278, dmg, kb, p.whoAmI)];
            arr.aiStyle = 8; // 207 Chloro Bullets 297 Spectre Staff
            arr.penetrate = 2;

            return false;
        }
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using TAPI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Avalon.Projectiles.FromPlayer.Bullets
{
    /// <summary>
    /// The Binge Eater.
    /// </summary>
    public sealed class BingeEater : ModProjectile
    {
        int
            hits = 0,
            defDmg = 0;

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            defDmg = projectile.damage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="hitDir"></param>
        /// <param name="damage"></param>
        /// <param name="knockback"></param>
        /// <param name="crit"></param>
        /// <param name="critMult"></param>
        public override void DamageNPC(NPC npc, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult)
        {
            Dust d = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, 0, 0, 100, new Color(), 1.5f)];
            d.noGravity = false;

            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);

            if (++hits == 1)
            {
                if (projectile.damage <= (defDmg * 3))
                {
                    projectile.damage += 4;
                    projectile.scale += 0.05f;
                    d.scale += 0.06f;

                    hits = 0;
                }
                if (projectile.damage >= (defDmg * 2))
                {
                    Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, 1330, 1, false, 0);
                    hits = 0;
                }
            }
        }
    }
}

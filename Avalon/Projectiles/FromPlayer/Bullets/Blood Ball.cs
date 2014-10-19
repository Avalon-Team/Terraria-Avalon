using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Projectiles.FromPlayer.Bullets
{
    /// <summary>
    /// The Blood Ball.
    /// </summary>
    public sealed class BloodBall : ModProjectile
    {
        int timer = 0;

        /// <summary>
        /// 
        /// </summary>
        public override void AI()
        {
            Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, 0, 0, 100, new Color(), 3f)].noGravity = true;

            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);

            if (++timer == 40)
            {
                projectile.velocity *= (float)(Main.rand.NextDouble() * 3.5 + 1.5f);
                timer = 0;
            }
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
            npc.AddBuff(69, 600, true);
        }
    }
}

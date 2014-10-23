using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Projectiles.FromPlayer.Spells
{
    /// <summary>
    /// The Freeze Bolt.
    /// </summary>
    public sealed class FreezeBolt : ModProjectile
    {
        /// <summary>
        /// 
        /// </summary>
        public override void AI()
        {
            for (int i = 0; i < 5; i++)
            {
                const int offset = 4;

                Dust d = Main.dust[Dust.NewDust(projectile.position + new Vector2(offset), projectile.width - offset * 2, projectile.height - offset * 2, 135, 0f, 0f, 100, default(Color), 1.2f)];

                d.noGravity = true;
                d.velocity *= 0.1f;
                d.velocity += projectile.velocity * 0.1f;
                d.position -= projectile.velocity / 3f * i;
            }

            projectile.rotation += 0.3f * (float)projectile.direction;
            if (Main.rand.Next(5) == 0)
            {
                const int offset = 2;

                Dust d = Main.dust[Dust.NewDust(projectile.position + new Vector2(offset), projectile.width - offset * 2, projectile.height - offset * 2, 15, 0f, 0f, 100, default(Color), 0.6f)];

                d.velocity *= 0.25f;
                d.velocity += projectile.velocity * 0.5f;
            }

            if (projectile.ai[1] >= 20f)
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;

            if (projectile.velocity.Y > 16f)
                projectile.velocity.Y = 16f;
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
            if (Main.rand.Next(10) == 1)
                npc.AddBuff(BuffDef.byName["Avalon:Freeze"], 300, true);
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Kill()
        {
            Main.PlaySound(2, -1, -1, 10);
            projectile.active = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="velocityChange"></param>
        /// <returns></returns>
        public override bool OnTileCollide(ref Vector2 velocityChange)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            projectile.ai[0] += 1f;

            if (projectile.ai[0] >= 7f)
            {
                projectile.position += projectile.velocity;
                projectile.Kill();
            }
            else
            {
                if (projectile.velocity.Y != velocityChange.Y)
                    projectile.velocity.Y = -velocityChange.Y;
                if (projectile.velocity.X != velocityChange.X)
                    projectile.velocity.X = -velocityChange.X;
            }

            return false;
        }
    }
}

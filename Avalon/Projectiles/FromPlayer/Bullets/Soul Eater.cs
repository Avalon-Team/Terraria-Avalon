using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Projectiles.FromPlayer.Bullets
{
    /// <summary>
    /// The Soul Eater.
    /// </summary>
    public sealed class SoulEater : ModProjectile
    {
        int
            shootTimer = 0,
            defDmg     = 0;

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
        public override void AI()
        {
            if (++shootTimer == 24)
            {
                Projectile p = Main.projectile[Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, 307, defDmg / 4, 1, projectile.owner)];

                p.ranged = true;
                p.melee = false;

                shootTimer = 0;
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
            Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 37, 0, 0, 100, new Color(), 1.5f)].noGravity = false;
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);

            int P2 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, 307, (defDmg / 4), 1, Main.myPlayer);
            int P3 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, 307, (defDmg / 4), 1, Main.myPlayer);

            Main.projectile[P2].ranged = true ;
            Main.projectile[P2].melee  = false;
            Main.projectile[P3].ranged = true ;
            Main.projectile[P3].melee  = false;

            if (Main.rand.Next(35) == 0)
            {
                int type = 0;

                switch (Main.rand.Next(6))
                {
                    case 0:
                        type = 520;
                        break;
                    case 1:
                        type = 521;
                        break;
                    case 2:
                        type = 547;
                        break;
                    case 3:
                        type = 548;
                        break;
                    case 4:
                        type = 549;
                        break;
                    case 5:
                        type = 575;
                        break;
                }

                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, type, -1, false, 0);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Projectiles.FromPlayer
{
    /// <summary>
    /// The Ichor Glob.
    /// </summary>
    public sealed class IchorGlob : ModProjectile
    {
        /// <summary>
        /// 
        /// </summary>
        public override void AI()
        {
            Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 162, 0, 0, 100, new Color(), 0.6f)].noGravity = false;
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Kill()
        {
            //Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, Defs.projectiles["Avalon:Ichor Explosion"].type, 35, 0, projectile.whoAmI);
            Main.PlaySound(3, -1, -1, 9);

            Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 162, 0, 0, 100, new Color(), 3f)].noGravity = false;
        }
    }
}

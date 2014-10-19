using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Projectiles.FromPlayer.Arrows
{
    /// <summary>
    /// The Vertebrae Rivet.
    /// </summary>
    public sealed class VertebraeRivet : ModProjectile
    {
        /// <summary>
        /// 
        /// </summary>
        public override void AI()
        {
            Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 5, 0, 0, 100, new Color(), 1f)].noGravity = false;

            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
        }
        /// <summary>
        /// 
        /// </summary>
        public override void PostKill() //spawns dust on collision and plays a sound.
        {
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height); //tile dust
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
        }
    }
}

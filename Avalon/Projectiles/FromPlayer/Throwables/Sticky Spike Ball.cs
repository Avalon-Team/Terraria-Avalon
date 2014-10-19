using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TAPI;

namespace Avalon.Projectiles.FromPlayer.Throwables
{
    /// <summary>
    /// The Sticky Spike Ball.
    /// </summary>
    public sealed class StickySpikeBall : ModProjectile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="velocityChange"></param>
        /// <returns></returns>
        public override bool OnTileCollide(ref Vector2 velocityChange) 
        {
            projectile.velocity = Vector2.Zero;

            return false;
        }
    }
}

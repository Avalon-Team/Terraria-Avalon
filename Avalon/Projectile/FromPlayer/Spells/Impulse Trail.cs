using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Projectiles.FromPlayer.Spells
{
    /// <summary>
    /// The Impulse Trail.
    /// </summary>
    public sealed class ImpulseTrail : ModProjectile
    {
        /// <summary>
        /// Executed after <see cref="Projectile.AI" /> is executed.
        /// </summary>
        /// <remarks>Still executes, even if the preceding <see cref="Projectile" />.PreAI returned false.</remarks>
        public override void PostAI()
        {
            base.PostAI();

            projectile.alpha = 255 - projectile.timeLeft * 2 - (int)(25f * projectile.scale);

            //if (projectile.alpha < 100)
            //    projectile.alpha = 0;
        }
    }
}

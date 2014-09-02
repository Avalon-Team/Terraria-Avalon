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
        /// Creates a new instance of the <see cref="ImpulseTrail" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this projectile.</param>
        /// <param name="p">The <see cref="Projectile" /> to attach the <see cref="ModProjectile" /> to.</param>
        public ImpulseTrail(ModBase @base, Projectile p)
            : base(@base, p)
        {

        }

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

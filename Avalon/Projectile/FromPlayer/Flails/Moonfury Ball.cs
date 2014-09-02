using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Projectiles.FromPlayer.Flails
{
    /// <summary>
    /// The Moonfury Ball.
    /// </summary>
    public sealed class MoonfuryBall : ModProjectile
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MoonfuryBall" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this projectile.</param>
        /// <param name="p">The <see cref="Projectile" /> to attach the <see cref="ModProjectile" /> to.</param>
        public MoonfuryBall(ModBase @base, Projectile p)
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

            Main.dust[Dust.NewDust(projectile.Hitbox, 27, 0f, 0f, 80, Color.White, 1.5f)].noGravity = true;
        }
    }
}

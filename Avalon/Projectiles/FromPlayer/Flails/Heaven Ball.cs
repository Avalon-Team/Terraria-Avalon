using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;
using Avalon.API;

namespace Avalon.Projectiles.FromPlayer.Flails
{
    /// <summary>
    /// The Heaven Ball.
    /// </summary>
    public sealed class HeavenBall : ModProjectile
    {
        /// <summary>
        /// Executed after <see cref="Projectile.AI" /> is executed.
        /// </summary>
        /// <remarks>Still executes, even if the preceding <see cref="Projectile" />.PreAI returned false.</remarks>
        public override void PostAI()
        {
            base.PostAI();

            Main.dust[ExtendedSpawning.NewDust(projectile.Hitbox, 57, Vector2.Zero, 80, Color.White, 1.5f)].noGravity = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Projectiles.FromPlayer.Spells
{
    /// <summary>
    /// The Impulse Bolt.
    /// </summary>
    public sealed class ImpulseBolt : ModProjectile
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ImpulseBolt" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this projectile.</param>
        /// <param name="p">The <see cref="Projectile" /> to attach the <see cref="ModProjectile" /> to.</param>
        public ImpulseBolt(ModBase @base, Projectile p)
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

            if (projectile.scale == 1.0)
            {
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);

                if (Main.time % 6 <= 1)
                {
                    int pID = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, "Avalon:Impulse Trail", 10, 0f, projectile.owner);

                    Main.projectile[pID].rotation = projectile.rotation;
                    Main.projectile[pID].timeLeft = projectile.timeLeft;

                    projectile.rotation += (Main.rand.Next(200) - 100) / 100f;

                    projectile.velocity.Y = (float)Math.Sin(projectile.rotation) * 10f;
                    projectile.velocity.X = (float)Math.Cos(projectile.rotation) * 10f;
                }
            }

            if (((Main.rand.Next(15) == 0 && projectile.ai[0] > 0) || projectile.scale == 1.0) && projectile.scale > 0.4f)
            {
                int pID = Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, "Avalon:Impulse Bolt", 50, 0f, projectile.owner);
                Main.projectile[pID].scale = projectile.scale * (Main.rand.Next(100) / 100f);
                Main.projectile[pID].rotation = projectile.rotation + (Main.rand.Next(100) - 50) / 100f;
                Main.projectile[pID].timeLeft = projectile.timeLeft;
                Main.projectile[pID].position.X += (float)Math.Cos(projectile.rotation) * Main.projectile[pID].scale * 48f;
                Main.projectile[pID].position.Y += (float)Math.Sin(projectile.rotation) * Main.projectile[pID].scale * 48f;
                Main.projectile[pID].position.X += (float)Math.Cos(Main.projectile[pID].rotation) * Main.projectile[pID].scale * 48f;
                Main.projectile[pID].position.Y += (float)Math.Sin(Main.projectile[pID].rotation) * Main.projectile[pID].scale * 48f;
            }

            projectile.alpha = 255 - projectile.timeLeft * 2 - (int)(25f * projectile.scale);
            //if (projectile.alpha < 100)
            //    projectile.alpha = 0;

            if (!Main.tile[(int)(projectile.position.X / 16f), (int)(projectile.position.Y / 16f)].active())
                projectile.tileCollide = true;
        }
    }
}

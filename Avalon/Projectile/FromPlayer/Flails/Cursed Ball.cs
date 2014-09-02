using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Projectiles.FromPlayer.Flails
{
    /// <summary>
    /// The Cursed Ball.
    /// </summary>
    public sealed class CursedBall : ModProjectile
    {
        /// <summary>
        /// Creates a new instance of the <see cref="CursedBall" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this projectile.</param>
        /// <param name="p">The <see cref="Projectile" /> to attach the <see cref="ModProjectile" /> to.</param>
        public CursedBall(ModBase @base, Projectile p)
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

            Main.dust[Dust.NewDust(projectile.Hitbox, 74)].noGravity = true;
        }

        /// <summary>
        /// When an <see cref="NPC" /> is damaged by the <see cref="Projectile" />.
        /// </summary>
        /// <param name="n">The <see cref="NPC" /> that got damaged.</param>
        /// <param name="dir">In which direction the <see cref="NPC"/> got hit.</param>
        /// <param name="dmg">The damage dealt to the <see cref="NPC" />.</param>
        /// <param name="kb">The knockback the <see cref="NPC" /> wil receive.</param>
        /// <param name="crit">Wether it was a critical hit or not.</param>
        /// <param name="cMult">The damage multiplier of a critical hit.</param>
        public override void DamageNPC(NPC n, int dir, ref int dmg, ref float kb, ref bool crit, ref float cMult)
        {
            base.DamageNPC(n, dir, ref dmg, ref kb, ref crit, ref cMult);

            if (Main.rand.Next(3) == 0)
                n.AddBuff(39, 300);
        }
        /// <summary>
        /// When a <see cref="Player" /> in PvP is damaged by the <see cref="Projectile" />.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that got damaged.</param>
        /// <param name="dir">In which direction the <see cref="Player"/> got hit.</param>
        /// <param name="dmg">The damage dealt to the <see cref="Player" />.</param>
        /// <param name="crit">Wether it was a critical hit or not.</param>
        /// <param name="cMult">The damage multiplier of a critical hit.</param>
        public override void DamagePVP(Player p, int dir, ref int dmg, ref bool crit, ref float cMult)
        {
            base.DamagePVP(p, dir, ref dmg, ref crit, ref cMult);

            if (Main.rand.Next(3) == 0)
                p.AddBuff(39, 300);
        }
    }
}

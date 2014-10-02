using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;
using Avalon.API;
using Avalon.API.Items.MysticalTomes;

namespace Avalon.Items.MysticalTomes
{
    /// <summary>
    /// Adventures and Mishaps.
    /// </summary>
    [TomeSkill(typeof(AdventuresAndMishapsSkill))]
    public sealed class AdventuresAndMishaps : ModItem
    {
        /// <summary>
        /// Called when the <see cref="Player" /> has the <see cref="Item" /> equipped.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that owns the <see cref="Item" />.</param>
        public override void Effects(Player p)
        {
            base.Effects(p);

            p.statLifeMax2 += 60;

            p.meleeDamage  += 0.05f;
            p.rangedDamage += 0.05f;
            p.magicDamage  += 0.05f;
            p.minionDamage += 0.05f;

            p.manaCost -= 0.1f;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="AdventuresAndMishaps" />.
    /// </summary>
    public sealed class AdventuresAndMishapsSkill : SkillManager
    {
        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public override int Cooldown
        {
            get
            {
                return 120;
            }
        }

        /// <summary>
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public override void Activate(Player p)
        {
            if (p.statMana < 20f * p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 20f * p.manaCost);

            for (double d = 0d; d < MathHelper.TwoPi; d += 0.2d)
                Main.projectile[ExtendedSpawning.NewProj(p.Centre,
                    new Vector2((float)Math.Cos(d) * -12f, (float)Math.Sin(d) * -12f), 14, (int)(25f * p.magicDamage), 1.1f, p.whoAmI)].timeLeft = 600;
        }
    }
}

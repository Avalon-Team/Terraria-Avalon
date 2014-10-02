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
    /// Mediation's Flame.
    /// </summary>
    [TomeSkill(typeof(MediationsFlameSkill))]
    public sealed class MediationsFlame : ModItem
    {
        /// <summary>
        /// Called when the <see cref="Player" /> has the <see cref="Item" /> equipped.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that owns the <see cref="Item" />.</param>
        public override void Effects(Player p)
        {
            base.Effects(p);

            p.statManaMax2 += 60;

            p.magicDamage += 0.05f;

            p.manaCost -= 0.1f;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="MediationsFlame" />.
    /// </summary>
    public sealed class MediationsFlameSkill : SkillManager
    {
        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public override int Cooldown
        {
            get
            {
                return 6;
            }
        }

        /// <summary>
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public override void Activate(Player p)
        {
            if (p.statMana < 16f * p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 16f * p.manaCost);

            double rot = p.AngleTo(Main.mouseWorld);

			ExtendedSpawning.NewProj(p.Centre, new Vector2((float)Math.Cos(rot) * 8f, (float)Math.Sin(rot) * 8f), 95, (int)(37f * p.magicDamage), 1.1f, p.whoAmI);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;
using Avalon.API.Items.MysticalTomes;

namespace Avalon.Items.MysticalTomes
{
    /// <summary>
    /// Flanker's Tome.
    /// </summary>
    [TomeSkill(typeof(FlankersTomeSkill))]
    public sealed class FlankersTome : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="FlankersTome" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public FlankersTome(ModBase @base, Item i)
            : base(@base, i)
        {

        }

        /// <summary>
        /// Called when the <see cref="Player" /> has the <see cref="Item" /> equipped.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that owns the <see cref="Item" />.</param>
        public override void Effects(Player p)
        {
            base.Effects(p);

            p.meleeDamage += 0.1f;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="FlankersTome" />.
    /// </summary>
    public sealed class FlankersTomeSkill : SkillManager
    {
        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public override int Cooldown
        {
            get
            {
                return 15;
            }
        }

        /// <summary>
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public override void Activate(Player p)
        {
            if (p.statMana < 15f * p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 15f * p.manaCost);

            for (double d = 0d; d < MathHelper.TwoPi; d += 0.3f)
                Main.projectile[Projectile.NewProjectile(p.Centre, new Vector2((float)Math.Cos(d) * -12f, (float)Math.Sin(d) * -12f),
                    Main.rand.Next(3) == 0 ? 48 : Main.rand.Next(5) != 0 ? 3 : 24, (int)(29f * p.magicDamage), 1.1f, p.whoAmI)].timeLeft = 600;
        }
    }
}

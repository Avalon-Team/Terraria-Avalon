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
    /// Tome of the River Spirit.
    /// </summary>
    [TomeSkill(typeof(TomeOfTheRiverSpiritSkill))]
    public sealed class TomeOfTheRiverSpirit : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TomeOfTheRiverSpirit" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public TomeOfTheRiverSpirit(ModBase @base, Item i)
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

            p.magicDamage += 0.15f;

            p.manaCost -= 0.05f;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="TomeOfTheRiverSpirit" />.
    /// </summary>
    public sealed class TomeOfTheRiverSpiritSkill : SkillManager
    {
        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public override int Cooldown
        {
            get
            {
                return 20;
            }
        }

        /// <summary>
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public override void Activate(Player p)
        {
            if (p.statMana < 10f * p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 10f * p.manaCost);

            for (int i = 0; i < 7; i++)
            {
                double d = Main.rand.Next((int)(Math.PI * 200d)) / 100d;
                Main.projectile[Projectile.NewProjectile(p.Centre, new Vector2((float)Math.Cos(d) * -12f, (float)Math.Sin(d) * -12f), 27, (int)(25f * p.magicDamage), 1.1f, 0)].timeLeft = 600;
            }
        }
    }
}

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
    /// Burning Desire.
    /// </summary>
    [TomeSkill(typeof(BurningDesireSkill))]
    public sealed class BurningDesire : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="BurningDesire" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public BurningDesire(ModBase @base, Item i)
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

            p.statLifeMax2 += 40;
            p.statManaMax2 += 40;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="BurningDesire" />.
    /// </summary>
    public sealed class BurningDesireSkill : SkillManager
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
            if (p.statMana < 3f * p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 3f * p.manaCost);

            double rot = p.AngleTo(Main.mouseWorld);

            Projectile.NewProjectile(p.Centre, new Vector2((float)Math.Cos(rot) * 10f, (float)Math.Sin(rot) * 10f), 85, (int)(32f * p.magicDamage), 1.1f, p.whoAmI);
        }
    }
}

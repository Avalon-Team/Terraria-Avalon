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
    /// Tale of the Dolt.
    /// </summary>
    [TomeSkill(typeof(TaleOfTheDoltSkill))]
    public sealed class TaleOfTheDolt : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TaleOfTheDolt" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public TaleOfTheDolt(ModBase @base, Item i)
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

            p.statManaMax2 += 20;
            p.statLifeMax2 += 20;

            p.meleeDamage += 0.05f;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="TaleOfTheDolt" />.
    /// </summary>
    public sealed class TaleOfTheDoltSkill : SkillManager
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
            if (p.direction == 1)
            {
                if (p.statMana > 20)
                {
                    p.statLife += 20;
                    p.statMana -= 20;
                    p.HealEffect(20);
                }
            }
            else if (p.statLife > 20)
            {
                p.statMana += 20;
                p.statLife -= 5 ;
                p.ManaEffect(20);
            }
        }
    }
}

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
    /// Love Up and Down.
    /// </summary>
    [TomeSkill(typeof(LoveUpAndDownSkill))]
    public sealed class LoveUpAndDown : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="LoveUpAndDown" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public LoveUpAndDown(ModBase @base, Item i)
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

            p.meleeDamage  += 0.15f;
            p.rangedDamage += 0.15f;
            p.magicDamage  += 0.15f;
            p.minionDamage += 0.15f;

            p.meleeCrit  += 7;
            p.rangedCrit += 7;
            p.magicCrit  += 7;

            p.manaCost -= 0.25f;

            p.ammoCost80 = true;

            p.statLifeMax2 += 80;
            p.statManaMax2 += 80;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="LoveUpAndDown" />.
    /// </summary>
    public sealed class LoveUpAndDownSkill : SkillManager
    {
        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public override int Cooldown
        {
            get
            {
                return 180;
            }
        }

        /// <summary>
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public override void Activate(Player p)
        {
            if (p.statMana < 30f* p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 30f * p.manaCost);

            if (Main.rand.Next(5) == 0 && (p.wet || p.lavaWet))
            {
                if (p.wet)
                    p.breath += 50;
                if (p.lavaWet)
                    p.AddBuff(1, 180);
            }
            else if (Main.rand.Next(2) == 0)
            {
                p.statMana += 130;
                p.ManaEffect(130);
            }
            else
            {
                p.statLife += 100;
                p.HealEffect(100);
            }
        }
    }
}

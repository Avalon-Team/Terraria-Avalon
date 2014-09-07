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
    /// Christmas Tome.
    /// </summary>
    [TomeSkill(typeof(ChristmasTomeSkill))]
    public sealed class ChristmasTome : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ChristmasTome" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public ChristmasTome(ModBase @base, Item i)
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

            p.meleeCrit  += 3;
            p.rangedCrit += 3;
            p.magicCrit  += 3;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="ChristmasTome" />.
    /// </summary>
    public sealed class ChristmasTomeSkill : SkillManager
    {
        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public override int Cooldown
        {
            get
            {
                return 7;
            }
        }

        /// <summary>
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public override void Activate(Player p)
        {
            if (p.statMana < 4f * p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 4f * p.manaCost);

            double rot = p.AngleTo(Main.mouseWorld);

            Projectile.NewProjectile(p.Centre, new Vector2((float)Math.Cos(rot) * -15f, (float)Math.Sin(rot) * -15f), 10, 35, 1.2f, p.whoAmI);
        }
    }
}

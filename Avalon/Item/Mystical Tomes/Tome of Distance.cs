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
    /// Tome of Distance.
    /// </summary>
    [TomeSkill(typeof(TomeOfDistanceSkill))]
    public sealed class TomeOfDistance : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TomeOfDistance" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public TomeOfDistance(ModBase @base, Item i)
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

            p.rangedDamage += 0.15f;

            p.ammoCost80 = true;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="TomeOfDistance" />.
    /// </summary>
    public sealed class TomeOfDistanceSkill : SkillManager
    {
        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public override int Cooldown
        {
            get
            {
                return 600;
            }
        }

        /// <summary>
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public override void Activate(Player p)
        {
            if (p.statMana < 100f * p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 100f * p.manaCost);

            Main.PlaySound(2, (int)p.position.X, (int)p.position.Y, 29);

            for (int i = 0; i < 70; i++)
                Dust.NewDust(p.position, p.width, p.height, 76, p.velocity.X / 2f, p.velocity.Y / 2f, 150, default(Color), 2.0f);

            p.Centre = Main.mouseWorld;

            for (int i = 0; i < 70; i++)
                Dust.NewDust(p.position, p.width, p.height, 66, 0f, 0f, 150, default(Color), 2f);
        }
    }
}

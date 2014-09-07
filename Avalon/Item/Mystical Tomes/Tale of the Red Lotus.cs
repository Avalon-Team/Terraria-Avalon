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
    /// Tale of the Red Lotus.
    /// </summary>
    [TomeSkill(typeof(TaleOfTheRedLotusSkill))]
    public sealed class TaleOfTheRedLotus : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TaleOfTheRedLotus" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public TaleOfTheRedLotus(ModBase @base, Item i)
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

            p.rangedDamage += 0.1f;

            p.statLifeMax2 += 20;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="TaleOfTheRedLotus" />.
    /// </summary>
    public sealed class TaleOfTheRedLotusSkill : SkillManager
    {
        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public override int Cooldown
        {
            get
            {
                return 40;
            }
        }

        /// <summary>
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public override void Activate(Player p)
        {
            if (p.statMana < 14f * p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 14f * p.manaCost);

            for (int i = 0; i < 13; i++)
            {
                double d = Main.rand.Next((int)(Math.PI * 200d)) / 100d;
                Main.projectile[Projectile.NewProjectile(p.Centre, new Vector2((float)Math.Cos(d) * -13f, (float)Math.Sin(d) * -13f), 45, (int)(35f * p.magicDamage), 1.1f, p.whoAmI)].timeLeft = 600;
            }
        }
    }
}

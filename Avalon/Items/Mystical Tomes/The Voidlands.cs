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
    /// The Voidlands.
    /// </summary>
    [TomeSkill(typeof(TheVoidlandsSkill))]
    public sealed class TheVoidlands : ModItem
    {
        /// <summary>
        /// Called when the <see cref="Player" /> has the <see cref="Item" /> equipped.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that owns the <see cref="Item" />.</param>
        public override void Effects(Player p)
        {
            base.Effects(p);

            p.statLifeMax2 += 60;

            p.meleeDamage  += 0.15f;
            p.rangedDamage += 0.15f;
            p.magicDamage  += 0.15f;
            p.minionDamage += 0.15f;

            p.meleeCrit  += 3;
            p.rangedCrit += 3;
            p.magicCrit  += 3;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="TheVoidlands" />.
    /// </summary>
    public sealed class TheVoidlandsSkill : SkillManager
    {
        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public override int Cooldown
        {
            get
            {
                return 1200;
            }
        }

        /// <summary>
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public override void Activate(Player p)
        {
            if (p.statMana < 200f * p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 200f * p.manaCost);

            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i] == null || !Main.npc[i].active || Main.npc[i].townNPC || Main.npc[i].friendly)
                    continue;

                Main.npc[i].StrikeNPC(50 + Main.npc[i].defense / 2, 5f, p.direction, true);
            }
        }
    }
}

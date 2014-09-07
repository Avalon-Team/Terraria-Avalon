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
    /// Misty Peach Blossoms.
    /// </summary>
    [TomeSkill(typeof(MistyPeachBlossomsSkill))]
    public sealed class MistyPeachBlossoms : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MistyPeachBlossoms" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public MistyPeachBlossoms(ModBase @base, Item i)
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
        }
    }

    /// <summary>
    /// The tome skill of <see cref="MistyPeachBlossoms" />.
    /// </summary>
    public sealed class MistyPeachBlossomsSkill : SkillManager
    {
        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public override int Cooldown
        {
            get
            {
                return 240;
            }
        }

        /// <summary>
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public override void Activate(Player p)
        {
            if (p.statMana < 40f * p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 40f * p.manaCost);

            for (int i = 0; i < 30; i++)
                Main.dust[Dust.NewDust(p.position, p.width, p.height, 3, p.velocity.X / 2f, p.velocity.Y / 2f, 150, default(Color), 2f)].noGravity = true;
            for (int i = 0; i < 500; i++)
                Main.dust[Dust.NewDust(Main.screenPosition, Main.screenWidth, Main.screenHeight, 3, p.velocity.X / 5f, p.velocity.Y / 5f, 150, default(Color), 1.2f)].noGravity = true;

            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i] == null || !Main.npc[i].active || Main.npc[i].townNPC || Main.npc[i].friendly ||
                        !Main.npc[i].Hitbox.Intersects(new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight)))
                    continue;

                Main.npc[i].AddBuff(20, 240);
                Main.npc[i].StrikeNPC(p.statMana % 30, 2f, p.direction, true);
            }
        }
    }
}

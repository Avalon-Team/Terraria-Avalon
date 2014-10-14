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
    /// Creator's Tome.
    /// </summary>
    [TomeSkill(typeof(CreatorsTomeSkill))]
    public sealed class CreatorsTome : ModItem
    {
        /// <summary>
        /// Called when the <see cref="Player" /> has the <see cref="Item" /> equipped.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that owns the <see cref="Item" />.</param>
        public override void Effects(Player p)
        {
            base.Effects(p);

            p.meleeDamage  += 0.2f;
            p.rangedDamage += 0.2f;
            p.magicDamage  += 0.2f;
            p.minionDamage += 0.2f;

            p.meleeCrit  += 5;
            p.rangedCrit += 5;
            p.magicCrit  += 5;

            p.manaCost -= 0.2f;

            p.ammoCost75 = true;

            p.statLifeMax2 += 100;
            p.statManaMax2 += 100;
        }
    }

    /// <summary>
    /// The tome skill of <see cref="CreatorsTome" />.
    /// </summary>
    public sealed class CreatorsTomeSkill : SkillManager
    {
        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public override int Cooldown
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public override void Activate(Player p)
        {
            if (p.statMana < 1f * p.manaCost)
                return;
            p.statMana = (int)(p.statMana - 1f * p.manaCost);

            int
                x = (int)(Main.mouseWorld.X / 16f),
                y = (int)(Main.mouseWorld.Y / 16f);

            if (Main.tile[x, y].active() && Main.tile[x, y].type == 59)
                Main.tile[x, y].type = 60;
            if (Main.tile[x, y].active() && Main.tile[x, y].type == 0)
                Main.tile[x, y].type = 2;

            WorldGen.SquareTileFrame(x, y, true);
            NetMessage.SendTileSquare(p.whoAmI, x, y, 3);
        }
    }
}

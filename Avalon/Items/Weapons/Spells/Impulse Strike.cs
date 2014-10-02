using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Items.Weapons.Spells
{
    /// <summary>
    /// The Impulse Strike.
    /// </summary>
    public sealed class ImpulseStrike : ModItem
    {
        /// <summary>
        /// When the <see cref="Player" /> is holding the <see cref="Item" />.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that is holding the <see cref="Item" />.</param>
        public override void HoldStyle(Player p)
        {
            base.HoldStyle(p);

            item.mana =
                // not implementing those yet
                //p.armor[0].type == Defs.items["Avalon:Berserker Headpiece"].type &&
                //p.armor[1].type == Defs.items["Avalon:Berserker Bodyarmor"].type &&
                //p.armor[2].type == Defs.items["Avalon:Berserker Cuisses"  ].type
                Main.rand.Next(2) == 0
                    ? 0 : (int)(17 * p.manaCost);
        }
    }
}

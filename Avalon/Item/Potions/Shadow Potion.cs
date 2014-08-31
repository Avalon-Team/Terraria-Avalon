using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Items.Potions
{
    /// <summary>
    /// The Shadow Potion.
    /// </summary>
    public sealed class ShadowPotion : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ShadowPotion" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public ShadowPotion(ModBase @base, Item i)
            : base(@base, i)
        {

        }

        /// <summary>
        /// When the <see cref="Player" /> uses the <see cref="Item" />.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that is using the <see cref="Item" />.</param>
        /// <returns>No idea at all.</returns>
        public override bool? UseItem(Player p)
        {
            p.AddBuff("Avalon:Shadows", 25200);

            return base.UseItem(p);
        }
    }
}

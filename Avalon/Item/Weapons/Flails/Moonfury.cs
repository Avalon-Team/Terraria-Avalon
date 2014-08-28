using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Items;

namespace Avalon.Items.Weapons.Flails
{
    /// <summary>
    /// The Moonfury.
    /// </summary>
    [ChainTexture("Moonfury Chain.png", ReplaceFlailChain = true)] // well this is easy
    public sealed class Moonfury : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Moonfury" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public Moonfury(ModBase @base, Item i)
            : base(@base, i)
        {

        }
    }
}

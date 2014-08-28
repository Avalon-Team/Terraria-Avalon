using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Items;

namespace Avalon.Items.Flails
{
    /// <summary>
    /// The Caesium Flail.
    /// </summary>
    [ChainTexture("Cursed Chain.png", ReplaceFlailChain = true)]
    public sealed class CaesiumFlail : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="CaesiumFlail" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public CaesiumFlail(ModBase @base, Item i)
            : base(@base, i)
        {

        }
    }
}

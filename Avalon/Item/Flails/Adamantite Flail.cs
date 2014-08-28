using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Items;

namespace Avalon.Items.Flails
{
    /// <summary>
    /// The Adamantite Flail
    /// </summary>
    [ChainTexture("Adamantite Chain.png", ReplaceFlailChain = true)] // well this is easy
    public sealed class AdamantiteFlail : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="AdamantiteFlail" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public AdamantiteFlail(ModBase @base, Item i)
            : base(@base, i)
        {

        }
    }
}

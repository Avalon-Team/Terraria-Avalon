using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Items;

namespace Avalon.Items.Flails
{
    /// <summary>
    /// The Silver Flail.
    /// </summary>
    [ChainTexture("Silver Chain.png", ReplaceFlailChain = true)]
    public sealed class SilverFlail : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SilverFlail" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public SilverFlail(ModBase @base, Item i)
            : base(@base, i)
        {

        }
    }
}

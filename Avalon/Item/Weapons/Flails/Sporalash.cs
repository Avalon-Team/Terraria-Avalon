using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Items;

namespace Avalon.Items.Weapons.Flails
{
    /// <summary>
    /// The Sporalash.
    /// </summary>
    [ChainTexture("Sporalash Chain.png", ReplaceFlailChain = true)]
    public sealed class Sporalash : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Sporalash" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public Sporalash(ModBase @base, Item i)
            : base(@base, i)
        {

        }
    }
}

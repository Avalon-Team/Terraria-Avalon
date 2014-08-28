using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;
using Avalon.API.Items;

namespace Avalon.Items.Weapons.Flails
{
    /// <summary>
    /// The Eclipse's Waning.
    /// </summary>
    [ChainTexture("Eclipse Chain.png", ReplaceFlailChain = true)]
    public sealed class EclipsesWaning : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="EclipsesWaning" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public EclipsesWaning(ModBase @base, Item i)
            : base(@base, i)
        {

        }
    }
}

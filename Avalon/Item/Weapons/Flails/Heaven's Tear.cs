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
    /// The Heaven's Tear.
    /// </summary>
    [ChainTexture("Heaven Chain.png", ReplaceFlailChain = true)]
    public sealed class HeavensTear : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="HeavensTear" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public HeavensTear(ModBase @base, Item i)
            : base(@base, i)
        {

        }
    }
}

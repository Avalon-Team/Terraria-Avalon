using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Items;

namespace Avalon.Items.Weapons.Flails
{
    /// <summary>
    /// The Meteor Masher.
    /// </summary>
    [ChainTexture("Meteor Chain.png", ReplaceFlailChain = true)]
    public sealed class MeteorMasher : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MeteorMasher" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public MeteorMasher(ModBase @base, Item i)
            : base(@base, i)
        {

        }
    }
}

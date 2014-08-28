using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Items;

namespace Avalon.Items.Weapons.Flails
{
    /// <summary>
    /// The Mythril Flail
    /// </summary>
    [ChainTexture("Mythril Chain.png", ReplaceFlailChain = true)]
    public sealed class MythrilFlail : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MythrilFlail" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public MythrilFlail(ModBase @base, Item i)
            : base(@base, i)
        {

        }
    }
}

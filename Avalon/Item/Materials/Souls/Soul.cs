using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Items.Materials.Souls
{
    /// <summary>
    /// A soul.
    /// </summary>
    public class Soul : ModItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Soul" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public Soul(ModBase @base, Item i)
            : base(@base, i)
        {

        }

        /// <summary>
        /// Called when the <see cref="Item" /> gets updated.
        /// </summary>
        /// <param name="gravity">The gravity strenght that works on the <see cref="Item" />.</param>
        /// <param name="maxVelocity">The maximum velocity of the <see cref="Item" /> (lenght of the velocity vector).</param>
        public override void OnUpdate(ref float gravity, ref float maxVelocity)
        {
            gravity = 0f;
            item.scale = Main.essScale;

            base.OnUpdate(ref gravity, ref maxVelocity);
        }
    }
}

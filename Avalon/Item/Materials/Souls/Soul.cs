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
		/// Called when the <see cref="Item" /> gets updated.
		/// </summary>
		/// <param name="gravity">The gravity strenght that works on the <see cref="Item" />.</param>
		/// <param name="maxVelocity">The maximum velocity of the <see cref="Item" /> (lenght of the velocity vector).</param>
		public override void MidUpdate(ref float gravity, ref float maxVelocity)
        {
            gravity = 0f;
            item.scale = Main.essScale;

            base.MidUpdate(ref gravity, ref maxVelocity);
        }
    }
}

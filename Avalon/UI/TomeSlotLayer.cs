using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TAPI;

namespace Avalon.UI
{
    /// <summary>
    /// The <see cref="InterfaceLayer" /> that contains the <see cref="MysticalTomeSlot" />.
    /// </summary>
    public sealed class TomeSlotLayer : InterfaceLayer
    {
        /// <summary>
        /// Gets the <see cref="TomeSlotLayer" /> singleton instance.
        /// </summary>
        public static TomeSlotLayer Instance
        {
            get;
            internal set;
        }

        MysticalTomeSlot slot;

        internal TomeSlotLayer()
            : base("Avalon:MysticalTomeSlotLayer")
        {
            Instance = this;

			slot = new MysticalTomeSlot()
			{
				position = new Vector2(Main.screenWidth - 186f, 100f)
			};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        protected override void OnDraw(SpriteBatch sb)
        {
            slot.Update(  );
			slot.Draw  (sb);
        }
    }
}

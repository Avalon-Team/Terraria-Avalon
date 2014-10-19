using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Items.Accessories
{
    /// <summary>
    /// The Bag of Blood.
    /// </summary>
	public sealed class BagOfBlood : ModItem
    {
        /// <summary>
        /// When the <see cref="Player" /> has the <see cref="Item" /> equipped.
        /// </summary>
        /// <param name="p"></param>
        public override void Effects(Player p) 
        {
            if (p.controlRight)
            {
                Main.dust[Dust.NewDust(p.position, p.width - 20, p.height, 5, 0, 0, 100, Color.White, 2f)].noGravity = true;
                Lighting.AddLight(p.Centre, 1f, 0f, 0f);
            }
            if (p.controlLeft)
            {
                Main.dust[Dust.NewDust(p.position, p.width + 20, p.height, 5, 0, 0, 100, Color.White, 2f)].noGravity = true;
                Lighting.AddLight(p.Centre, 1f, 0f, 0f);
            }
            if (p.controlJump)
            {
                Main.dust[Dust.NewDust(p.position, p.width + 20, p.height + 20, 5, 0, 0, 100, Color.White, 2f)].noGravity = true;
                Lighting.AddLight(p.Centre, 1f, 0f, 0f);
            }
        }
	}
}

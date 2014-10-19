using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Items.Armour.DarkMatter
{
    /// <summary>
    /// The Dark Matter Chest.
    /// </summary>
	public sealed class DarkMatterChest : ModItem
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public override void ArmorSetBonus(Player p)
        {
            Main.dust[Dust.NewDust(p.position, p.width, p.height, 27, 0, 0, 200, Color.Purple, 1.0f)].noGravity = true;

            p.setBonus = "The Dark Matter has spread";
            p.invis = true;
        }
	}
}

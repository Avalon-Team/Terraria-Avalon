using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.DevItems.Bullseye55
{
    /// <summary>
    /// Bullseye55's dev armour breastplate.
    /// </summary>
	public sealed class Bullseye55Breastplate : ModItem
	{
        //int mode = 0;
        //bool shift = false, shift2 = false;

        public override void ArmorSetBonus(Player p)
        {
            Main.dust[Dust.NewDust(p.position, p.width, p.height, 5, 0, 0, 200, Color.Red, 1.2f)].noGravity = false;

            p.setBonus = "Grants effects based on Bullseye55's preferences";
            p.lifeRegen = 555;
            p.maxMinions = 55;
            p.noFallDmg = true;
            p.statLifeMax2 += 500;
            p.moveSpeed *= 10;
            p.gravControl = true;
            p.gravControl2 = true;
            p.canRocket = true;
            p.rocketBoots = 1;
            p.rocketTime = p.rocketTime + 1;
        }
	}
}

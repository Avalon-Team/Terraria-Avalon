using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;
using Avalon.API.Items.DevItems;

namespace Avalon.Items.DevItems.Bullseye55
{
    /// <summary>
    /// Bullseye55's dev armour breastplate.
    /// </summary>
	public sealed class Bullseye55Breastplate : DevArmour
    {
        //int mode = 0;
        //bool shift = false, shift2 = false;

        /// <summary>
        /// Creates a new instance of the <see cref="Bullseye55Breastplate" /> class.
        /// </summary>
        public Bullseye55Breastplate()
            : base(DEV_DATA.Bullseye55)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public override void DevSetBonus(Player p)
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

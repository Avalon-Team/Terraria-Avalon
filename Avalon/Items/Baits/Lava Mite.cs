using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Items.Baits
{
	/// <summary>
	/// The Lava Mite.
	/// </summary>
	public sealed class LavaMite : ModItem
	{
		/// <summary>
		/// <!-- I don't feel like writing XmlDoc right now -->
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		public override bool CanUse(Player player)
		{
			NPC.NewNPC(Main.mouseX + (int)Main.screenPosition.X, Main.mouseY + (int)Main.screenPosition.Y, NPCDef.byName["Avalon:Lava Mite"].type);

			return true;
		}
	}
}

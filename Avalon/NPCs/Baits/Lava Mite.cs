using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.NPCs.Baits
{
	/// <summary>
	/// The Lava Mite.
	/// </summary>
	public sealed class LavaMite : ModNPC
	{
		/// <summary>
		/// Called after <see cref="NPC.AI" /> is called.
		/// </summary>
		public override void PostAI()
		{
			base.PostAI();

			// float in lava
			if (npc.lavaWet)
				npc.position.Y -= 0.025f;
		}

		/// <summary>
		/// Checks whether the spawning mechanism should spawn an <see cref="NPC" /> or not.
		/// </summary>
		/// <param name="x">The X position of the spawn place (in tiles).</param>
		/// <param name="y">The X position of the spawn place (in tiles).</param>
		/// <param name="type">The type of the <see cref="NPC" /> that might spawn.</param>
		/// <param name="spawnedOn">The <see cref="Player" /> the <see cref="NPC" /> might spawn on.</param>
		/// <returns>true if the <see cref="NPC" /> should spawn; otherwise, false.</returns>
		public override bool CanSpawn(int x, int y, int type, Player spawnedOn)
		{
			return Biome.Biomes["Hell"].Check(spawnedOn);// && Main.rand.Next(-1) == 0; // need a rarity value
		}
	}
}

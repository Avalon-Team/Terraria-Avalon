using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Buffs
{
    /// <summary>
    /// The Brain Follower buff.
    /// </summary>
    public class BrainFollower : ModBuff
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that has the buff.</param>
        /// <param name="index">The index of the buff.</param>
        public override void Effects(Player p, int index) 
		{
			bool spawned = false;

			for (int i = 0; i < 200; i++)
				if (Main.npc[i].active && Main.npc[i].type == NPCDef.byName["Avalon:Brain Follower"].type && Main.npc[i].ai[0] == p.whoAmI)
                {
                    spawned = true;

                    break;
                }

			if (!spawned)
			{
                NPC follower = Main.npc[NPC.NewNPC((int)p.position.X + p.width / 2, (int)p.position.Y + p.height / 2, NPCDef.byName["Avalon:Brain Follower"].type, 0)];

                follower.ai[0] = p.whoAmI;
                follower.netUpdate = true;

				if (Main.netMode == 2 && follower.whoAmI < 200)
					NetMessage.SendData(23, -1, -1, String.Empty, follower.whoAmI, 0f, 0f, 0f, 0);
			}
		}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.API.World
{
    /// <summary>
    /// A boss automatically spawning when the world is in superhardmode.
    /// </summary>
    public class BossSpawn
    {
        /// <summary>
        /// Gets or sets the type of the NPC to spawn.
        /// </summary>
        public int Type
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the type of the NPC to spawn by internal name.
        /// </summary>
        public string NpcInternalName
        {
            get
            {
                return Defs.npcNames[Type];
            }
            set
            {
                Type = Defs.npcs[value].type;
            }
        }
        /// <summary>
        /// Gets or sets wether the boss should spawn. ShouldSpawn() has a higher priority.
        /// </summary>
        public Func<int, Player, bool> CanSpawn
        {
            get;
            set;
        }

        /// <summary>
        /// Spawns the NPC on the specified player (by his/her ID).
        /// </summary>
        /// <param name="pid">The ID of the player to spawn the NPC on.</param>
        public virtual void Spawn(int pid)
        {
            NPC.SpawnOnPlayer(pid, Type);
            Main.PlaySound(15);
        }
        /// <summary>
        /// Gets wether the boss should spawn.
        /// </summary>
        /// <param name="rate">The spawn rate (a multiplier).</param>
        /// <param name="p">The player where to spawn the boss on.</param>
        /// <returns>true when it should, false otherwise.</returns>
        /// <remarks>It behaves as 'internalComputedStuff &amp; ShouldSpawn()'. The base method calls CanSpawn (if not null).</remarks>
        public virtual bool ShouldSpawn(int rate, Player p)
        {
            bool ret = true;

            if (CanSpawn != null)
                ret &= CanSpawn(rate, p);

            return ret;
        }
    }
}

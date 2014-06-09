using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Biomes;

namespace Avalon
{
    /// <summary>
    /// Global NPC stuff
    /// </summary>
    [GlobalMod]
    public class MNPC : ModNPC
    {
        /// <summary>
        /// Creates a new instance of the MNPC class
        /// </summary>
        /// <param name="base">The ModBase which belongs to the ModNPC instance</param>
        /// <param name="n">The NPC instance which is modified by the ModNPC</param>
        /// <remarks>Called by the mod loader</remarks>
        public MNPC(ModBase @base, NPC n)
            : base(@base, n)
        {

        }

        /// <summary>
        /// Updates the spawn rate for every player. Called every tick.
        /// </summary>
        /// <param name="player">The Player where the NPC(s) could spawn.</param>
        public override void UpdateSpawnRate(Player player)
        {
            base.UpdateSpawnRate(player);

            if (MWorld.InWraithInvasion)
            {
                NPC.spawnRate = 15;
                NPC.maxSpawns = 20;

                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i] == null || Main.npc[i].type <= 0 || Main.npc[i].life <= 0 || !Main.npc[i].active || String.IsNullOrEmpty(Main.npc[i].name))
                        continue;

                    if (Main.npc[i].townNPC || Main.npc[i].boss)
                        continue;

                    bool keepAlive = false;

                    object[] attr = Main.npc[i].subClass.GetType().GetCustomAttributes(typeof(WraithInvasionNPCAttribute), true);
                    for (int j = 0; j < attr.Length; j++)
                        if (attr[j] is WraithInvasionNPCAttribute)
                            keepAlive = true;

                    Main.npc[i].active &= keepAlive;
                }
            }
        }
    }
}

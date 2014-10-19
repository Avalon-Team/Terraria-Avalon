using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.Projectiles.FromPlayer.Spears
{
    /// <summary>
    /// The Black Lance.
    /// </summary>
    public sealed class BlackLance : ModProjectile
    {
        /// <summary>
        /// 
        /// </summary>
        public override void AI()
        {
            projectile.alpha = 0;

            for (int i = 0; i < Main.npc.Length; i++)
                if (Main.npc[i].type == NPCDef.byName["Avalon:Black Lancer"].type && Main.npc[i].position.X == projectile.position.X && Main.npc[i].position.Y == projectile.position.Y)
                {
                    projectile.position = Main.npc[i].position;
                    projectile.spriteDirection = Main.npc[i].spriteDirection;

                    if (!Main.npc[i].active)
                        projectile.active = false;
                }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="hitDir"></param>
        /// <param name="damage"></param>
        /// <param name="knockback"></param>
        /// <param name="crit"></param>
        /// <param name="critMult"></param>
        public override void DamageNPC(NPC npc, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult)
        {
            npc.AddBuff(80, 600, true);
        }
    }
}

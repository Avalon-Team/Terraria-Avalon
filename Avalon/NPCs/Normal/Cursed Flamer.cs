using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.NPCs.Normal
{
    /// <summary>
    /// The Cursed Flamer.
    /// </summary>
    public sealed class CursedFlamer : ModNPC
    {
        int fire = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="type"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool CanSpawn(int x, int y, int type, Player p)
        {
            return Main.player[Main.myPlayer].zoneEvil && Main.hardMode && Main.rand.Next(15) == 1 && !Main.dayTime;
        }
        /// <summary>
        /// 
        /// </summary>
        public override void AI()
        {
            npc.TargetClosest(true);

            if (++fire == 240)
            {
                const float spd = 10f;
                const int dmg = 52;

                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);

                float rotation = (float)Math.Atan2(npc.Centre.Y - Main.player[npc.target].Centre.Y, npc.Centre.X - Main.player[npc.target].Centre.X);

                Projectile p = Main.projectile[Projectile.NewProjectile(npc.Centre.X, npc.Centre.Y, (float)((Math.Cos(rotation) * spd) * -1), (float)((Math.Sin(rotation) * spd) * -1), 96, dmg, 2f, 255)];

                if (Main.rand.Next(5) == 1)
                    p.velocity *= 2;
                if (Main.rand.Next(5) == 1)
                    p.scale += 1;
                if (Main.rand.Next(5) == 1)
                    p.damage += 26;

                fire = 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hitDir"></param>
        /// <param name="damage"></param>
        /// <param name="knockback"></param>
        /// <param name="crit"></param>
        /// <param name="critMult"></param>
        public override void DamageNPC(Player p, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult) 
        {
            fire = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        public override void NPCLoot()
        {
            Gore.NewGore(npc.position, npc.velocity, 14, 1f);
            Gore.NewGore(npc.position, npc.velocity, 15, 1f);
        }
    }
}

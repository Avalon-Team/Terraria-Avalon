using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.NPCs.Normal
{
    /// <summary>
    /// The Crimper.
    /// </summary>
    public sealed class Crimper : ModNPC
    {
        int timer = 0;

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
            return Main.localPlayer.zoneBlood && Main.rand.Next(8) == 1 && Main.hardMode;
        }
        /// <summary>
        /// 
        /// </summary>
        public override void AI()
        {
            //float speed = 1.6f; // Change homing projectile speed.
            //npc.ai[0]++;
            //int p = Player.FindClosest(npc.position, npc.width, npc.height);
            //if (p != -1)
            //{
            //    Main.NewText("Before CanHit", 255, 255, 255);

            //    if (npc.ai[0] > 240 && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[p].position, Main.player[p].width, Main.player[p].height))
            //    {
            //        int proj = Projectile.NewProjectile(npc.Centre.X, npc.Centre.Y, npc.velocity.X, npc.velocity.Y, "Avalon:Blood Ball", npc.damage, 1, 255, 0f, 0f);
            //        Main.projectile[proj].velocity = (Main.player[p].Centre - npc.Centre) * speed; // Vector2.Normalize(npc.Centre, Main.player[P].Centre) * speed;
            //        Main.projectile[proj].ai[0] = p;
            //        npc.ai[0] = 0;
            //        Main.NewText("AFTER CanHit", 255, 255, 255);
            //    }
            //}

            timer++;
            npc.TargetClosest(true);

            if (timer == 125)
            {
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);

                float
                    spd = (float)(Main.rand.NextDouble() * 6.5f + 1.5f),
                    rot = (float)Math.Atan2(npc.Centre.Y - Main.player[npc.target].Centre.Y, npc.Centre.X - Main.player[npc.target].Centre.Y);

                Projectile.NewProjectile(npc.Centre.X, npc.Centre.Y, (float)Math.Cos(rot) * -spd, (float)Math.Sin(rot) * -spd, "Avalon:Blood Ball", npc.damage / 2, 1, 255, 0f, 0f);

                timer = 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override void NPCLoot()
        {
            Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Avalon:Crimper Gore 1"], 1f);
            Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Avalon:Crimper Gore 2"], 1f);
            Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Avalon:Crimper Gore 3"], 1f);
        }
    }
}

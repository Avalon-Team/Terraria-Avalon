using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.NPCs.Normal
{
    /// <summary>
    /// The Black Lancer.
    /// </summary>
    public sealed class BlackLancer : ModNPC
    {
        // Commented out for first testing version until the Hellcastle is implemented later down the road.
        //public override bool CanSpawn(int x, int y, int type, Player p)
        //{
        //    return Main.rand.Next(2) == 1;
        //}

        /// <summary>
        /// 
        /// </summary>
        public override void AI()
        {
            //lances++;
            //if (lances == 1)
            //{
            float Speed = 0f;
            Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
            int damage = 90;
            //Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 14);
            float rotation = (float)Math.Atan2(vector8.Y - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), vector8.X - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
            Projectile.NewProjectile(vector8.X, vector8.Y, (float)Math.Cos(rotation) * -Speed, (float)Math.Sin(rotation) * -Speed, "Avalon:Black Lance", damage, 0f, 0);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        public override void NPCLoot()
        {
            Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Avalon:Black Lancer Gore 1"], 1f);
            Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Avalon:Black Lancer Gore 2"], 1f);
            Gore.NewGore(npc.position, npc.velocity, GoreDef.gores["Avalon:Black Lancer Gore 3"], 1f);
        }
    }
}

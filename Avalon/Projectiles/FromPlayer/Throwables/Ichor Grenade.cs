using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Projectiles.FromPlayer.Throwables
{
    /// <summary>
    /// The Ichor Grenade.
    /// </summary>
    public sealed class IchorGrenade : ModProjectile
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Kill()
        {
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, ProjDef.byName["Avalon:Ichor Explosion"].type, 35, 0, projectile.whoAmI);
            Main.PlaySound(2, -1, -1, 38);

            Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 162, 0, 0, 100, new Color(), 1.3f)].noGravity = false;
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
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
            npc.AddBuff(69, 1800, true);
        }
    }
}

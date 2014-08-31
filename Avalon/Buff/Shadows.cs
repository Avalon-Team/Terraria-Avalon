using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using TAPI;

namespace Avalon.Buffs
{
    /// <summary>
    /// The Shadows buff.
    /// </summary>
    public sealed class Shadows : ModBuff
    {
        const int CD_MAX = 300;

        int cd = 0;

        /// <summary>
        /// Creates a new instance of the <see cref="Shadows" /> class.
        /// </summary>
        /// <param name="base">The <see cref="ModBase" /> that owns the Buff.</param>
        public Shadows(ModBase @base)
            : base(@base)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that has the buff.</param>
        /// <param name="index">The index of the buff.</param>
        public override void Effects(Player p, int index)
        {
            base.Effects(p, index);

            if (--cd < 0)
                cd = 0;

            if (Main.keyState.IsKeyDown(Keys.J) && Main.oldKeyState.IsKeyUp(Keys.J) && cd <= 0)
            {
                for (int i = 0; i < 10; i++)
                    Dust.NewDust(p.Hitbox, 15);

                p.position = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;

                Main.PlaySound(2, (int)p.position.X, (int)p.position.Y, 8);

                for (int i = 0; i < 10; i++)
                    Dust.NewDust(p.Hitbox, 15);

                cd = CD_MAX;
            }
        }
    }
}

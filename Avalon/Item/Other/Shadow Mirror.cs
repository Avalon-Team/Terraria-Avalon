using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using TAPI;
using PoroCYon.MCT.Net;

namespace Avalon.Items.Other
{
    /// <summary>
    /// The Shadow Mirror.
    /// </summary>
    public sealed class ShadowMirror : ModItem
    {
        /// <summary>
        /// All possible modes of the <see cref="ShadowMirror" />.
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// The <see cref="Player" />'s spawn point.
            /// </summary>
            Spawn,
            /// <summary>
            /// The Dungeon.
            /// </summary>
            Dungeon,
            /// <summary>
            /// The Jungle.
            /// </summary>
            Jungle,
            /// <summary>
            /// The left ocean.
            /// </summary>
            LeftOcean,
            /// <summary>
            /// The right ocean.
            /// </summary>
            RightOcean,
            /// <summary>
            /// The underworld.
            /// </summary>
            Underworld
        }

        const int
            TP_CD_MAX = 300,
            MODE_CD_MAX = 10;

        int tpCd = 0, modeCd = 0;

        /// <summary>
        /// Gets or sets the <see cref="Mode" /> of the <see cref="ShadowMirror" />.
        /// </summary>
        public Mode UseMode
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ShadowMirror" /> class.
        /// </summary>
        /// <param name="base">The mod that owns this item.</param>
        /// <param name="i">The <see cref="Item" /> to attach the <see cref="ModItem" /> to.</param>
        public ShadowMirror(ModBase @base, Item i)
            : base(@base, i)
        {

        }

        /// <summary>
        /// Gets where the <see cref="Player" /> should teleport to in the given <see cref="Mode" />.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that will teleport.</param>
        /// <param name="mode">The teleport mode.</param>
        /// <returns>The location where the <see cref="Player" /> will teleport to.</returns>
        public static Vector2 PositionOf(Player p, Mode mode)
        {
            switch (mode)
            {
                case Mode.Dungeon:
                    return new Vector2(Main.dungeonX, Main.dungeonY - 3f) * 16f;
                case Mode.Jungle:
                    return new Vector2(MWorld.JunglePosition.X, MWorld.JunglePosition.Y) * 16f;
                case Mode.LeftOcean:
                    return new Vector2(MWorld.LeftOceanPosition.X , MWorld.LeftOceanPosition.Y ) * 16f;
                case Mode.RightOcean:
                    return new Vector2(MWorld.RightOceanPosition.X, MWorld.RightOceanPosition.Y) * 16f;
                case Mode.Spawn:
                    return new Vector2(p.SpawnX, p.SpawnY) * 16f;
                case Mode.Underworld:
                    return new Vector2(MWorld.UnderworldPosition.X, MWorld.UnderworldPosition.Y) * 16f;
            }

            throw new ArgumentOutOfRangeException("mode", "Invalid value " + mode);
        }

        /// <summary>
        /// When the <see cref="Player" /> uses the <see cref="Item" />.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that is using the <see cref="Item" />.</param>
        /// <returns>No idea at all.</returns>
        public override bool? UseItem(Player p)
        {
            p.position = PositionOf(p, UseMode);

            if (Main.netMode == 1 && p.whoAmI == Main.myPlayer)
                NetHelper.SendModData(Mod.Instance, NetMessages.RequestTiles, p.whoAmI,
                    (int)(p.position.X / 16f), (int)(p.position.Y / 16f), Math.Max(Main.screenWidth, Main.screenHeight) * 2); // usually screenWidth is greater, but you never know..

            return base.UseItem(p);
        }
        /// <summary>
        /// When the <see cref="Player" /> is holding the <see cref="Item" />.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that is holding the <see cref="Item" />.</param>
        public override void HoldStyle(Player p)
        {
            base.HoldStyle(p);

            if (--tpCd < 0)
                tpCd = 0;
            if (--modeCd < 0)
                modeCd = 0;

            if (Main.mouseRight && Main.mouseRightRelease && modeCd <= 0) 
            {
                UseMode++;

                if (UseMode > Mode.Underworld /* 6 */)
                    UseMode = 0;

                Main.NewText("Mode changed: " + UseMode);

                modeCd = MODE_CD_MAX;
            }

            if (Main.keyState.IsKeyDown(Keys.J) && Main.oldKeyState.IsKeyUp(Keys.J) && tpCd <= 0) // TODO: mod settings?
            {
                for (int i = 0; i < 10; i++)
                    Dust.NewDust(p.Hitbox, 15);

                p.position = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;

                Main.PlaySound(2, (int)p.position.X, (int)p.position.Y, 8);

                for (int i = 0; i < 10; i++)
                    Dust.NewDust(p.Hitbox, 15);

                tpCd = TP_CD_MAX;
            }
        }

        /// <summary>
        /// When the <see cref="Player" /> has the <see cref="Item" /> equipped.
        /// </summary>
        /// <param name="p"></param>
        public override void Effects(Player p)
        {
            base.Effects(p);

            if (Main.keyState.IsKeyDown(Keys.J) && Main.oldKeyState.IsKeyUp(Keys.J) && tpCd <= 0) // TODO: mod settings?
            {
                for (int i = 0; i < 10; i++)
                    Dust.NewDust(p.Hitbox, 15);

                p.position = new Vector2(Main.mouseX, Main.mouseY) + Main.screenPosition;

                Main.PlaySound(2, (int)p.position.X, (int)p.position.Y, 8);

                for (int i = 0; i < 10; i++)
                    Dust.NewDust(p.Hitbox, 15);
            }
        }
    }
}

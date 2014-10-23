using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;

namespace Avalon.Tiles.CraftingStations
{
    /// <summary>
    /// The Hallowed Altar.
    /// </summary>
    public sealed class HallowedAltar : ModTile
    {
        readonly static Vector2[] possOffs = { new Vector2(20f, 12f), new Vector2(12f, 20f) };
        readonly static Vector2[] possVels = { new Vector2(1.6f), new Vector2(-1.6f), new Vector2(1.6f, -1.6f), new Vector2(-1.6f, 1.6f) };

        // need the candestorytile, canexplode and candestoryaround hooks

        /// <summary>
        /// Called when the <see cref="Tile" /> is mined, blown up, etc.
        /// </summary>
        /// <param name="x">The X position of the <see cref="Tile" />.</param>
        /// <param name="y">The Y position of the <see cref="Tile" />.</param>
        /// <param name="fail">Wether it succeeded to kill the <see cref="Tile" /> or not. If false, the <see cref="Tile" /> is still active.</param>
        /// <param name="effectsOnly">A flag used to mark that only dust should be spawned.</param>
        /// <param name="noItem">Not sure.</param>
        public override void Kill(int x, int y, bool fail, bool effectsOnly, bool noItem)
        {
            base.Kill(x, y, fail, effectsOnly, noItem);

            if (!fail && !effectsOnly)
                MWorld.SmashHallowAltar(x, y);
        }
        
        /// <summary>
        /// Updates the <see cref="Tile" />.
        /// </summary>
        public override void Update()
        {
            base.Update();

            if (!Main.tile[position.X, position.Y].active() || Main.tile[position.X, position.Y].type != TileDef.byName["Avalon:Hallowed Altar"])
                return;

            Vector2 vel = possVels[Main.rand.Next(possVels.Length)];
            Main.dust[Dust.NewDust(worldPos + possOffs[Main.rand.Next(possOffs.Length)], 4, 4, 57, vel.X, vel.Y, 30, Color.White, 1.8f)].noGravity = true;
        }
    }
}

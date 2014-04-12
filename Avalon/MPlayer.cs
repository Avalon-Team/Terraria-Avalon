using System;
using System.Collections.Generic;
using System.Linq;
using TAPI;

namespace Avalon
{
    /// <summary>
    /// Global player stuff
    /// </summary>
    public sealed class MPlayer : ModPlayer
    {
        /// <summary>
        /// Creates a new instance of the MPlayer class
        /// </summary>
        /// <param name="@base">The ModBase which belongs to the ModPlayer instance</param>
        /// <param name="p">The Player instance which is modified by the ModPlayer</param>
        /// <remarks>Called by the mod loader</remarks>
        public MPlayer(ModBase @base, Player p)
            : base(@base, p)
        {

        }

        /// <summary>
        /// Called when the Player spawns for the first time in the world
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();


        }

        /// <summary>
        /// Called when a Player is loaded
        /// </summary>
        /// <param name="bb">A binary buffer which contains save data</param>
        public override void Load(BinBuffer bb)
        {
            base.Load(bb);


        }
        /// <summary>
        /// Called when a Player is saved
        /// </summary>
        /// <param name="bb">A binary buffer where to put save data in</param>
        public override void Save(BinBuffer bb)
        {
            base.Save(bb);


        }

        /// <summary>
        /// Called when the Player is updated
        /// </summary>
        public override void OnUpdate()
        {
            base.OnUpdate();


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TAPI;
using PoroCYon.MCT;

namespace Avalon
{
    /// <summary>
    /// Like 'Program' but for a mod
    /// </summary>
    public sealed class Mod : ModBase
    {
        /// <summary>
        /// Creates a new instance of the Mod class
        /// </summary>
        /// <remarks>Called by the mod loader</remarks>
        public Mod()
			: base()
        {

        }

        /// <summary>
        /// Called when the mod is loaded
        /// </summary>
		public override void OnLoad()
		{
			// initializes most of the MCT features
			Mct.Init();

			base.OnLoad();
		}
    }
}

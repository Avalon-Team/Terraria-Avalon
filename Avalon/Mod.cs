using System;
using TAPI.SDK;

namespace TAPI.Avalon
{
    /// <summary>
    /// Like 'Program' but for a mod
    /// </summary>
    public sealed class Mod : ModBase
    {
        /// <summary>
        /// Called by the mod loader through reflection
        /// </summary>
        public Mod()
			: base()
        {

        }

        /// <summary>
        /// Called when the mod is loaded
        /// </summary>
		public override void OnLoad()
		{
			// initializes most of the SDK features
			Sdk.Init();

			base.OnLoad();
		}
    }
}

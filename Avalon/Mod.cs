using System;
using TAPI.SDK;

namespace TAPI.Avalon
{
    /// <summary>
    /// Like 'Program' but for a mod
    /// </summary>
    public sealed class Mod : ModBase
    {
        public Mod()
			: base()
        {

        }

		public override void OnLoad()
		{
			// initializes most of the SDK features
			Sdk.Init();

			base.OnLoad();
		}
    }
}

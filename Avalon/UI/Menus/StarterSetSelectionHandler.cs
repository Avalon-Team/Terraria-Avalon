using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TAPI;
using Avalon.API.StarterSets;

namespace Avalon.UI.Menus
{
	static class StarterSetSelectionHandler
	{
		readonly static string STARTER_SET = "Starter set: ";

		readonly static Action EmptyAction = () => { };

		static MenuButtonHorizontalSlider slider = new MenuButtonHorizontalSlider(new Vector2(0.2f, 0f), new Vector2(0f, 200f),
			String.Empty, String.Empty, String.Empty, EmptyAction).SetSize(200f, 20f).With(mbhs =>
		{
			mbhs.segments   = StarterSet.Sets.Length;

			mbhs.Update += () =>
			{
				mbhs.offset.X = -mbhs.size.X / 2f;

				mbhs.segments = StarterSet.Sets.Length;
			};

			mbhs.Click += () =>
			{
				if (mbhs.slider_hover <= -1)
					return;

				text.displayText = STARTER_SET + StarterSet.Names[StarterSet.SelectedSet = mbhs.slider = mbhs.slider_hover];
            };
			mbhs.ClickHold += mbhs.Click;
		});
		static MenuButton text = new MenuButton(new Vector2(0.2f, 0f), new Vector2(0f, 150f), STARTER_SET + StarterSet.Names[0],
			String.Empty, String.Empty, EmptyAction).SetSize(350f, 40f).With(mb =>
		{
			//mb.disabled = true;

			mb.Update    = () => mb.offset.X = -mb.size.X / 2f;
			mb.Click     = EmptyAction;
			mb.ClickHold = mb.Click;
		});

		static StarterSetDisplayer displayer = (StarterSetDisplayer)new StarterSetDisplayer(new Vector2(0.2f, 0f), new Vector2(20f, 250f),
			String.Empty, String.Empty, String.Empty, EmptyAction).With(mb =>
		{
			mb.Update = () => mb.offset.X = -mb.size.X / 2f;
		});

		internal static void Init()
		{
			Menu.menuPages["Create Player"].buttons.Add(slider   );
			Menu.menuPages["Create Player"].buttons.Add(text     );
			Menu.menuPages["Create Player"].buttons.Add(displayer);

			Menu.menuPages["Create Player"].OnEntry += () => StarterSet.SelectedSet = 0;
        }
	}
}

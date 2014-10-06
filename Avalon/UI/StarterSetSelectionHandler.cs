using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TAPI;
using Avalon.API.StarterSets;

namespace Avalon.UI
{
	static class StarterSetSelectionHandler
	{
		readonly static string STARTER_SET = "Starter set:";

		readonly static Action EmptyAction = () => { };

		static MenuButtonHorizontalSlider slider = new MenuButtonHorizontalSlider(new Vector2(0.5f, 0f), new Vector2(0f, 150f), "", String.Empty, String.Empty, EmptyAction).SetSize(200f, 20f).With(mbhs =>
		{
			mbhs.multislide = StarterSet.Sets.Length;
			mbhs.segments   = StarterSet.Sets.Length;

			mbhs.Update += () =>
			{
				mbhs.offset.X = -mbhs.size.X / 2f;

				mbhs.slider   = StarterSet.Sets.Length;
				mbhs.segments = StarterSet.Sets.Length;

				//mbhs.disabled = mbhs.segments < mbhs.multislide + 1;
			};

			mbhs.Click += () =>
			{
				if (mbhs.slider_hover <= -1)
					return;

				text.displayText = STARTER_SET + StarterSet.Names[StarterSet.SelectedSet = mbhs.slider_hover];
            };
			mbhs.ClickHold += mbhs.Click;
		});
		static MenuButton text = new MenuButton(new Vector2(0.5f, 0f), new Vector2(0f, 100f), STARTER_SET, String.Empty, String.Empty, EmptyAction).With(mb =>
		{
			//mb.disabled = true;

			mb.Update += () =>
			{
				mb.offset.X = -mb.size.X / 2f;
			};
			mb.Click = EmptyAction;
			mb.ClickHold = mb.Click;
		});

		internal static void Init()
		{
			Menu.menuPages["Create Player"].buttons.Add(slider);
			Menu.menuPages["Create Player"].buttons.Add(text  );
		}
	}
}

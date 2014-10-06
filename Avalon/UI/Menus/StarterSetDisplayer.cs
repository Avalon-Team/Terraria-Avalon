using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TAPI;
using Avalon.API.StarterSets;

namespace Avalon.UI.Menus
{
	/// <summary>
	/// A <see cref="MenuButton" /> that displays a <see cref="StarterSet" />.
	/// </summary>
	public class StarterSetDisplayer : MenuButton
	{
		const float ARMOUR_OFFSET = 70f;

#pragma warning disable 1591
		public StarterSetDisplayer(int pageAnchor, string display, string to, string desc = "", Action cl = null, Action clh = null)
			: base(pageAnchor, display, to, desc, cl, clh)
		{

		}
		public StarterSetDisplayer(Vector2 anchor, Vector2 offset, string display, string to, string desc = "", Action cl = null, Action clh = null)
			: base(anchor, offset, display, to, desc, cl, clh)
		{

		}

		// these two methods are probably the hardest ones to debug in the entire mod. I HATE red's colour system.
		static Color ComposeColour(Item item, Color? tint = null)
		{
			Color c;

			c = item.color == default(Color) ? item.GetAlpha(tint ?? Color.White) : item.GetAlpha(item.GetColor(tint ?? Color.White));

			//c.A = 255;

			return c;
		}
		static void DrawItem(Item item, SpriteBatch sb, Vector2 position, Color? tint = null)
		{
			sb.Draw(item.GetTexture(), position, item.GetAlpha(tint ?? Color.White));

			if (item.color != default(Color))
				sb.Draw(item.GetTexture(), position, ComposeColour(item, tint));
		}

		public override bool MouseOver(Vector2 mouse)
		{
			return false;
		}

		public override void Draw(SpriteBatch sb, bool mouseOver)
		{
			//base.Draw(sb, mouseOver);

			StarterSet set = StarterSet.Sets[StarterSet.SelectedSet];

			Item drawItem = new Item();

			for (int i = 0; i < set.Items.Length; i++)
			{
				drawItem.netDefaults(set.Items[i]);

				DrawItem(drawItem, sb, position + new Vector2(0f, i * (drawItem.GetTexture().Height + 5f)));
				//sb.Draw(drawItem.GetTexture(), position + new Vector2(0f, i * (drawItem.GetTexture().Height + 5f)), ComposeColour(drawItem));
			}

			if (set.ArmourHead != 0)
			{
				drawItem.netDefaults(set.ArmourHead);

				DrawItem(drawItem, sb, position + new Vector2(ARMOUR_OFFSET, 0f));
				//sb.Draw(drawItem.GetTexture(), position + new Vector2(ARMOUR_OFFSET, 0f                                     ), ComposeColour(drawItem));
			}
			if (set.ArmourBody != 0)
			{
				drawItem.netDefaults(set.ArmourBody);

				DrawItem(drawItem, sb, position + new Vector2(ARMOUR_OFFSET, 2f * drawItem.GetTexture().Height + 5f));
				//sb.Draw(drawItem.GetTexture(), position + new Vector2(ARMOUR_OFFSET, 2f * drawItem.GetTexture().Height +  5f), ComposeColour(drawItem));
			}
			if (set.ArmourLegs != 0)
			{
				drawItem.netDefaults(set.ArmourLegs);

				DrawItem(drawItem, sb, position + new Vector2(ARMOUR_OFFSET, 4f * drawItem.GetTexture().Height + 10f));
				//sb.Draw(drawItem.GetTexture(), position + new Vector2(ARMOUR_OFFSET, 4f * drawItem.GetTexture().Height + 10f), ComposeColour(drawItem));
			}
		}
#pragma warning restore 1591
	}
}

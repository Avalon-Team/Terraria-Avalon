using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace Avalon.API.StarterSets
{
	/// <summary>
	/// A starter set - a set of <see cref="Item" />s a <see cref="Player" /> has when created.
	/// </summary>
	public struct StarterSet
	{
		/// <summary>
		/// All available <see cref="StarterSet" />s.
		/// </summary>
		public readonly static StarterSet[] Sets =
		{
			// basic
			new StarterSet(new[] { -15, -13, -16      },   0,   0,   0),
			// basic with hammer
			new StarterSet(new[] { -15, -13, -16, -17 },   0,   0,   0),
			// basic with hammer and wood armour
			new StarterSet(new[] { -15, -13, -16, -17 }, 727, 728, 729),

			// copper
			new StarterSet(new[] { -15, -13, -16, -17 },  89,  80,  76),
			// tin
			new StarterSet(new[] { -15, -13, -16, -17 }, 687, 688, 689),
			// iron
			new StarterSet(new[] {   6,   1,  10,   7 },  90,  81,  77),
			// lead
			new StarterSet(new[] { -33, -31, -34, -35 }, 690, 691, 692),
			// silver
			new StarterSet(new[] { - 9, - 7, -10, -11 },  91,  82,  78),
			// tungsten
			new StarterSet(new[] { -39, -37, -40, -41 }, 693, 694, 695),
			// gold
			new StarterSet(new[] {  -3,  -1,  -4,  -5 },  92,  83,  74),
			// platinum
			new StarterSet(new[] { -45, -43, -46, -47 }, 696, 697, 698),
		};
		/// <summary>
		/// All <see cref="StarterSet" />'s names.
		/// </summary>
		public readonly static string[] Names =
		{
			"Basic", "Basic with Hammer",
			"basic wiht Hammer and Wood Armour",

			"Copper", "Tin",
			"Iron", "Lead",
			"Silver", "Tungsten",
			"Gold", "Platinum"
		};

		internal static int SelectedSet = 0;

		/// <summary>
		/// The type of the <see cref="Item" />s the <see cref="Player" /> initially has in his/her inventory.
		/// </summary>
		public int[] Items;

		/// <summary>
		/// The type of the helmet <see cref="Item" /> the <see cref="Player" /> will start with.
		/// </summary>
		public int ArmourHead;
		/// <summary>
		/// The type of the chainmail <see cref="Item" /> the <see cref="Player" /> will start with.
		/// </summary>
		public int ArmourBody;
		/// <summary>
		/// The type of the greaves <see cref="Item" /> the <see cref="Player" /> will start with.
		/// </summary>
		public int ArmourLegs;

		/// <summary>
		/// Creates a new instance of the <see cref="StarterSet" /> class.
		/// </summary>
		/// <param name="items"><see cref="Items" /></param>
		/// <param name="head"><see cref="ArmourHead" /></param>
		/// <param name="body"><see cref="ArmourBody" /></param>
		/// <param name="legs"><see cref="ArmourLegs" /></param>
		public StarterSet(int[] items, int head, int body, int legs)
		{
			Items      = items;
			ArmourHead = head ;
			ArmourBody = body ;
			ArmourLegs = legs ;
		}
	}
}

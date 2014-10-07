using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.NPCs
{
    /// <summary>
    /// A structure used to 
    /// </summary>
    public struct VanillaDrop
    {
        /// <summary>
        /// Gets the dictionary of vanilla drops.
        /// </summary>
        public static Dictionary<int, VanillaDrop> Drops
        {
            get;
            private set;
        }

        /// <summary>
        /// The type of the <see cref="Item" /> to drop.
        /// </summary>
        public int Type;
        /// <summary>
        /// The chance (in percents) of the <see cref="Item" /> to be dropped.
        /// </summary>
        public float Chance;
        /// <summary>
        /// The amount of <see cref="Item" />s that will be dropped.
        /// </summary>
        public int Amount;

        /// <summary>
        /// Creates a new instance of the <see cref="VanillaDrop" /> structure.
        /// </summary>
        /// <param name="type"><see cref="Type" /></param>
        public VanillaDrop(int type)
            : this(type, 1f, 1)
        {

        }
        /// <summary>
        /// Creates a new instance of the <see cref="VanillaDrop" /> structure.
        /// </summary>
        /// <param name="type"><see cref="Type" /></param>
        /// <param name="chance"><see cref="Chance" /></param>
        public VanillaDrop(int type, float chance)
            : this(type, chance, 1)
        {

        }
        /// <summary>
        /// Creates a new instance of the <see cref="VanillaDrop" /> structure.
        /// </summary>
        /// <param name="type"><see cref="Type" /></param>
        /// <param name="chance"><see cref="Chance" /></param>
        /// <param name="amt"><see cref="Amount" /></param>
        public VanillaDrop(int type, float chance, int amt)
        {
            Type   = type;
            Chance = chance;
            Amount = amt;
        }

        internal static void InitDrops()
        {
            Drops = new Dictionary<int, VanillaDrop>()
            {
                {
                    ItemDef.byName["Vanilla:Paladin"].type,
                    new VanillaDrop(ItemDef.byName["AvalonMod:Throwing Paladin's Hammer"].type, 0.01f, 1)
                }
            };
        }
    }
}

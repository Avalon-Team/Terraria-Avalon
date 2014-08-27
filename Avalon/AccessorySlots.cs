using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TAPI;

namespace Avalon
{
    class AccessorySlots : InterfaceLayer
    {
        static AccessorySlots instance;

        public sealed class Items
        {
            // ha. ha. ha.
            internal Items() { }

            public Item this[int index]
            {
                get
                {
                    return MWorld.accessories[Main.myPlayer][index];
                }
                set
                {
                    MWorld.accessories[Main.myPlayer][index] = value;
                    // instance.slots[index].ContainedItem = value; // pointers
                }
            }
        }
        internal static Items Slots
        {
            get;
            private set;
        }

        Interface.ItemSlot[] slots = new Interface.ItemSlot[Mod.ExtraSlots];
        Item[] items = new Item[Mod.ExtraSlots];

        internal AccessorySlots()
            : base("Avalon:AccessorySlots")
        {
            instance = this;

            for (int i = 0; i < slots.Length; i++)
                slots[i] = new Interface.ItemSlot(Mod.Instance, "Avalon:ExtraAccSlot", i,
                    (s, it) => MWorld.accessories[Main.myPlayer][s.index] = it,
                     s      => MWorld.accessories[Main.myPlayer][s.index]);

            Slots = new Items();
        }

        protected override void OnDraw(SpriteBatch sb)
        {
            for (int i = 0; i < slots.Length; i++)
                slots[i].UpdateAndDraw(sb);
        }
    }
}

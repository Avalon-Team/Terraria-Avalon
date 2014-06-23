using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using PoroCYon.MCT.UI.Interface;
using PoroCYon.MCT.UI.Interface.Controls;

namespace Avalon
{
    class AccessorySlots : CustomUI
    {
        static AccessorySlots instance;

        public class Items
        {
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

        ItemContainer[] slots = new ItemContainer[Mod.ExtraSlots];

        internal AccessorySlots()
            : base()
        {
            instance = this;

            for (int i = 0; i < slots.Length; i++)
                slots[i] = new ItemContainer(MWorld.accessories[Main.myPlayer][i], Mod.Instance, i, "Avalon:ExtraAccSlot") { InventoryBackTextureNum = 3 };

            Slots = new Items();
        }

        public override void Init()
        {
            base.Init();

            for (int i = 0; i < slots.Length; i++)
                AddControl(slots[i]);
        }

        internal static LayerUI GetNewLayer()
        {
            return new LayerUI(new AccessorySlots(), "Avalon:AccessorySlots");
        }
    }
}

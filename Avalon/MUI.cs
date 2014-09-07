using System;
using System.Collections.Generic;
using System.Linq;
using TAPI;
using Avalon.UI;

namespace Avalon
{
    /// <summary>
    /// The <see cref="ModInterface" /> class of the Avalon mod.
    /// </summary>
    public sealed class MUI : ModInterface
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MUI" /> class.
        /// </summary>
        /// <param name="base">The <see cref="ModBase" /> that owns this <see cref="ModInterface" />.</param>
        public MUI(ModBase @base)
            : base(@base)
        {

        }

        /// <summary>
        /// Modifies the given interface layer list.
        /// </summary>
        /// <param name="list">The interface layer list to modify.</param>
        public override void ModifyInterfaceLayerList(List<InterfaceLayer> list)
        {
            base.ModifyInterfaceLayerList(list);

            InterfaceLayer.Add(list, MWorld.AccessoryLayer = new AccessorySlotLayer(), InterfaceLayer.LayerInventory, false);
            InterfaceLayer.Add(list, MWorld.TomeSlotLayer  = new TomeSlotLayer     (),         MWorld.AccessoryLayer, false);
        }
    }
}

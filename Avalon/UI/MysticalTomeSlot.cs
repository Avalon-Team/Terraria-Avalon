using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Items.MysticalTomes;

namespace Avalon.UI
{
    /// <summary>
    /// An item slot for a <see cref="SkillManager" />.
    /// </summary>
    public class MysticalTomeSlot : Interface.ItemSlot
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MysticalTomeSlot" /> class.
        /// </summary>
        public MysticalTomeSlot()
            : base(Mod.Instance, "Avalon:MysticalTomeSlot", 0, (s, i) => MWorld.localManager = SkillManager.FromItem(MWorld.localTome = i), s => MWorld.localTome)
        {

        }

        /// <summary>
        /// Gets whether the <see cref="Interface.ItemSlot" /> allows the given <see cref="Item" /> or not.
        /// </summary>
        /// <param name="it">The <see cref="Item" /> to check.</param>
        /// <returns>true if the <see cref="Item" /> can be placed in the <see cref="Interface.ItemSlot" />, false otherwise.</returns>
        public override bool AllowsItem(Item it)
        {
            TomeSkillAttribute attr = null;

            for (int i = 0; i < it.allSubClasses.Length; i++)
                attr = it.allSubClasses[i].GetType().GetCustomAttributes(typeof(TomeSkillAttribute), true)
                    .FirstOrDefault() as TomeSkillAttribute; // there should be only one

            return base.AllowsItem(it) && attr != null;
        }
    }
}

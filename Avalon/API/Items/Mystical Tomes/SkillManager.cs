using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace Avalon.API.Items.MysticalTomes
{
    /// <summary>
    /// A skill manager.
    /// </summary>
    public abstract class SkillManager
    {
        internal WeakReference item_wr;

        /// <summary>
        /// Gets the tome <see cref="Terraria.Item" /> representation.
        /// </summary>
        protected Item Item
        {
            get
            {
                if (item_wr == null || !item_wr.IsAlive)
                    throw new ObjectDisposedException("Item");

                Item i = item_wr.Target as Item;

                if (i == null)
                    throw new ObjectDisposedException("Item");

                return i;
            }
        }

        /// <summary>
        /// Gets the cooldown of the skill, in ticks.
        /// </summary>
        public abstract int Cooldown
        {
            get;
        }

        /// <summary>
        /// Activates the tome.
        /// </summary>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public abstract void Activate(); // oh noes!

        internal static SkillManager FromItem(Item it)
        {
            TomeSkillAttribute attr = null;

            for (int i = 0; i < it.allSubClasses.Length; i++)
                attr = it.allSubClasses[i].GetType().GetCustomAttributes(typeof(TomeSkillAttribute), true)
                    .FirstOrDefault() as TomeSkillAttribute; // there should be only one

            if (attr == null)
                return null;

            if (attr == null)
                return null;

            return attr.Instantiate();
        }
    }
}

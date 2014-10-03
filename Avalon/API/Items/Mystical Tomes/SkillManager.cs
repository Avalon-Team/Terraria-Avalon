using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

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
        /// Activates the tome skill.
        /// </summary>
        /// <param name="p">The <see cref="Player" /> that activated the skill.</param>
        /// <remarks>No NetMessages have to be sent, this method is called on all clients and the server.</remarks>
        public abstract void Activate(Player p); // oh noes!

        internal static SkillManager FromItem(Item it)
        {
            TomeSkillAttribute attr = null;

            for (int i = 0; i < it.modEntities.Count; i++)
				if ((attr = it.modEntities[i].GetType().GetCustomAttributes(typeof(TomeSkillAttribute), true)
						.FirstOrDefault() as TomeSkillAttribute) != null) // there should be only one
					return attr.Instantiate();

			return null;
        }
    }
}

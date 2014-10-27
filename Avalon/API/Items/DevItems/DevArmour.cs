using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.API.Items.DevItems
{
    /// <summary>
    /// Armour that is meant for a developer.
    /// </summary>
    public abstract class DevArmour : DevItem
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DevArmour" /> class.
        /// </summary>
        /// <param name="devName">The name of the developer.</param>
        protected DevArmour(string devName)
            : base(devName)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public override void Effects       (Player p)
        {
            base.Effects(p);

            PunishIfCheater(p, DevEffects);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public override void ArmorSetBonus (Player p)
        {
            base.ArmorSetBonus(p);

            PunishIfCheater(p, DevSetBonus);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public override void VanityEffects (Player p)
        {
            base.VanityEffects(p);

            PunishIfCheater(p);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public override void VanitySetBonus(Player p)
        {
            base.VanitySetBonus(p);

            PunishIfCheater(p);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public virtual void DevEffects (Player p) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public virtual void DevSetBonus(Player p) { }
    }
}

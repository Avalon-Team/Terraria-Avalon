using System;
using System.Collections.Generic;
using System.Linq;

namespace Avalon.API.Items.MysticalTomes
{
    /// <summary>
    /// Specifies the skill manager of the tome.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class TomeSkillAttribute : Attribute
    {
        Type type;

        /// <summary>
        /// Creates a new instance of the <see cref="TomeSkillAttribute" /> class.
        /// </summary>
        /// <param name="managerType">The type of the <see cref="SkillManager" /> to instantiate.</param>
        public TomeSkillAttribute(Type managerType)
        {
            type = managerType;
        }

        internal SkillManager Instantiate()
        {
            if (!type.IsSubclassOf(typeof(SkillManager)))
                throw new ArgumentException(type + " must derive from Avalon.API.Items.MysticalTomes.SkillManager.", "managerType");

            SkillManager manager;
            try
            {
                manager = Activator.CreateInstance(type, type.IsNotPublic) as SkillManager;
            }
            catch (Exception e)
            {
                throw new ArgumentException(type + " has no parameterless constructor.", "managerType", e);
            }

            if (manager == null)
                throw new ArgumentException("Something weird happened.", "managerType");

            return manager;
        }
    }
}

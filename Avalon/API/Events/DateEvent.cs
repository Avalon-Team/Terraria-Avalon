using System;
using System.Collections.Generic;
using System.Linq;
using TAPI;
using Terraria;

namespace Avalon.API.Events
{
    /// <summary>
    /// An event that happens on a certain date.
    /// </summary>
    public abstract class DateEvent : IEvent
    {
        internal static Dictionary<string, DateEvent> events = new Dictionary<string, DateEvent>();

        /// <summary>
        /// The default value of the 'year' field when constructing a new <see cref="DateTime" /> that is meant for the value of <see cref="Date" />.
        /// </summary>
        public const int DefaultYear = 1;

        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        public string Name
        {
            get;
            protected set;
        }
        /// <summary>
        /// Gets or sets the date of the event.
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the <see cref="DateEvent" /> is active (today) or not.
        /// </summary>
        public virtual bool IsActive
        {
            get
            {
                return Date.DayOfYear == DateTime.Now.DayOfYear;
            }
        }

        /// <summary>
        /// Adds the given <see cref="DateEvent" /> to the game.
        /// </summary>
        /// <param name="base">The <see cref="ModBase" /> that owns the <see cref="DateEvent" />.</param>
        /// <param name="internalName">The internal name of the <see cref="DateEvent" />.</param>
        /// <param name="event">The <see cref="DateEvent" /> to add.</param>
        public static void AddToGame(ModBase @base, string internalName, DateEvent @event)
        {
            events.Add(@base.mod.InternalName + ":" + internalName, @event);
        }

        internal static void LoadVanilla()
        {
            // this reads a little weird...
            events.Add("Vanilla:Christmas", new Christmas());
            events.Add("Vanilla:Halloween", new Halloween());
        }

        /// <summary>
        /// Gets the <see cref="DateEvent" /> with the given name.
        /// </summary>
        /// <param name="internalName">The internal name of the <see cref="DateEvent" /> to return.</param>
        /// <returns>The <see cref="DateEvent" /> with <paramref name="internalName" /> as internal name.</returns>
        /// <exception cref="KeyNotFoundException">The <see cref="DateEvent" /> is not found.</exception>
        public static DateEvent GetEvent(string internalName)
        {
            return events[internalName];
        }
    }

    class Christmas : DateEvent
    {
        public Christmas()
        {
            Name = GetType().Name;

            Date = new DateTime(DefaultYear, 12, 25);
        }

        public override bool IsActive
        {
            get
            {
                return Main.xMas;
            }
        }
    }
    class Halloween : DateEvent
    {
        public Halloween()
        {
            Name = GetType().Name;

            Date = new DateTime(DefaultYear, 10, 31);
        }

        public override bool IsActive
        {
            get
            {
                return Main.halloween;
            }
        }
    }
}

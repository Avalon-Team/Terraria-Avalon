using System;
using System.Collections.Generic;
using System.Linq;

namespace Avalon.API.Events
{
    /// <summary>
    /// An event that can be active or inactive.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Gets whether the event is active or not.
        /// </summary>
        bool IsActive
        {
            get;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Avalon.API.Biomes
{
    /// <summary>
    /// Specifies wether the ModNPC is an NPC that appears (read: doesn't despawn) when a Wraith invasion is active.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class WraithInvasionNPCAttribute : Attribute
    {

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using TAPI;
using PoroCYon.MCT.Net;
using Avalon;

namespace PoroCYon.MCT
{
    /// <summary>
    /// An invasion. Does only manage active/inactive state and displayed text, not the NPCs spawning.
    /// </summary>
    public abstract class Invasion
    {
        static int invasionNextType = 0;

        internal static Dictionary<string, Invasion> invasions = new Dictionary<string, Invasion>();
        internal static Dictionary<int, string> invasionTypes = new Dictionary<int, string>();

        /// <summary>
        /// Gets the ID of the invasion.
        /// </summary>
        public int ID
        {
            get;
            internal set;
        }
        /// <summary>
        /// Gets wether the invasion is active or not.
        /// </summary>
        public bool IsActive
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the display name of the invasion.
        /// </summary>
        public abstract string DisplayName
        {
            get;
        }

        /// <summary>
        /// Gets the start text of the invasion. Must be invoked. The argument should be either "east" or "west".
        /// </summary>
        public Func<string, string> StartText
        {
            get;
            protected set;
        }
        /// <summary>
        /// Gets the arrived text of the invasion.
        /// </summary>
        public virtual string ArrivedText
        {
            get
            {
                return "The " + DisplayName + " has arrived!";
            }
        }
        /// <summary>
        /// Gets the defeated text of the invasion.
        /// </summary>
        public virtual string DefeatedText
        {
            get
            {
                return "The " + DisplayName + " has been defeated!";
            }
        }

        /// <summary>
        /// Creates a new instance of the Invasion class.
        /// </summary>
        protected Invasion()
        {
            StartText = d => "A " + DisplayName + " is coming from the " + d + "!";
        }

        /// <summary>
        /// Gets an Invasion from the specified ID.
        /// </summary>
        /// <param name="id">The ID of the Invasion to get.</param>
        /// <returns>The Invasion from the specified ID</returns>
        public static Invasion FromID(int id)
        {
            return FromInternalName(invasionTypes[id]);
        }
        /// <summary>
        /// Gets an Invasion from the specified internal name.
        /// </summary>
        /// <param name="name">The internal name the Invasion to get.</param>
        /// <returns>The Invasion from the specified internal name</returns>
        public static Invasion FromInternalName(string name)
        {
            return invasions[name];
        }

        /// <summary>
        /// Starts the invasion.
        /// </summary>
        public virtual void Start()
        {
            Main.StartInvasion(ID);
        }
        /// <summary>
        /// Stops all invasions.
        /// </summary>
        public virtual void Stop()
        {
            Main.invasionSize = 0;

            if (Main.invasionType != 0)
                InvasionWarning(FromID(Main.invasionType));

            Main.invasionType = Main.invasionDelay = 0;

            foreach (Invasion i in invasions.Values)
                i.IsActive = false;
        }

        static void InvasionWarning(Invasion inv)
        {
            // got from Terraria source

            Color c = new Color(175, 75, 255);

            if (Main.invasionSize <= 0)
                NetHelper.SendText(inv.DefeatedText, c);
            else if (Main.invasionX < Main.spawnTileX)
                NetHelper.SendText(inv.StartText("west"), c);
            else if (Main.invasionX > Main.spawnTileX)
                NetHelper.SendText(inv.StartText("east"), c);
            else
                NetHelper.SendText(inv.ArrivedText, c);
        }

        internal static int Load(ModBase @base, string internalName, Invasion invasion)
        {
            invasion.ID = ++invasionNextType;

            invasions.Add(@base.modName + ":" + internalName, invasion);
            invasionTypes.Add(invasion.ID, @base.modName + ":" + internalName);

            return invasion.ID;
        }

        internal static void LoadVanilla()
        {
            Load(Mod.Instance, "Vanilla:Goblin Army" , new GoblinArmyInv());
            Load(Mod.Instance, "Vanilla:Frost Legion", new FrostLegionInv());
            Load(Mod.Instance, "Vanilla:Pirates"     , new PiratesInv());
        }
    }

    class GoblinArmyInv : Invasion
    {
        public override string DisplayName
        {
            get
            {
                return "Goblin army";
            }
        }
    }
    class FrostLegionInv : Invasion
    {
        public override string DisplayName
        {
            get
            {
                return "Frost Legion";
            }
        }
    }
    class PiratesInv : Invasion
    {
        public override string DisplayName
        {
            get
            {
                return "Pirates";
            }
        }

        public override string ArrivedText
        {
            get
            {
                return "The " + DisplayName + " have arrived!";
            }
        }
        public override string DefeatedText
        {
            get
            {
                return "The " + DisplayName + " have been defeated!";
            }
        }

        internal PiratesInv()
            : base()
        {
            StartText = d => DisplayName + " are coming from the " + d + "!";
        }
    }
}

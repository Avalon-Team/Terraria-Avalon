using System;
using System.Collections.Generic;
using System.Linq;
using TAPI;

namespace Avalon
{
    /// <summary>
    /// Global world stuff
    /// </summary>
    public sealed class MWorld : ModWorld
    {
        /// <summary>
        /// Creates a new instance of the MWorld class
        /// </summary>
        /// <param name="base">The ModBase which belongs to the ModWorld instance</param>
        /// <remarks>Called by the mod loader</remarks>
        public MWorld(ModBase @base)
            : base(@base)
        {

        }

        /// <summary>
        /// Called when the world is loaded
        /// </summary>
        /// <param name="bb">A binary buffer which contains save data</param>
        public override void Load(BinBuffer bb)
        {
            base.Load(bb);


        }
        /// <summary>
        /// Called when the world is saved
        /// </summary>
        /// <param name="bb">A binary buffer where to put save data in</param>
        public override void Save(BinBuffer bb)
        {
            base.Save(bb);


        }

        /// <summary>
        /// Called when the world is opened on this client or server
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();


        }
        /// <summary>
        /// Called after the world is updated
        /// </summary>
        public override void PostUpdate()
        {
            base.PostUpdate();


        }

        /// <summary>
        /// Called when a Player joins the sever
        /// </summary>
        /// <param name="index">The whoAmI of the Player who is joining</param>
        public override void PlayerConnected(int index)
        {
            base.PlayerConnected(index);


        }

        /// <summary>
        /// Called after the world is generated
        /// </summary>
        public override void WorldGenPostInit()
        {
            base.WorldGenPostInit();


        }
        /// <summary>
        /// Used to modify the worldgen task list (insert additional tasks)
        /// </summary>
        /// <param name="list">The task list to modify</param>
        public override void WorldGenModifyTaskList(List<WorldGenTask> list)
        {
            base.WorldGenModifyTaskList(list);


        }
        /// <summary>
        /// Used to modify the hardmode task list (when the WoF is defeated)
        /// </summary>
        /// <param name="list">The task list to modify</param>
        public override void WorldGenModifyHardmodeTaskList(List<WorldGenTask> list)
        {
            base.WorldGenModifyHardmodeTaskList(list);


        }
    }
}

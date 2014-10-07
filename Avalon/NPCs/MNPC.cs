using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;
using Avalon.API.Biomes;

namespace Avalon.NPCs
{
    /// <summary>
    /// Global NPC stuff
    /// </summary>
    [GlobalMod]
    public class MNPC : ModNPC
    {
        /// <summary>
        /// Updates the spawn rate for every player. Called every tick.
        /// </summary>
        /// <param name="player">The Player where the NPC(s) could spawn.</param>
        public override void UpdateSpawnRate(Player player)
        {
            base.UpdateSpawnRate(player);

            if (AvalonMod.Wraiths.IsActive)
            {
                NPC.spawnRate = 15;
                NPC.maxSpawns = 20;

                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i] == null || Main.npc[i].type <= 0 || Main.npc[i].life <= 0 || !Main.npc[i].active || String.IsNullOrEmpty(Main.npc[i].name))
                        continue;

                    if (Main.npc[i].townNPC || Main.npc[i].boss)
                        continue;

                    bool keepAlive = false;

                    object[] attr = Main.npc[i].subClass.GetType().GetCustomAttributes(typeof(WraithInvasionNPCAttribute), true);
                    for (int j = 0; j < attr.Length; j++)
                        if (attr[j] is WraithInvasionNPCAttribute)
                            keepAlive = true;

                    Main.npc[i].active &= keepAlive;
                }
            }
        }

        /// <summary>
        /// Called before the <see cref="NPC" />'s loot is dropped.
        /// </summary>
        /// <returns></returns>
        public override bool PreNPCLoot()
        {
            if (VanillaDrop.Drops.ContainsKey(npc.type))
            {
                VanillaDrop drop = VanillaDrop.Drops[npc.type];

                if (Main.rand.NextDouble() > drop.Chance)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, drop.Type, drop.Amount, false, -1, true);
            }

            return true;

            // see VanillaDrop.cs for implementation

#pragma warning disable 162
            if (npc.type == NPCDef.byName["Vanilla:Paladin"].type && Main.rand.Next(99) == 0)
#pragma warning restore 162
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, (int)npc.width, (int)npc.height, ItemDef.byName["AvalonMod:Throwing Paladin's Hammer"].type, 1, false, 0);
                
            }
            #region SzGamer227's Ammo thread
            if (npc.type == NPCDef.byName["Vanilla:Goblin Archer"].type)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, (int)npc.width, (int)npc.height, ItemDef.byName["Vanilla:Arrow"].type, Main.rand.Next(0, 6), false, 0);
                
            }
            if (npc.type == NPCDef.byName["Vanilla:Pirate Crossbower"].type)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, (int)npc.width, (int)npc.height, ItemDef.byName["Vanilla:Arrow"].type, Main.rand.Next(0, 12), false, 0);
                
            }
            if (npc.type == NPCDef.byName["Vanilla:Skeleton Archer"].type)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, (int)npc.width, (int)npc.height, ItemDef.byName["Vanilla:Flaming Arrow"].type, Main.rand.Next(0, 4), false, 0);
                
            }
            if (npc.type == NPCDef.byName["Vanilla:Snowman Gangsta"].type)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, (int)npc.width, (int)npc.height, ItemDef.byName["Vanilla:Silver Bullet"].type, Main.rand.Next(0, 5), false, 0);
                
            }
            if (npc.type == NPCDef.byName["Vanilla:Tactical Skeleton"].type)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, (int)npc.width, (int)npc.height, ItemDef.byName["Vanilla:Silver Bullet"].type, Main.rand.Next(0, 20), false, 0);
                
            }
            if (npc.type == NPCDef.byName["Vanilla:Pirate Deadeye"].type)
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemDef.byName["Vanilla:Silver Bullet"].type, Main.rand.Next(0, 6), false, 0);
            if (npc.type == NPCDef.byName["Vanilla:Pirate Captain"].type)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemDef.byName["Vanilla:Silver Bullet"].type, Main.rand.Next(5, 60), false, 0);
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemDef.byName["Vanilla:Cannonball"   ].type, Main.rand.Next(0, 10), false, 0);
            }
            if (npc.type == NPCDef.byName["Vanilla:Elf Copter"       ].type)
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemDef.byName["Vanilla:Silver Bullet"       ].type, Main.rand.Next(0, 10), false, 0);
            if (npc.type == NPCDef.byName["Vanilla:Skeleton Sniper"  ].type)
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemDef.byName["Vanilla:High Velocity Bullet"].type, Main.rand.Next(0, 17), false, 0);
            if (npc.type == NPCDef.byName["Vanilla:Skeleton Commando"].type)
            {

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemDef.byName["Vanilla:Rocket I"  ].type, Main.rand.Next(0, 4), false, 0);
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemDef.byName["Vanilla:Rocket II" ].type, Main.rand.Next(0, 3), false, 0);
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemDef.byName["Vanilla:Rocket III"].type, Main.rand.Next(0, 2), false, 0);
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemDef.byName["Vanilla:Rocket IV" ].type, Main.rand.Next(0, 1), false, 0);
            }
            #endregion

            return base.PreNPCLoot();
        }
    }
}

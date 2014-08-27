using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace PoroCYon.MCT.Content
{
    internal static class ObjectLoader
    {
        internal static int AddWingsToGame(Texture2D texture)
        {
            if (Main.dedServ)
                return 1;

            int id = Main.wingsTexture.Count;

            if (Main.wingsTexture.ContainsValue(texture))
            {
                foreach (KeyValuePair<int, Texture2D> kvp in Main.wingsTexture)
                    if (kvp.Value == texture)
                    {
                        if (Main.wingsLoaded.ContainsKey(id))
                            Main.wingsLoaded[id] = true;
                        else
                            Main.wingsLoaded.Add(id, true);

                        id = kvp.Key;
                    }
            }
            else
                Main.wingsTexture.Add(id, texture);

            if (Main.wingsLoaded.ContainsKey(id))
                Main.wingsLoaded[id] = true;
            else
                Main.wingsLoaded.Add(id, true);

            return id;
        }
    }
}

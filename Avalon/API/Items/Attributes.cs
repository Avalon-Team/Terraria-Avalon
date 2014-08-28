using System;
using System.Collections.Generic;
using System.Linq;

namespace Avalon.API.Items
{
    /// <summary>
    /// Specifies the chain texture of the flail or grappling hook.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ChainTextureAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the texture name of the chain texture (in the 'Resources/Chain/' folder), not including the file extension.
        /// </summary>
        public string TextureName
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets or sets wether it should repace chain 3 (flail) instead of chain 1 (grappling hook).
        /// </summary>
        public bool ReplaceFlailChain
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the name of the mod base that contains the texture. Default is 'Avalon'.
        /// </summary>
        public string ModInternalName
        {
            get;
            set;
        }

        /// <summary>
        /// Specifies the chain texture of the flail or grappling hook.
        /// </summary>
        /// <param name="textureName">The texture name of the chain texture (in the './Chain/' folder)</param>
        public ChainTextureAttribute(string textureName)
        {
            TextureName = textureName;
        }
    }
}

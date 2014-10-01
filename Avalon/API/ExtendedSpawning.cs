using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;

namespace Avalon.API
{
	/// <summary>
	/// Provides extended and more convenient methods for entity spawning.
	/// </summary>
	public static class ExtendedSpawning
	{
		/// <summary>
		/// Spawns a <see cref="Projectile" />.
		/// </summary>
		/// <param name="pos">The position of the projectile.</param>
		/// <param name="vel">The velocity of the projectile.</param>
		/// <param name="type">The type of the projectile.</param>
		/// <param name="dmg">The damage of the projectile.</param>
		/// <param name="kb">The knockback of the projectile.</param>
		/// <param name="ow">The owner of the projectile.</param>
		/// <returns>The projectile's whoAmI.</returns>
		public static int NewProj(Vector2 pos, Vector2 vel, int    type, int dmg, float kb, int    ow)
		{
			return Projectile.NewProjectile(pos.X, pos.Y, vel.X, vel.Y, type, dmg, kb, ow);
		}
		/// <summary>
		/// Spawns a <see cref="Projectile" />.
		/// </summary>
		/// <param name="pos">The position of the projectile.</param>
		/// <param name="vel">The velocity of the projectile.</param>
		/// <param name="type">The type of the projectile.</param>
		/// <param name="dmg">The damage of the projectile.</param>
		/// <param name="kb">The knockback of the projectile.</param>
		/// <param name="ow">The owner of the projectile.</param>
		/// <returns>The projectile's whoAmI.</returns>
		public static int NewProj(Vector2 pos, Vector2 vel, string type, int dmg, float kb, int    ow)
		{
			return Projectile.NewProjectile(pos.X, pos.Y, vel.X, vel.Y, type, dmg, kb, ow);
		}
		/// <summary>
		/// Spawns a <see cref="Projectile" />.
		/// </summary>
		/// <param name="pos">The position of the projectile.</param>
		/// <param name="vel">The velocity of the projectile.</param>
		/// <param name="type">The type of the projectile.</param>
		/// <param name="dmg">The damage of the projectile.</param>
		/// <param name="kb">The knockback of the projectile.</param>
		/// <param name="ow">The owner of the projectile.</param>
		/// <returns>The projectile's whoAmI.</returns>
		public static int NewProj(Vector2 pos, Vector2 vel, int    type, int dmg, float kb, Player ow)
		{
			return Projectile.NewProjectile(pos.X, pos.Y, vel.X, vel.Y, type, dmg, kb, ow.whoAmI);
		}
		/// <summary>
		/// Spawns a <see cref="Projectile" />.
		/// </summary>
		/// <param name="pos">The position of the projectile.</param>
		/// <param name="vel">The velocity of the projectile.</param>
		/// <param name="type">The type of the projectile.</param>
		/// <param name="dmg">The damage of the projectile.</param>
		/// <param name="kb">The knockback of the projectile.</param>
		/// <param name="ow">The owner of the projectile.</param>
		/// <returns>The projectile's whoAmI.</returns>
		public static int NewProj(Vector2 pos, Vector2 vel, string type, int dmg, float kb, Player ow)
		{
			return Projectile.NewProjectile(pos.X, pos.Y, vel.X, vel.Y, type, dmg, kb, ow.whoAmI);
		}

		/// <summary>
		/// Spawns a <see cref="Dust" />.
		/// </summary>
		/// <param name="hitbox">Where to spawn the dust.</param>
		/// <param name="type">The type of the dust.</param>
		/// <param name="speed">The speed of the dust.</param>
		/// <param name="alpha">The alpha value of the dust.</param>
		/// <param name="colour">The tint of the dust.</param>
		/// <param name="scale">The scale of the dust.</param>
		/// <returns>The whoAmI of the dust.</returns>
		public static int NewDust(Rectangle hitbox, int type, Vector2 speed = default(Vector2), int alpha = 0, Color colour = default(Color), float scale = 1f)
		{
			return Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, type, speed.X, speed.Y, alpha, colour, scale);
		}
	}
}

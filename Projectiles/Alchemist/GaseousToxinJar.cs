using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Alchemist
{
    public class GaseousToxinJar : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Gaseous Toxin Jar";
            projectile.width = 12;
            projectile.height = 17;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.alpha = 1;
            projectile.damage = 0;
        }

        public override bool PreAI()
        {
            if (projectile.ai[0] == 0) // Has not yet collided
            {
                projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.02f * (float)projectile.direction;
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
                projectile.velocity.X = projectile.velocity.X * 0.99f;

                if (projectile.velocity.Y > 16f)
                {
                    projectile.velocity.Y = 16f;
                }
            }
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(13, (int)projectile.position.X, (int)projectile.position.Y, 1);
            projectile.alpha = 0;
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.PlaySound(13, (int)projectile.position.X, (int)projectile.position.Y, 1);
            projectile.alpha = 0;
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("ToxinCloud"), 0, 0, projectile.owner);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(), lightColor * projectile.alpha, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}

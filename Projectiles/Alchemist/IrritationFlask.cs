using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Alchemist
{
    public class IrritationFlask : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.damage = 20;
            Main.projFrames[projectile.type] = 4;
        }

        public override bool PreAI()
        {
            if (projectile.ai[0] == 0) // Has not yet collided
            {
                projectile.rotation = (float)Math.Atan2((double)-projectile.velocity.Y, (double)-projectile.velocity.X) + 1.57f;
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
                projectile.velocity.X = projectile.velocity.X * 0.99f;

                if (projectile.velocity.Y > 16f)
                {
                    projectile.velocity.Y = 16f;
                }
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 3)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(13, (int)projectile.position.X, (int)projectile.position.Y, 1);
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.PlaySound(13, (int)projectile.position.X, (int)projectile.position.Y, 1);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            Rectangle sourceRect = new Rectangle(0, 42 * projectile.frame, 22, 42);
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, sourceRect, lightColor, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}

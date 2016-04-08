using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic.Rings
{
    public class RingProjectile_FireBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Fire Bolt";
            projectile.width = 8;
            projectile.height = 8;

            projectile.ignoreWater = true;
            projectile.tileCollide = false;

            projectile.friendly = true;
            projectile.magic = true;
            projectile.timeLeft = 300;
        }

        public override bool PreAI()
        {
            for (int index = 3; index > 0; --index)
            {
                projectile.oldPos[index] = projectile.oldPos[index - 1];
            }
            projectile.oldPos[0] = projectile.position;

            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;

            if (projectile.position.Y >= projectile.ai[0])
                projectile.tileCollide = true;
            return false;
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);

            for (int i = 0; i < 3; ++i)
            {
                Vector2 vector2_2 = projectile.oldPos[i];
                Microsoft.Xna.Framework.Color color2 = lightColor * projectile.Opacity;
                color2.R = (byte)(0.5 * (double)color2.R * (double)(10 - i) / 20.0);
                color2.G = (byte)(0.5 * (double)color2.G * (double)(10 - i) / 20.0);
                color2.B = (byte)(0.5 * (double)color2.B * (double)(10 - i) / 20.0);
                color2.A = (byte)(0.5 * (double)color2.A * (double)(10 - i) / 20.0);
                Main.spriteBatch.Draw(texture, new Vector2(projectile.oldPos[i].X - Main.screenPosition.X + (projectile.width / 2),
                    projectile.oldPos[i].Y - Main.screenPosition.Y + projectile.height / 2), new Rectangle?(), color2, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0.0f);
            }

            ProjectileDrawing.DrawAroundOrigin(projectile.whoAmI, spriteBatch, lightColor);
            return false;
        }
    }
}

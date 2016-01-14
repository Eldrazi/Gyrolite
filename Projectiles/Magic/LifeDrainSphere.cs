using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class LifeDrainSphere : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Life Drain Sphere";
            projectile.width = 18;
            projectile.height = 22;
            projectile.aiStyle = 39;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.magic = true;
            Main.projFrames[projectile.type] = 2;
        }

        public override void PostAI()
        {
            if (projectile.frame > 1)
                projectile.frame = 0;
            base.PostAI();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.vampireHeal(damage, target.Center);
        }

        public override void PostDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture1 = (Texture2D)null;
            Color color1 = Color.White;
            Texture2D texture2 = ModLoader.GetTexture("Gyrolite/Projectiles/Magic/LifeDrain_Chain");

            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture2.Width * 0.5f, (float)texture2.Height * 0.5f);
            float num1 = (float)texture2.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture2, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                    if (texture1 != null)
                        Main.spriteBatch.Draw(texture1, position - Main.screenPosition, sourceRectangle, color1, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
        }
    }
}

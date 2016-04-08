using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Pets
{
    public class BabyIchorSticker : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Baby Ichor Sticker";
            projectile.width = 26;
            projectile.height = 34;

            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.netImportant = true;

            Main.projPet[projectile.type] = true;
            Main.projFrames[projectile.type] = 4;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            if (!player.active)
            {
                projectile.active = false;
                return false; ;
            }

            GyrolitePlayer modPlayer = (GyrolitePlayer)player.GetModPlayer(mod, "GyrolitePlayer");
            if (player.dead)
            {
                modPlayer.babyIchorStickerPet = false;
                modPlayer.petKillStack = 0;
            }
            if (modPlayer.babyIchorStickerPet)
            {
                if (modPlayer.petKillStack >= 50)
                {
                    int amount = Main.rand.Next(1, 4);
                    Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, ItemID.Ichor, amount);
                    modPlayer.petKillStack = 0;
                }
                projectile.timeLeft = 2;
            }

            float num16 = 0.2f;
            projectile.tileCollide = false;
            int num17 = 100;
            Vector2 center = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float dirX = player.position.X + (float)(player.width / 2) - center.X;
            float dirY = player.position.Y + (float)(player.height / 2) - center.Y;
            dirY += (float)Main.rand.Next(-10, 21);
            dirX += (float)Main.rand.Next(-10, 21);
            dirY -= 60f;
            float num20 = (float)Math.Sqrt((double)(dirX * dirX + dirY * dirY));
            float num21 = 6f;
            if (num20 < (float)num17 && player.velocity.Y == 0f && projectile.position.Y + (float)projectile.height <= player.position.Y + (float)player.height && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                if (projectile.velocity.Y < -6f)
                {
                    projectile.velocity.Y = -6f;
                }
            }
            if (num20 < 50f)
            {
                if (Math.Abs(projectile.velocity.X) > 2f || Math.Abs(projectile.velocity.Y) > 2f)
                {
                    projectile.velocity *= 0.99f;
                }
                num16 = 0.01f;
            }
            else
            {
                if (num20 < 100f)
                {
                    num16 = 0.02f;
                }
                if (num20 > 300f)
                {
                    num16 = 0.2f;
                }
                num20 = num21 / num20;
                dirX *= num20;
                dirY *= num20;
            }
            if (projectile.velocity.X < dirX)
            {
                projectile.velocity.X = projectile.velocity.X + num16;
                if (num16 > 0.05f && projectile.velocity.X < 0f)
                {
                    projectile.velocity.X = projectile.velocity.X + num16;
                }
            }
            if (projectile.velocity.X > dirX)
            {
                projectile.velocity.X = projectile.velocity.X - num16;
                if (num16 > 0.05f && projectile.velocity.X > 0f)
                {
                    projectile.velocity.X = projectile.velocity.X - num16;
                }
            }
            if (projectile.velocity.Y < dirY)
            {
                projectile.velocity.Y = projectile.velocity.Y + num16;
                if (num16 > 0.05f && projectile.velocity.Y < 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num16 * 2f;
                }
            }
            if (projectile.velocity.Y > dirY)
            {
                projectile.velocity.Y = projectile.velocity.Y - num16;
                if (num16 > 0.05f && projectile.velocity.Y > 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - num16 * 2f;
                }
            }

            if ((double)projectile.velocity.X > 0.25)
            {
                projectile.direction = -1;
            }
            else if ((double)projectile.velocity.X < -0.25)
            {
                projectile.direction = 1;
            }
            projectile.spriteDirection = projectile.direction;
            projectile.rotation = projectile.velocity.X * 0.05f;
            projectile.frameCounter++;

            if (projectile.frameCounter > 10)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame == Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }

            return false;
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Color lightColor)
        {
            ProjectileDrawing.DrawAroundOrigin(projectile.whoAmI, spriteBatch, lightColor);
            return false;
        }
    }
}

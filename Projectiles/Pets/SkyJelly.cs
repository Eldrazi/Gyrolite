using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Gyrolite.Projectiles.Pets
{
    public class SkyJelly : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Sky Jelly";
            projectile.width = 26;
            projectile.height = 34;

            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.netImportant = true;

            Main.projPet[projectile.type] = true;
            Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.LightPet[projectile.type] = true;
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
                modPlayer.skyJellyPet = false;
                modPlayer.petKillStack = 0;
            }
            if (modPlayer.skyJellyPet)
            {
                if (modPlayer.petKillStack >= 12)
                {
                    int amount = Main.rand.Next(1, 3);
                    Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, ItemID.StickyGlowstick, amount);
                    modPlayer.petKillStack = 0;
                }
                projectile.timeLeft = 2;
            }

            ++projectile.frameCounter;
            if (projectile.frameCounter < 6.0)
                projectile.frame = 0;
            else if (projectile.frameCounter < 12.0)
                projectile.frame = 1;
            else if (projectile.frameCounter < 18.0)
            {
                projectile.frame = 2;
            }
            else
            {
                projectile.frame = 3;
                if (projectile.frameCounter >= 23.0)
                    projectile.frameCounter = 0;
            }
            Lighting.AddLight((int)(projectile.position.X + (projectile.height / 2)) / 16, (int)(projectile.position.Y + (projectile.height / 2)) / 16, 0.1f, 0.3f, 0.8f);
        
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            float num2 = 0.5f;
            
            if (Vector2.Distance(player.Center, projectile.Center) >= 120)
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                projectile.velocity = projectile.velocity * 0.98f;

                if (projectile.velocity.X <= -num2 || projectile.velocity.X >= num2 || (projectile.velocity.Y <= -num2 || projectile.velocity.Y >= num2))
                    return false;
                float num3 = 7f;

                Vector2 center = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float dirX = player.position.X + (player.width / 2) - center.X;
                float dirY = player.position.Y + (player.height / 2) - center.Y;
                float num6 = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float num7 = num3 / num6;
                float num8 = dirX * num7;
                float num9 = dirY * num7;
                projectile.velocity.X = num8;
                projectile.velocity.Y = num9;
            }
            else
            {
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                projectile.velocity = projectile.velocity * 0.95f;

                if (projectile.velocity.X <= -num2 || projectile.velocity.X >= num2 || (projectile.velocity.Y <= -num2 || projectile.velocity.Y >= num2))
                    return false;

                projectile.ai[1]++;
                if (projectile.ai[1] >= 60)
                {
                    float num3 = 3f;

                    Vector2 center = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                    float dirX = (player.position.X + (player.width / 2) + Main.rand.Next(-player.width, player.width + 1)) - center.X;
                    float dirY = (player.position.Y + (player.height / 2) + Main.rand.Next(-player.height, player.height + 1)) - center.Y;
                    float num6 = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                    float num7 = num3 / num6;
                    float num8 = dirX * num7;
                    float num9 = dirY * num7;
                    projectile.velocity.X = num8;
                    projectile.velocity.Y = num9;

                    projectile.ai[1] = 0;
                }
            }

            return false;
        }
    }
}

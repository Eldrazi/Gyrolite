using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles
{
    public class AurasandBall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Aurasand Ball";
            projectile.width = 10;
            projectile.height = 10;
            projectile.knockBack = 6;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = true;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 17, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust].velocity.X *= 0.4f;
            }
            if (projectile.ai[0] == 1f)
            {
                if (!projectile.hostile)
                {
                    projectile.ai[1] += 1f;
                    if (projectile.ai[1] >= 60f)
                    {
                        projectile.ai[1] = 60f;
                        projectile.velocity.Y += 0.2f;
                    }
                }
                else
                {
                    projectile.velocity.Y += 0.41f;
                }
            }
            else if (projectile.ai[0] == 2f)
            {
                projectile.velocity.Y += 0.2f;
                if (projectile.velocity.X < -0.04f)
                {
                    projectile.velocity.X += 0.04f;
                }
                else if (projectile.velocity.X > 0.04f)
                {
                    projectile.velocity.X -= 0.04f;
                }
                else
                {
                    projectile.velocity.X = 0f;
                }
            }
            projectile.rotation += 0.1f;
            if (projectile.velocity.Y > 10f)
            {
                projectile.velocity.Y = 10f;
            }
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.1f, 0.2f, 0.7f);
        }

        public override void PostAI()
        {
            Vector2 oldVelocity = projectile.velocity;
            if (projectile.hostile)
            {
                projectile.velocity = Collision.AnyCollision(projectile.position, projectile.velocity, projectile.width, projectile.height);
            }
            else
            {
                projectile.velocity = Collision.TileCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, true, true, 1);
            }
            if (projectile.velocity != oldVelocity)
            {
                projectile.position += projectile.velocity;
                projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.owner == Main.myPlayer && !projectile.noDropItem)
            {
                int tileX = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
                int tileY = (int)(projectile.position.Y + (float)(projectile.width / 2)) / 16;
                int tileType = mod.TileType("Aurasand");
                if (Main.tile[tileX, tileY].halfBrick() && projectile.velocity.Y > 0f && System.Math.Abs(projectile.velocity.Y) > System.Math.Abs(projectile.velocity.X))
                {
                    tileY--;
                }
                if (!Main.tile[tileX, tileY].active())
                {
                    bool flag = WorldGen.PlaceTile(tileX, tileY, tileType, false, true, -1, 0);
                    if (flag)
                    {
                        if (Main.tile[tileX, tileY + 1].halfBrick() || Main.tile[tileX, tileY + 1].slope() != 0)
                        {
                            WorldGen.SlopeTile(tileX, tileY + 1, 0);
                            if (Main.netMode == 2)
                            {
                                NetMessage.SendData(17, -1, -1, "", 14, (float)tileX, (float)(tileY + 1), 0f, 0);
                            }
                        }
                        if (Main.netMode != 0)
                        {
                            NetMessage.SendData(17, -1, -1, "", 1, (float)tileX, (float)tileY, (float)tileType, 0);
                        }
                    }
                }
            }
        }
    }
}

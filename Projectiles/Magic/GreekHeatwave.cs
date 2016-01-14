using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class GreekHeatwave : ModProjectile
    {
        private int bounceTimes = 4;

        public override void SetDefaults()
        {
            projectile.name = "Life Leech Sphere";
            projectile.width = 24;
            projectile.height = 22;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.timeLeft = 600;
        }

        public override void AI()
        {
            projectile.velocity.Y += 0.2F;//projectile.ai[0];

            if (projectile.velocity.X > 0f)
            {
                projectile.rotation += (Math.Abs(projectile.velocity.Y) + Math.Abs(projectile.velocity.X)) * 0.02f;
            }
            else
            {
                projectile.rotation -= (Math.Abs(projectile.velocity.Y) + Math.Abs(projectile.velocity.X)) * 0.02f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bounceTimes--;
            if (bounceTimes <= 0)
            {
                projectile.Kill();
            }
            else
            {
                projectile.ai[0] += 0.1f;
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                projectile.velocity *= 0.75f;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            if (!projectile.active)
            {
                return;
            }

            Main.projectileIdentity[projectile.owner, projectile.identity] = -1;
            int num = projectile.timeLeft;
            projectile.timeLeft = 0;

            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);

            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 22;
            projectile.height = 22;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
            for (int num615 = 0; num615 < 30; num615++)
            {
                int num616 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num616].velocity *= 1.4f;
            }
            for (int num617 = 0; num617 < 20; num617++)
            {
                int num618 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3.5f);
                Main.dust[num618].noGravity = true;
                Main.dust[num618].velocity *= 7f;
                num618 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num618].velocity *= 3f;
            }
            for (int num619 = 0; num619 < 2; num619++)
            {
                float scaleFactor9 = 0.4f;
                if (num619 == 1)
                {
                    scaleFactor9 = 0.8f;
                }
                int num620 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore97 = Main.gore[num620];
                gore97.velocity.X = gore97.velocity.X + 1f;
                Gore gore98 = Main.gore[num620];
                gore98.velocity.Y = gore98.velocity.Y + 1f;
                num620 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore99 = Main.gore[num620];
                gore99.velocity.X = gore99.velocity.X - 1f;
                Gore gore100 = Main.gore[num620];
                gore100.velocity.Y = gore100.velocity.Y + 1f;
                num620 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore101 = Main.gore[num620];
                gore101.velocity.X = gore101.velocity.X + 1f;
                Gore gore102 = Main.gore[num620];
                gore102.velocity.Y = gore102.velocity.Y - 1f;
                num620 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore103 = Main.gore[num620];
                gore103.velocity.X = gore103.velocity.X - 1f;
                Gore gore104 = Main.gore[num620];
                gore104.velocity.Y = gore104.velocity.Y - 1f;
            }
            for (int num620 = 0; num620 < 10; ++num620)
            {
                float num2 = 240f;
                Vector2 vector2 = new Vector2(projectile.Center.X, projectile.Center.Y);
                float num3 = 0;
                float num4 = projectile.position.Y - 10;
                float num5 = num2 + Math.Abs(num3) * (1.0F / 500.0F);
                float num6 = num3 + (float)Main.rand.Next(-50, 51);
                float num7 = num4 - (float)Main.rand.Next(50, 201);
                float num8 = (float)Math.Sqrt((double)num6 * (double)num6 + (double)num7 * (double)num7);
                float num9 = num5 / num8;
                float num10 = num6 * num9;
                float num11 = num7 * num9;
                float SpeedX = num10 * (float)(1.0 + (double)Main.rand.Next(-30, 31) * 0.0001);
                float SpeedY = num11 * (float)(1.0 + (double)Main.rand.Next(-30, 31) * 0.0001);
                Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, mod.ProjectileType("GreekFire"), 20, 0, Main.myPlayer);
            }
            projectile.active = false;
        }
    }
}

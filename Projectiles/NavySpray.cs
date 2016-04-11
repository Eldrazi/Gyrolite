using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Gyrolite.Projectiles
{
    public class NavySpray : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Pure Spray";
            projectile.width = 6;
            projectile.height = 6;

            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override bool PreAI()
        {
            int conversionType = GyroliteWorld.AuraConversionType;
            if (projectile.owner == Main.myPlayer)
            {
                GyroliteWorld.Convert((int)(projectile.position.X + (float)(projectile.width / 2)) / 16, (int)(projectile.position.Y + (float)(projectile.height / 2)) / 16, conversionType, 2);
            }
            if (projectile.timeLeft > 133)
            {
                projectile.timeLeft = 133;
            }
            if (projectile.ai[0] > 7f)
            {
                float num346 = 1f;
                if (projectile.ai[0] == 8f)
                {
                    num346 = 0.2f;
                }
                else if (projectile.ai[0] == 9f)
                {
                    num346 = 0.4f;
                }
                else if (projectile.ai[0] == 10f)
                {
                    num346 = 0.6f;
                }
                else if (projectile.ai[0] == 11f)
                {
                    num346 = 0.8f;
                }
                projectile.ai[0] += 1f;
                for (int num347 = 0; num347 < 1; num347++)
                {
                    int num348 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("SoulticleDust"), projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                    Main.dust[num348].noGravity = true;
                    Main.dust[num348].scale *= 1.75f;
                    Dust dust54 = Main.dust[num348];
                    dust54.velocity.X = dust54.velocity.X * 2f;
                    Dust dust55 = Main.dust[num348];
                    dust55.velocity.Y = dust55.velocity.Y * 2f;
                    Main.dust[num348].scale *= num346;
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }
            projectile.rotation += 0.3f * projectile.direction;
            return false;
        }
    }
}

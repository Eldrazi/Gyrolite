using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Alchemist.Clouds
{
    public class ToxinCloud : AlchemistCloud
    {
        public override void SetDefaults()
        {
            projectile.name = "Gaseous Toxin Cloud";
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.knockBack = 0;
            projectile.thrown = true;
            projectile.damage = 0;
            projectile.timeLeft = 360;
            AoESizeX = AoESizeY = 160;
        }

        public override void AoEEffect()
        {
            if (projectile.ai[1] % 20 == 0)
            {
                projectile.damage = 5;
                projectile.Damage();
                projectile.damage = 0;
            }
            if (projectile.ai[1] % 2 == 0)
            {
                for (int num105 = 0; num105 < 5; num105++)
                {
                    int speedX = Main.rand.Next(-1, 2);
                    int speedY = Main.rand.Next(-1, 2);
                    int num106 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("ToxinCloud"), speedX, speedY, 30, default(Color), 2f);
                    Main.dust[num106].noGravity = true;
                    Dust dust9 = Main.dust[num106];
                    dust9.velocity.X = dust9.velocity.X * 0.1f;
                    dust9.velocity.Y = dust9.velocity.Y * 0.1f;
                }
            }
            ++projectile.ai[1];
        }
    }
}

using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Alchemist.Clouds
{
    public class RegenerativeCloud : AlchemistCloud
    {
        public override void SetDefaults()
        {
            projectile.name = "Gaseous Regenerative Cloud";
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
            if (projectile.ai[1] % 30 == 0)
            {
                for (int i = 0; i < Main.player.Length - 1; ++i)
                {
                    if (Main.player[i].active && !Main.player[i].dead && Vector2.Distance(projectile.Center, Main.player[i].Center) <= AoESizeX / 2)
                    {
                        int healAmount = Main.rand.Next(2, 6); // Get a random heal amount between 1 and 5.
                        Main.player[i].HealEffect(healAmount, false);
                        Main.player[i].statLife += healAmount;
                        if (Main.player[i].statLife > Main.player[i].statLifeMax2)
                        {
                            Main.player[i].statLife = Main.player[i].statLifeMax2;
                        }
                        NetMessage.SendData(66, -1, -1, "", i, (float)healAmount, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            if (projectile.ai[1] % 10 == 0)
            {
                for (int num697 = 0; num697 < 5; num697++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("RegenSparkle"));
                }
            }
            ++projectile.ai[1];
        }
    }
}

using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Alchemist.Clouds
{
    public class VacuumCloud : AlchemistCloud
    {
        protected float pullForce;

        public override void SetDefaults()
        {
            projectile.name = "Gaseous Vacuum Cloud";
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.knockBack = 0;
            projectile.thrown = true;
            projectile.damage = 0;
            projectile.timeLeft = 360;
            AoESizeX = AoESizeY = 160;
            pullForce = 0.1F;
        }

        public override void AoEEffect()
        {
            if (projectile.ai[1] % 20 == 0)
            {
                projectile.damage = 5;
                projectile.Damage();
                projectile.damage = 0;
            }
            for (int k = 0; k < 200; ++k)
            {
                // If the NPC is active and the distance between this projectile and the npc is less than 160 (16 blocks).
                if (Main.npc[k].active && !Main.npc[k].boss && Vector2.Distance(projectile.Center, Main.npc[k].Center) < AoESizeX / 2 + 48)
                {
                    Vector2 pullDirection = Main.npc[k].Center - projectile.Center;
                    pullDirection.Normalize();
                    Main.npc[k].velocity -= (pullDirection * pullForce);
                }
            }
            ++projectile.ai[1];
        }
    }
}

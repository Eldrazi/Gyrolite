using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class LifeSapDart : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Life Sap Dart";
            projectile.width = 14;
            projectile.height = 16;

            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 360;
            Main.projFrames[projectile.type] = 4;
        }

        public override bool PreAI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 6)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.vampireHeal(damage, target.Center);
        }
    }
}

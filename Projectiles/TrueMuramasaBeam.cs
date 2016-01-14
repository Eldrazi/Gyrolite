using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles
{
    public class TrueMuramasaBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "True Muramasa Beam";
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 27;
            projectile.damage = 150;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.light = 1f;
            projectile.alpha = (int)byte.MaxValue;
            projectile.friendly = true;
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

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(1, 1, 1, 0.5F);
        }
    }
}

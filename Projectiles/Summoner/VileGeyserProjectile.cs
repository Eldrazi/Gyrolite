using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Summoner
{
    public class VileGeyserProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Vile Flake";
            projectile.width = 8;
            projectile.height = 8;

            projectile.friendly = true;
            projectile.alpha = 0;
            Main.projFrames[projectile.type] = 3;
            projectile.extraUpdates = 1;

            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

        public override bool PreAI()
        {
            projectile.frame = (int)projectile.ai[0];

            projectile.ai[1]++;
            if (projectile.ai[1] >= 60)
            {
                projectile.alpha += 4;
                if (projectile.alpha >= 255)
                    projectile.Kill();
            }

            projectile.rotation += projectile.localAI[0];
            return false;
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Color lightColor)
        {
            ProjectileDrawing.DrawAroundOrigin(projectile.whoAmI, spriteBatch, lightColor);
            return false;
        }
    }
}

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gyrolite.Projectiles.Spears
{
    public class ObelisiumHalbert : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Obelisium Halbert";
            projectile.width = 18;
            projectile.height = 18;
            projectile.scale = 1.3f;

            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;

            projectile.penetrate = -1;
            projectile.melee = true;

            projectile.alpha = 0;
            projectile.hide = true;
        }

        public override bool PreAI()
        {
            ProjectileAI.SpearAI(projectile.whoAmI);
            return false;
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Color lightColor)
        {
            ProjectileDrawing.DrawSpear(projectile.whoAmI, spriteBatch, lightColor);
            return false;
        }
    }
}

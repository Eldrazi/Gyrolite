using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Flails
{
    public class CoreFlail : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "The Core";
            projectile.width = projectile.height = 22;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
        }

        public override bool PreAI()
        {
            ProjectileAI.FlailAI(projectile.whoAmI);
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return ProjectileAI.FlailTileCollide(projectile.whoAmI, oldVelocity);
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            ProjectileDrawing.DrawChain(projectile.whoAmI, Main.player[projectile.owner].MountedCenter,
                "Gyrolite/Projectiles/Chains/CoreFlail_Chain");
            ProjectileDrawing.DrawAroundOrigin(projectile.whoAmI, spriteBatch, lightColor);
            return false;
        }
    }
}

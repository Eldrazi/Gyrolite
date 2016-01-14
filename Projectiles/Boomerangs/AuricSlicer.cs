using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Boomerangs
{
    public class AuricSlicer : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Auric Slicer";
            projectile.width = projectile.height = 22;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
        }

        public override bool PreAI()
        {
            ProjectileAI.BoomerangAI(projectile.whoAmI, 30, 20, 1);
            projectile.spriteDirection = projectile.direction;
            return false;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            return ProjectileAI.BoomerangTileCollide(projectile.whoAmI, oldVelocity);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            ProjectileAI.BoomerangOnHitEntity(projectile.whoAmI);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            ProjectileAI.BoomerangOnHitEntity(projectile.whoAmI);
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Color lightColor)
        {
            ProjectileDrawing.DrawAroundOrigin(projectile.whoAmI, spriteBatch, lightColor);
            return false;
        }
    }
}

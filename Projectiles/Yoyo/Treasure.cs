using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Yoyo
{
    public class Treasure : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Treasure";
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.extraUpdates = 0;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.scale = 1.05f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 120);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 120);
        }
        public override void AI()
        {
            ProjectileAI.ExtraAction action = delegate()
            {
                if (projectile.localAI[0] % 30 == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.Next(-4, 5), Main.rand.Next(-4, 5), 24, 10, 0, projectile.owner);
                }
            };
            ProjectileAI.YoyoAI(projectile.whoAmI, 10, 320f, 15f, 0.4f, action);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            ProjectileDrawing.DrawString(projectile.whoAmI);
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}

using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    class GreekFireball : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Greek Fireball";
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.light = 0.8F;
            projectile.alpha = 100;
            projectile.magic = true;

            projectile.penetrate = 3;
        }

        public override bool PreAI()
        {
            for (int num105 = 0; num105 < 2; num105++)
            {
                int num106 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[num106].noGravity = true;
                Dust dust9 = Main.dust[num106];
                dust9.velocity.X = dust9.velocity.X * 0.3f;
                Dust dust10 = Main.dust[num106];
                dust10.velocity.Y = dust10.velocity.Y * 0.3f;
            }
            projectile.ai[1] += 1f;
            if (projectile.ai[1] >= 20f)
			{
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			}
            projectile.rotation += 0.3f * (float)projectile.direction;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            return false;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 5f)
            {
                projectile.position += projectile.velocity;
                projectile.Kill();
            }
            else
            {
                if (projectile.velocity.Y > 4f)
                {
                    if (projectile.velocity.Y != oldVelocity.Y)
                    {
                        projectile.velocity.Y = -oldVelocity.Y * 0.8f;
                    }
                }
                else if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 10);
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}

using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class LifeLeechBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Life Leech Sphere";
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 48;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.timeLeft = 100;
        }

        public override bool PreAI()
        {
            for (int num445 = 0; num445 < 4; num445++)
            {
                Vector2 vector30 = projectile.position;
                vector30 -= projectile.velocity * ((float)num445 * 0.25f);
                projectile.alpha = 255;
                int num446 = Dust.NewDust(vector30, 1, 1, 183, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num446].position = vector30;
                Dust dust60 = Main.dust[num446];
                dust60.position.X = dust60.position.X + (float)(projectile.width / 2);
                Dust dust61 = Main.dust[num446];
                dust61.position.Y = dust61.position.Y + (float)(projectile.height / 2);
                Main.dust[num446].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                Main.dust[num446].velocity *= 0.2f;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.vampireHeal(damage, target.Center);
        }
    }
}

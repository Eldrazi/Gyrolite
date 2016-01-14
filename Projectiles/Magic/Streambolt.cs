﻿using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class Streambolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Stream Bolt";
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.timeLeft = 600;
            projectile.penetrate = 10;
            projectile.magic = true;
        }

        public override bool PreAI()
        {
            for (int num92 = 0; num92 < 5; num92++)
            {
                float num93 = projectile.velocity.X / 3f * (float)num92;
                float num94 = projectile.velocity.Y / 3f * (float)num92;
                int num95 = 4;
                int num96 = Dust.NewDust(new Vector2(projectile.position.X + (float)num95, projectile.position.Y + (float)num95), projectile.width - num95 * 2, projectile.height - num95 * 2, 172, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num96].noGravity = true;
                Main.dust[num96].velocity *= 0.1f;
                Main.dust[num96].velocity += projectile.velocity * 0.1f;
                Dust dust5 = Main.dust[num96];
                dust5.position.X = dust5.position.X - num93;
                Dust dust6 = Main.dust[num96];
                dust6.position.Y = dust6.position.Y - num94;
            }
            if (Main.rand.Next(5) == 0)
            {
                int num97 = 4;
                int num98 = Dust.NewDust(new Vector2(projectile.position.X + (float)num97, projectile.position.Y + (float)num97), projectile.width - num97 * 2, projectile.height - num97 * 2, 172, 0f, 0f, 100, default(Color), 0.6f);
                Main.dust[num98].velocity *= 0.25f;
                Main.dust[num98].velocity += projectile.velocity * 0.5f;
            }
            projectile.ai[1] += 1f;
            if (projectile.ai[1] >= 20f)
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            }

            return false;
        }

        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 10) // Bounces 9 times.
            {
                projectile.position += projectile.velocity;
                projectile.Kill();
            }
            else
            {
                if (projectile.velocity.Y != oldVelocity.Y)
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
            if (Main.rand.Next(10) == 0) // 10% chance to inflict slow on an enemy.
                target.AddBuff(BuffID.Slow, 5);
            
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}

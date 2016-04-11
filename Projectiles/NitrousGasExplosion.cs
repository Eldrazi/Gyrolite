using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles
{
    public class NitrousGasExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "";
            projectile.width = 22;
            projectile.height = 22;

            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.npcProj = true;
            projectile.hostile = true;
        }

        public override bool PreAI()
        {
            projectile.Kill();
            return false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 180); // Applies Frostburn debuff for 3 seconds.
        }

        public override void Kill(int timeLeft)
        {
            if (!projectile.active)
            {
                return;
            }

            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 128;
            projectile.height = 128;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
            projectile.Damage();

            Main.projectileIdentity[projectile.owner, projectile.identity] = -1;
            int num = projectile.timeLeft;
            projectile.timeLeft = 0;

            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 22;
            projectile.height = 22;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

            for (int i = 0; i < 20; i++)
            {
                int dust11 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, 0f, -2f, 0, default(Color), 2.5F);
                Main.dust[dust11].noGravity = true;
                Main.dust[dust11].position.X += Main.rand.Next(-50, 51) / 20 - 1.5f;
                Main.dust[dust11].position.Y += Main.rand.Next(-50, 51) / 20 - 1.5f;
                if (Main.dust[dust11].position != projectile.Center)
                {
                    Main.dust[dust11].velocity = projectile.DirectionTo(Main.dust[dust11].position) * 6f;
                }
            }
        }
    }
}

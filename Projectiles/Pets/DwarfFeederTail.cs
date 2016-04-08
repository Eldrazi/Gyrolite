using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Pets
{
    public class DwarfFeederTail : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Dwarf Feeder";
            projectile.width = 14;
            projectile.height = 14;

            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
        }

        // projectile.ai[0] = target projectile.
        public override bool PreAI()
        {
            if (projectile.ai[0] >= 0 && projectile.ai[0] < Main.npc.Length)
            {
                Projectile target = Main.projectile[(int)projectile.ai[0]];

                if (target.active)
                {
                    projectile.timeLeft = 2;
                }

                Vector2 center = new Vector2(projectile.position.X + (projectile.width * 0.5F), projectile.position.Y + (projectile.height * 0.5F));
                float dirX = target.position.X + (target.width * 0.5F) - center.X;
                float dirY = target.position.Y + (target.height * 0.5F) - center.Y;

                projectile.rotation = (float)Math.Atan2(dirY, dirX) + 1.57F;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

                float projWidth = projectile.width;

                float dist = (length - projWidth) / length;
                float distX = dirX * dist;
                float distY = dirY * dist;
                projectile.velocity = Vector2.Zero;
                projectile.position.X = projectile.position.X + distX;
                projectile.position.Y = projectile.position.Y + distY;
            }
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            ProjectileDrawing.DrawAroundOrigin(projectile.whoAmI, spriteBatch, lightColor);
            return false;
        }
    }
}

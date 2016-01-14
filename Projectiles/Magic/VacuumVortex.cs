using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class VacuumVortex : ModProjectile
    {
        protected float pullForce;
        protected float pullDistance;

        public override void SetDefaults()
        {
            projectile.name = "Vacuum Vortex";
            projectile.width = 14;
            projectile.height = 16;

            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 360;
            projectile.alpha = 0;
            projectile.scale = 2;

            pullForce = 0.5F;
            pullDistance = 160;
        }

        public override bool PreAI()
        {            
            if (projectile.velocity.X > 0f)
            {
                projectile.rotation += (Math.Abs(projectile.velocity.Y) + Math.Abs(projectile.velocity.X)) * 0.05f;
            }
            else
            {
                projectile.rotation -= (Math.Abs(projectile.velocity.Y) + Math.Abs(projectile.velocity.X)) * 0.05f;
            }
            
            for (int k = 0; k < 200; ++k)
            {
                // If the NPC is active and the distance between this projectile and the npc is less than 160 (10 blocks).
                if (Main.npc[k].active && !Main.npc[k].boss && Vector2.Distance(projectile.position, Main.npc[k].position) < pullDistance)
                {
                    Vector2 pullDirection = (Main.npc[k].position - projectile.position);
                    pullDirection.Normalize();
                    Main.npc[k].velocity -= (pullDirection * pullForce);
                }
            }

            if (Main.rand.Next(4) == 0)
            {

                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, mod.DustType("VortexDust"), projectile.velocity.X, projectile.velocity.Y);
            }
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(), Color.White * 0.75F, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}

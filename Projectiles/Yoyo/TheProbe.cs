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
    public class TheProbe : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "The Probe";
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.extraUpdates = 0;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.scale = 1.08f;
        }
        public override void AI()
        {
            ProjectileAI.ExtraAction action = delegate ()
            {
                try
                {
                    projectile.frameCounter++;
                    if (projectile.frameCounter >= 15)
                    {
                        projectile.frameCounter = 0;
                        float rotation = (float)(Main.rand.Next(0, 361) * (Math.PI / 180));
                        Vector2 velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
                        int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, velocity.X, velocity.Y, ProjectileID.PinkLaser, 68, 0f);
                        Main.projectile[proj].friendly = true;
                        Main.projectile[proj].hostile = false;
                        Main.projectile[proj].velocity *= 6f;
                    }
                }
                catch (Exception e)
                {
                    Main.NewText(e.Message);
                }
            };
            ProjectileAI.YoyoAI(projectile.whoAmI, 14, 256f, 13f, 0.39f, action);
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

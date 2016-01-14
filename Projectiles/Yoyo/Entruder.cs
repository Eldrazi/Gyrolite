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
    public class Entruder : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Entruder";
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
                    if (projectile.frameCounter >= 30)
                    {
                        projectile.frameCounter = 0;
                        float distance = 2000f;
                        int index = -1;
                        for (int i = 0; i < 200; i++)
                        {
                            float dist = Vector2.Distance(projectile.Center, Main.npc[i].Center);
                            if (dist < distance && dist < 640f && !Main.npc[i].friendly && Main.npc[i].lifeMax > 5 && !Main.npc[i].townNPC)
                            {
                                index = i;
                                distance = dist;
                            }
                        }
                        if (index != -1)
                        {
                            bool check = Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[index].position, Main.npc[index].width, Main.npc[index].height);
                            if (check)
                            {
                                Vector2 vector = Main.npc[index].Center - projectile.Center;
                                float speed = 9f;
                                float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                                if (mag > speed)
                                {
                                    mag = speed / mag;
                                }
                                vector *= mag;
                                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector.X, vector.Y, 206, 16, 0.5f, projectile.owner);
                            }
                        }
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

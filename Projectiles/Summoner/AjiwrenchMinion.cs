using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Summoner
{
    public class AjiwrenchMinion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Ajiwrench";
            projectile.width = 28;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = -1;

            projectile.timeLeft *= 5;
            projectile.netImportant = true;
            projectile.minion = true;
            projectile.minionSlots = 1;            
        }

        public override bool PreAI()
        {
            if (Main.player[projectile.owner].dead)
            {
                Main.player[projectile.owner].sharknadoMinion = false;
            }
            if (Main.player[projectile.owner].sharknadoMinion)
            {
                projectile.timeLeft = 2;
            }

            float num9 = 0.1f;
            projectile.tileCollide = false;
            int num10 = 300;
            Vector2 vector2 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num11 = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - vector2.X;
            float num12 = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - vector2.Y;

            float num13 = (float)Math.Sqrt((double)(num11 * num11 + num12 * num12));
            float num14 = 3f;
            if (num13 > 500f)
            {
                projectile.localAI[0] = 10000f;
            }
            if (projectile.localAI[0] >= 10000f)
            {
                num14 = 14f;
            }
            if (num13 < (float)num10 && Main.player[projectile.owner].velocity.Y == 0f && projectile.position.Y + (float)projectile.height <= Main.player[projectile.owner].position.Y + (float)Main.player[projectile.owner].height && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                //projectile.ai[0] = 0f;
                if (projectile.velocity.Y < -6f)
                {
                    projectile.velocity.Y = -6f;
                }
            }
            if (num13 < 150f)
            {
                if (Math.Abs(projectile.velocity.X) > 2f || Math.Abs(projectile.velocity.Y) > 2f)
                {
                    projectile.velocity *= 0.99f;
                }
                num9 = 0.01f;
                if (num11 < -2f)
                {
                    num11 = -2f;
                }
                if (num11 > 2f)
                {
                    num11 = 2f;
                }
                if (num12 < -2f)
                {
                    num12 = -2f;
                }
                if (num12 > 2f)
                {
                    num12 = 2f;
                }
            }
            else
            {
                if (num13 > 300f)
                {
                    num9 = 0.2f;
                }
                num13 = num14 / num13;
                num11 *= num13;
                num12 *= num13;
            }
            if (projectile.velocity.X < num11)
            {
                projectile.velocity.X = projectile.velocity.X + num9;
                if (num9 > 0.05f && projectile.velocity.X < 0f)
                {
                    projectile.velocity.X = projectile.velocity.X + num9;
                }
            }
            if (projectile.velocity.X > num11)
            {
                projectile.velocity.X = projectile.velocity.X - num9;
                if (num9 > 0.05f && projectile.velocity.X > 0f)
                {
                    projectile.velocity.X = projectile.velocity.X - num9;
                }
            }
            if (projectile.velocity.Y < num12)
            {
                projectile.velocity.Y = projectile.velocity.Y + num9;
                if (num9 > 0.05f && projectile.velocity.Y < 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num9;
                }
            }
            if (projectile.velocity.Y > num12)
            {
                projectile.velocity.Y = projectile.velocity.Y - num9;
                if (num9 > 0.05f && projectile.velocity.Y > 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - num9;
                }
            }
            projectile.localAI[0] += (float)Main.rand.Next(10);
            if (projectile.localAI[0] > 10000f)
            {
                if (projectile.localAI[1] == 0f)
                {
                    if (projectile.velocity.X < 0f)
                    {
                        projectile.localAI[1] = -1f;
                    }
                    else
                    {
                        projectile.localAI[1] = 1f;
                    }
                }
                projectile.rotation += 0.25f * projectile.localAI[1];
                if (projectile.localAI[0] > 12000f)
                {
                    projectile.localAI[0] = 0f;
                }
            }
            else
            {
                projectile.localAI[1] = 0f;
                float num15 = projectile.velocity.X * 0.1f;
                if (projectile.rotation > num15)
                {
                    projectile.rotation -= (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f;
                    if (projectile.rotation < num15)
                    {
                        projectile.rotation = num15;
                    }
                }
                if (projectile.rotation < num15)
                {
                    projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f;
                    if (projectile.rotation > num15)
                    {
                        projectile.rotation = num15;
                    }
                }
            }
            if ((double)projectile.rotation > 6.28)
            {
                projectile.rotation -= 6.28f;
            }
            if ((double)projectile.rotation < -6.28)
            {
                projectile.rotation += 6.28f;
                return false;
            }

            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            switch ((int)projectile.ai[0])
            {
                case 1:
                    target.AddBuff(BuffID.OnFire, 300); // 5 second debuff
                    break;
                case 2:
                    target.AddBuff(BuffID.Frostburn, 300); // 5 second debuff
                    break;
                case 3:
                    target.AddBuff(BuffID.CursedInferno, 300); // 5 second debuff
                    break;
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModLoader.GetTexture("Gyrolite/Projectiles/Summoner/AjiwrenchMinion" + (int)projectile.ai[0]);
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(), Color.White * 0.75F, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}

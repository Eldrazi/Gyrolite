using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class LifeLeechSphere : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Life Leech Sphere";
            projectile.width = 22;
            projectile.height = 22;
            projectile.magic = true;
            projectile.timeLeft = 660;
            projectile.light = 0.5f;
            projectile.aiStyle = 47;
            Main.projFrames[projectile.type] = 5;
        }

        public override bool PreAI()
        {
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = projectile.velocity.X;
                projectile.ai[1] = projectile.velocity.Y;
            }
            if (projectile.velocity.X > 0f)
            {
                projectile.rotation += (Math.Abs(projectile.velocity.Y) + Math.Abs(projectile.velocity.X)) * 0.001f;
            }
            else
            {
                projectile.rotation -= (Math.Abs(projectile.velocity.Y) + Math.Abs(projectile.velocity.X)) * 0.001f;
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 6)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 4)
                {
                    projectile.frame = 0;
                }
            }

            if (Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y)) > 2.0)
            {
                projectile.velocity *= 0.98f;
            }
            for (int num431 = 0; num431 < 1000; num431++)
            {
                if (num431 != projectile.whoAmI && Main.projectile[num431].active && Main.projectile[num431].owner == projectile.owner && Main.projectile[num431].type == projectile.type && projectile.timeLeft > Main.projectile[num431].timeLeft && Main.projectile[num431].timeLeft > 30)
                {
                    Main.projectile[num431].timeLeft = 30;
                }
            }
            int[] array = new int[20];
            int num432 = 0;
            float num433 = 300f;
            bool flag14 = false;
            for (int num434 = 0; num434 < 200; num434++)
            {
                if (Main.npc[num434].CanBeChasedBy(this, false))
                {
                    float num435 = Main.npc[num434].position.X + (float)(Main.npc[num434].width / 2);
                    float num436 = Main.npc[num434].position.Y + (float)(Main.npc[num434].height / 2);
                    float num437 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num435) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num436);
                    if (num437 < num433 && Collision.CanHit(projectile.Center, 1, 1, Main.npc[num434].Center, 1, 1))
                    {
                        if (num432 < 20)
                        {
                            array[num432] = num434;
                            num432++;
                        }
                        flag14 = true;
                    }
                }
            }
            if (projectile.timeLeft < 30)
            {
                flag14 = false;
            }
            if (flag14)
            {
                int num438 = Main.rand.Next(num432);
                num438 = array[num438];
                float num439 = Main.npc[num438].position.X + (float)(Main.npc[num438].width / 2);
                float num440 = Main.npc[num438].position.Y + (float)(Main.npc[num438].height / 2);
                if (++projectile.localAI[0] > 30)
                {
                    projectile.localAI[0] = 0f;
                    float num441 = 6f;
                    Vector2 value10 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    value10 += projectile.velocity * 4f;
                    float num442 = num439 - value10.X;
                    float num443 = num440 - value10.Y;
                    float num444 = (float)Math.Sqrt((double)(num442 * num442 + num443 * num443));
                    num444 = num441 / num444;
                    num442 *= num444;
                    num443 *= num444;
                    Projectile.NewProjectile(value10.X, value10.Y, num442, num443, mod.ProjectileType("LifeLeechBolt"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                    return false;
                }
            }
            return false;
        }
    }
}

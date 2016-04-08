using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Gyrolite.Projectiles;

namespace Gyrolite
{
    class GyroliteGlobalProjectiles : GlobalProjectile
    {
        public override bool PreAI(Projectile projectile)
        {            
            #region Rewriting Yoyo AIs
            if (projectile.type == 534 || (projectile.type >= 541 && projectile.type <= 555) || (projectile.type >= 562 && projectile.type <= 564) || projectile.type == 603)
            {
                switch (projectile.type)
                {
                    case 534:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 9, 220f, 13);
                        return false;
                    case 541:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 3, 130f, 9);
                        return false;
                    case 542:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 7, 195f, 12.5f);
                        return false;
                    case 543:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 6, 207f, 12);
                        return false;
                    case 544:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 8, 215f, 13f);
                        return false;
                    case 545:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 13, 235f, 14f, 045f, delegate() 
                        {
                            if (Main.rand.Next(6) == 0)
                            {
                                int num8 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 1f);
                                Main.dust[num8].noGravity = true;
                            }
                        });
                        return false;
                    case 546:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 16, 275f, 17f);
                        return false;
                    case 547:
                        ProjectileAI.YoyoAI(projectile.whoAmI, -1, 280f, 17f);
                        return false;
                    case 548:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 5, 170f, 11f);
                        return false;
                    case 549:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 14, 290f, 16f);
                        return false;
                    case 550:
                    case 551:
                        ProjectileAI.YoyoAI(projectile.whoAmI, -1, 370f, 16f);
                        return false;
                    case 552:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 15, 270f, 14f);
                        return false;
                    case 553:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 12, 275f, 15f, 0.45f, delegate()
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                int num9 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 1f);
                                Main.dust[num9].noGravity = true;
                                Main.dust[num9].scale = 1.6f;
                            }
                        });
                        return false;
                    case 554:
                        ProjectileAI.YoyoAI(projectile.whoAmI, -1, 340f, 16f);
                        return false;
                    case 555:
                        ProjectileAI.YoyoAI(projectile.whoAmI, -1, 360f, 16.5f);
                        return false;
                    case 562:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 8, 235f, 15f);
                        return false;
                    case 563:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 10, 250f, 12f);
                        return false;
                    case 564:
                        ProjectileAI.YoyoAI(projectile.whoAmI, 11, 225f, 14f);
                        return false;
                    case 603:
                        ProjectileAI.YoyoAI(projectile.whoAmI, -1, 400f, 17.5f, 0.45f, delegate()
                        {
                            projectile.localAI[1] += 1f;
                            if (projectile.localAI[1] >= 6f)
                            {
                                float num2 = 400f;
                                Vector2 vector = projectile.velocity;
                                Vector2 vector2 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                                vector2.Normalize();
                                vector2 *= (float)Main.rand.Next(10, 41) * 0.1f;
                                if (Main.rand.Next(3) == 0)
                                {
                                    vector2 *= 2f;
                                }
                                vector *= 0.25f;
                                vector += vector2;
                                for (int j = 0; j < 200; j++)
                                {
                                    if (Main.npc[j].CanBeChasedBy(projectile, false))
                                    {
                                        float num3 = Main.npc[j].position.X + (float)(Main.npc[j].width / 2);
                                        float num4 = Main.npc[j].position.Y + (float)(Main.npc[j].height / 2);
                                        float num5 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num3) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num4);
                                        if (num5 < num2 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
                                        {
                                            num2 = num5;
                                            vector.X = num3;
                                            vector.Y = num4;
                                            vector -= projectile.Center;
                                            vector.Normalize();
                                            vector *= 8f;
                                        }
                                    }
                                }
                                vector *= 0.8f;
                                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector.X, vector.Y, 604, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                                projectile.localAI[1] = 0f;
                            }
                        });
                        return false;
                }
            }
            #endregion

            return true;
        }
    }
}

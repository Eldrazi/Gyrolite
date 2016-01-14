using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles
{
    public static class ProjectileDrawing
    {
        public static void DrawString(int index, Vector2 to = default(Vector2))
        {
            Projectile projectile = Main.projectile[index];
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Vector2 vector = mountedCenter;
            vector.Y += Main.player[projectile.owner].gfxOffY;
            if (to != default(Vector2))
            {
                vector = to;
            }
            float num3 = projectile.Center.X - vector.X;
            float num4 = projectile.Center.Y - vector.Y;
            Math.Sqrt((double)(num3 * num3 + num4 * num4));
            float rotation = (float)Math.Atan2((double)num4, (double)num3) - 1.57f;
            if (!projectile.counterweight)
            {
                int num5 = -1;
                if (projectile.position.X + (float)(projectile.width / 2) < Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
                {
                    num5 = 1;
                }
                num5 *= -1;
                Main.player[projectile.owner].itemRotation = (float)Math.Atan2((double)(num4 * (float)num5), (double)(num3 * (float)num5));
            }
            bool flag = true;
            if (num3 == 0f && num4 == 0f)
            {
                flag = false;
            }
            else
            {
                float num6 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
                num6 = 12f / num6;
                num3 *= num6;
                num4 *= num6;
                vector.X -= num3 * 0.1f;
                vector.Y -= num4 * 0.1f;
                num3 = projectile.position.X + (float)projectile.width * 0.5f - vector.X;
                num4 = projectile.position.Y + (float)projectile.height * 0.5f - vector.Y;
            }
            while (flag)
            {
                float num7 = 12f;
                float num8 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
                float num9 = num8;
                if (float.IsNaN(num8) || float.IsNaN(num9))
                {
                    flag = false;
                }
                else
                {
                    if (num8 < 20f)
                    {
                        num7 = num8 - 8f;
                        flag = false;
                    }
                    num8 = 12f / num8;
                    num3 *= num8;
                    num4 *= num8;
                    vector.X += num3;
                    vector.Y += num4;
                    num3 = projectile.position.X + (float)projectile.width * 0.5f - vector.X;
                    num4 = projectile.position.Y + (float)projectile.height * 0.1f - vector.Y;
                    if (num9 > 12f)
                    {
                        float num10 = 0.3f;
                        float num11 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
                        if (num11 > 16f)
                        {
                            num11 = 16f;
                        }
                        num11 = 1f - num11 / 16f;
                        num10 *= num11;
                        num11 = num9 / 80f;
                        if (num11 > 1f)
                        {
                            num11 = 1f;
                        }
                        num10 *= num11;
                        if (num10 < 0f)
                        {
                            num10 = 0f;
                        }
                        num10 *= num11;
                        num10 *= 0.5f;
                        if (num4 > 0f)
                        {
                            num4 *= 1f + num10;
                            num3 *= 1f - num10;
                        }
                        else
                        {
                            num11 = Math.Abs(projectile.velocity.X) / 3f;
                            if (num11 > 1f)
                            {
                                num11 = 1f;
                            }
                            num11 -= 0.5f;
                            num10 *= num11;
                            if (num10 > 0f)
                            {
                                num10 *= 2f;
                            }
                            num4 *= 1f + num10;
                            num3 *= 1f - num10;
                        }
                    }
                    rotation = (float)Math.Atan2((double)num4, (double)num3) - 1.57f;
                    int stringColor = Main.player[projectile.owner].stringColor;
                    Microsoft.Xna.Framework.Color color = WorldGen.paintColor(stringColor);
                    if (color.R < 75)
                    {
                        color.R = 75;
                    }
                    if (color.G < 75)
                    {
                        color.G = 75;
                    }
                    if (color.B < 75)
                    {
                        color.B = 75;
                    }
                    if (stringColor == 13)
                    {
                        color = new Microsoft.Xna.Framework.Color(20, 20, 20);
                    }
                    else if (stringColor == 14 || stringColor == 0)
                    {
                        color = new Microsoft.Xna.Framework.Color(200, 200, 200);
                    }
                    else if (stringColor == 28)
                    {
                        color = new Microsoft.Xna.Framework.Color(163, 116, 91);
                    }
                    else if (stringColor == 27)
                    {
                        color = new Microsoft.Xna.Framework.Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
                    }
                    color.A = (byte)((float)color.A * 0.4f);
                    float num12 = 0.5f;
                    color = Lighting.GetColor((int)vector.X / 16, (int)(vector.Y / 16f), color);
                    color = new Microsoft.Xna.Framework.Color((int)((byte)((float)color.R * num12)), (int)((byte)((float)color.G * num12)), (int)((byte)((float)color.B * num12)), (int)((byte)((float)color.A * num12)));
                    Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(vector.X - Main.screenPosition.X + (float)Main.fishingLineTexture.Width * 0.5f, vector.Y - Main.screenPosition.Y + (float)Main.fishingLineTexture.Height * 0.5f) - new Vector2(6f, 0f), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num7)), color, rotation, new Vector2((float)Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
                }
            }
        }

        /// <summary>
        /// Draws a chain between the projectile and the given coordinate.
        /// </summary>
        /// <param name="index">Index of the projectile in the Main.projecile array.</param>
        /// <param name="to">The coordinate that the chain should be drawn to. If a player is a target, its MountedCenter should be used.</param>
        /// <param name="chainPath">The full path of a chain texture, including mod name (example: "MyMod/Projectiles/Projectile_Chain").</param>
        public static void DrawChain(int index, Vector2 to, string chainPath)
        {
            Texture2D texture = ModLoader.GetTexture(chainPath);
            Projectile projectile = Main.projectile[index];
            Vector2 position = projectile.Center;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = to - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = to - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, 
                        color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }
        }

        public static void DrawAroundOrigin(int index, SpriteBatch spriteBatch, Color lightColor)
        {
            Projectile projectile = Main.projectile[index];
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            SpriteEffects effect = projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(), lightColor, projectile.rotation, origin, projectile.scale, effect, 0);
        }

        public static void DrawSpear(int index, SpriteBatch spriteBatch, Color lightColor)
        {
            Projectile projectile = Main.projectile[index];
            Vector2 zero = Vector2.Zero;
            SpriteEffects effects1 = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                zero.X = (float)Main.projectileTexture[projectile.type].Width;
                effects1 = SpriteEffects.FlipHorizontally;
            }
            Main.spriteBatch.Draw(Main.projectileTexture[projectile.type], new Vector2(projectile.position.X - Main.screenPosition.X + (float)(projectile.width / 2), projectile.position.Y - Main.screenPosition.Y + (float)(projectile.height / 2) + projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height)), projectile.GetAlpha(lightColor), projectile.rotation, zero, projectile.scale, effects1, 0.0f);

        }
    }
    public static class ProjectileAI
    {
        public delegate void ExtraAction();
        /// <summary>
        /// LocalAI[1] cannot be used in either of the Extra Actions due to it being used for the initialization (or at least if it is never set to 0 it can be).
        /// </summary>
        /// <param name="index">Index of the projectile in the Main.projecile array.</param>
        /// <param name="seconds">How long before the yoyo comes back to the player in seconds.</param>
        /// <param name="length">How far away from the player the yoyo can go.</param>
        /// <param name="acceleration">How fast the yoyo travels?</param>
        /// <param name="rotationSpeed">The rotation speed of the yoyo.</param>
        /// <param name="action">An action to perform when the projectile is out in front of the player.</param>
        /// <param name="initialize">An action to perform once when initializing.</param>
        public static void YoyoAI(int index, float seconds, float length, float acceleration = 14f, float rotationSpeed = 0.45f, ExtraAction action = null, ExtraAction initialize = null) //0.45f is the default
        {
            //extra action allows us to shoot extra projectiles and such

            /*variable dictionary:
            num7 = max length of yoyo in pixels, so 16 = 1 tile.
            num6 = something related to velocity
            */
            Projectile projectile = Main.projectile[index];

            bool flag = false;
            if (initialize != null && projectile.localAI[1] == 0)
            {
                projectile.localAI[1] = 1;
                initialize();
            }
            for (int i = 0; i < projectile.whoAmI; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == projectile.owner && Main.projectile[i].type == projectile.type)
                {
                    flag = true;
                }
            }
            if (projectile.owner == Main.myPlayer)
            {
                projectile.localAI[0] += 1f;
                if (flag)
                {
                    projectile.localAI[0] += (float)Main.rand.Next(10, 31) * 0.1f;
                }
                float num = projectile.localAI[0] / 60f;
                num /= (1f + Main.player[projectile.owner].meleeSpeed) / 2f;
                if (num > seconds)
                {
                    projectile.ai[0] = -1f; //retract yoyo
                }
            }
            bool flag2 = false; //Just gonna leave this here, it was to check if it is a counterweight but in our case it never can be
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return;
            }
            if (!flag2 && !flag)
            {
                Main.player[projectile.owner].heldProj = projectile.whoAmI;
                Main.player[projectile.owner].itemAnimation = 2;
                Main.player[projectile.owner].itemTime = 2;
                if (projectile.position.X + (float)(projectile.width / 2) > Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
                {
                    Main.player[projectile.owner].ChangeDir(1);
                    projectile.direction = 1;
                }
                else
                {
                    Main.player[projectile.owner].ChangeDir(-1);
                    projectile.direction = -1;
                }
            }
            if (projectile.velocity.HasNaNs())
            {
                projectile.Kill();
            }
            projectile.timeLeft = 6;
            float num6 = acceleration;
            float num7 = length;
            if (Main.player[projectile.owner].yoyoString)
            {
                num7 = num7 * 1.25f + 30f;
            }
            num7 /= (1f + Main.player[projectile.owner].meleeSpeed * 3f) / 4f;
            num6 /= (1f + Main.player[projectile.owner].meleeSpeed * 3f) / 4f;
            float num10 = 14f - num6 / 2f;
            float num11 = 5f + num6 / 2f;
            if (flag)
            {
                num11 += 20f;
            }
            if (projectile.ai[0] >= 0f)
            {
                if (projectile.velocity.Length() > num6)
                {
                    projectile.velocity *= 0.98f;
                }
                bool flag3 = false;
                bool flag4 = false;
                Vector2 vector3 = Main.player[projectile.owner].Center - projectile.Center;
                if (vector3.Length() > num7)
                {
                    flag3 = true;
                    if ((double)vector3.Length() > (double)num7 * 1.3)
                    {
                        flag4 = true;
                    }
                }
                if (projectile.owner == Main.myPlayer)
                {
                    if (!Main.player[projectile.owner].channel || Main.player[projectile.owner].stoned || Main.player[projectile.owner].frozen)
                    {
                        projectile.ai[0] = -1f;
                        projectile.ai[1] = 0f;
                        projectile.netUpdate = true;
                    }
                    else
                    {
                        Vector2 vector4 = Main.ReverseGravitySupport(Main.MouseScreen, 0f) + Main.screenPosition;
                        float x = vector4.X;
                        float y = vector4.Y;
                        Vector2 vector5 = new Vector2(x, y) - Main.player[projectile.owner].Center;
                        if (vector5.Length() > num7)
                        {
                            vector5.Normalize();
                            vector5 *= num7;
                            vector5 = Main.player[projectile.owner].Center + vector5;
                            x = vector5.X;
                            y = vector5.Y;
                        }
                        if (projectile.ai[0] != x || projectile.ai[1] != y)
                        {
                            Vector2 value = new Vector2(x, y);
                            Vector2 vector6 = value - Main.player[projectile.owner].Center;
                            if (vector6.Length() > num7 - 1f)
                            {
                                vector6.Normalize();
                                vector6 *= num7 - 1f;
                                value = Main.player[projectile.owner].Center + vector6;
                                x = value.X;
                                y = value.Y;
                            }
                            projectile.ai[0] = x;
                            projectile.ai[1] = y;
                            projectile.netUpdate = true;
                        }
                    }
                }
                if (flag4 && projectile.owner == Main.myPlayer)
                {
                    projectile.ai[0] = -1f;
                    projectile.netUpdate = true;
                }
                if (projectile.ai[0] >= 0f)
                {
                    if (flag3)
                    {
                        num10 /= 2f;
                        num6 *= 2f;
                        if (projectile.Center.X > Main.player[projectile.owner].Center.X && projectile.velocity.X > 0f)
                        {
                            projectile.velocity.X = projectile.velocity.X * 0.5f;
                        }
                        if (projectile.Center.Y > Main.player[projectile.owner].Center.Y && projectile.velocity.Y > 0f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y * 0.5f;
                        }
                        if (projectile.Center.X < Main.player[projectile.owner].Center.X && projectile.velocity.X > 0f)
                        {
                            projectile.velocity.X = projectile.velocity.X * 0.5f;
                        }
                        if (projectile.Center.Y < Main.player[projectile.owner].Center.Y && projectile.velocity.Y > 0f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y * 0.5f;
                        }
                    }
                    Vector2 value2 = new Vector2(projectile.ai[0], projectile.ai[1]);
                    Vector2 vector7 = value2 - projectile.Center;
                    projectile.velocity.Length();
                    if (vector7.Length() > num11)
                    {
                        vector7.Normalize();
                        vector7 *= num6;
                        projectile.velocity = (projectile.velocity * (num10 - 1f) + vector7) / num10;
                    }
                    else if (flag)
                    {
                        if ((double)projectile.velocity.Length() < (double)num6 * 0.6)
                        {
                            vector7 = projectile.velocity;
                            vector7.Normalize();
                            vector7 *= num6 * 0.6f;
                            projectile.velocity = (projectile.velocity * (num10 - 1f) + vector7) / num10;
                        }
                    }
                    else
                    {
                        projectile.velocity *= 0.8f;
                    }
                    if (flag && !flag3 && (double)projectile.velocity.Length() < (double)num6 * 0.6)
                    {
                        projectile.velocity.Normalize();
                        projectile.velocity *= num6 * 0.6f;
                    }
                    if (action != null)
                    {
                        action(); // Run the action specified.
                    }
                }
            }
            else // projectile is returning to player
            {
                num10 = (float)((int)((double)num10 * 0.8));
                num6 *= 1.5f;
                projectile.tileCollide = false;
                Vector2 vector8 = Main.player[projectile.owner].position - projectile.Center;
                float num12 = vector8.Length();
                if (num12 < num6 + 10f || num12 == 0f)
                {
                    projectile.Kill();
                }
                else
                {
                    vector8.Normalize();
                    vector8 *= num6;
                    projectile.velocity = (projectile.velocity * (num10 - 1f) + vector8) / num10;
                }
            }
            projectile.rotation += rotationSpeed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">Index of the projectile in the Main.projecile array.</param>
        /// <param name="protractSpeed">The speed at which the spear shoots outward (protracts).</param>
        /// <param name="retractSpeed">The speed at which the spear shoots back (retracts).</param>
        /// <param name="action">An action to perform when the projectile is out in front of the player.</param>
        /// <param name="initialize">An action to perform once when initializing.</param>
        public static void SpearAI(int index, float protractSpeed = 1.5F, float retractSpeed = 1.4F, ExtraAction action = null, ExtraAction initialize = null)
        {
            Projectile projectile = Main.projectile[index];

            if (initialize != null && projectile.localAI[1] == 0)
            {
                projectile.localAI[1] = 1;
                initialize();
            }

            Vector2 vector21 = Main.player[projectile.owner].RotatedRelativePoint(Main.player[projectile.owner].MountedCenter, true);
            projectile.direction = Main.player[projectile.owner].direction;
            Main.player[projectile.owner].heldProj = projectile.whoAmI;
            Main.player[projectile.owner].itemTime = Main.player[projectile.owner].itemAnimation;
            projectile.position.X = vector21.X - (float)(projectile.width / 2);
            projectile.position.Y = vector21.Y - (float)(projectile.height / 2);
            if (!Main.player[projectile.owner].frozen)
            {
                if (projectile.ai[0] == 0f)
                {
                    projectile.ai[0] = 3f;
                    projectile.netUpdate = true;
                }
                if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 3)
                {
                    projectile.ai[0] -= retractSpeed;
                }
                else
                {
                    projectile.ai[0] += protractSpeed;
                }
            }
            projectile.position += projectile.velocity * projectile.ai[0];
            if (Main.player[projectile.owner].itemAnimation == 0)
            {
                projectile.Kill();
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= 1.57f;
            }

            if (action != null)
            {
                action(); // Run the action specified.
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">Index of the projectile in the Main.projecile array.</param>
        /// <param name="initialRange">The range of the initial shoot of the flail.</param>
        /// <param name="weaponOutRange">The range of the flail once it has been shot out.</param>
        /// <param name="retractRange">The range at which the flail starts to direct towards the player.</param>
        /// <param name="action">An action to perform when the projectile is out in front of the player.</param>
        /// <param name="initalize">An action to perform once when initializing.</param>
        public static void FlailAI(int index, float initialRange = 160, float weaponOutRange = 300, float retractRange = 100, ExtraAction action = null, ExtraAction initialize = null)
        {
            Projectile projectile = Main.projectile[index];

            if (initialize != null && projectile.localAI[1] == 0)
            {
                projectile.localAI[1] = 1;
                initialize();
            }

            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return;
            }
            Main.player[projectile.owner].itemAnimation = 10;
            Main.player[projectile.owner].itemTime = 10;
            if (projectile.position.X + (float)(projectile.width / 2) > Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
            {
                Main.player[projectile.owner].ChangeDir(1);
                projectile.direction = 1;
            }
            else
            {
                Main.player[projectile.owner].ChangeDir(-1);
                projectile.direction = -1;
            }
            Vector2 mountedCenter2 = Main.player[projectile.owner].MountedCenter;
            Vector2 vector18 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num204 = mountedCenter2.X - vector18.X;
            float num205 = mountedCenter2.Y - vector18.Y;
            float num206 = (float)Math.Sqrt((double)(num204 * num204 + num205 * num205));
            if (projectile.ai[0] == 0f)
            {
                float num207 = initialRange;
                projectile.tileCollide = true;
                if (num206 > num207)
                {
                    projectile.ai[0] = 1f;
                    projectile.netUpdate = true;
                }
                else if (!Main.player[projectile.owner].channel)
                {
                    if (projectile.velocity.Y < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y * 0.9f;
                    }
                    projectile.velocity.Y = projectile.velocity.Y + 1f;
                    projectile.velocity.X = projectile.velocity.X * 0.9f;
                }
            }
            else if (projectile.ai[0] == 1f)
            {
                float num208 = 14f / Main.player[projectile.owner].meleeSpeed;
                float num209 = 0.9f / Main.player[projectile.owner].meleeSpeed;
                float num210 = weaponOutRange;
                Math.Abs(num204);
                Math.Abs(num205);
                if (projectile.ai[1] == 1f)
                {
                    projectile.tileCollide = false;
                }
                if (!Main.player[projectile.owner].channel || num206 > num210 || !projectile.tileCollide)
                {
                    projectile.ai[1] = 1f;
                    if (projectile.tileCollide)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.tileCollide = false;
                    if (num206 < 20f)
                    {
                        projectile.Kill();
                    }
                }
                if (!projectile.tileCollide)
                {
                    num209 *= 2f;
                }
                int num211 = (int)retractRange;
                if (num206 > (float)num211 || !projectile.tileCollide)
                {
                    num206 = num208 / num206;
                    num204 *= num206;
                    num205 *= num206;
                    new Vector2(projectile.velocity.X, projectile.velocity.Y);
                    float num212 = num204 - projectile.velocity.X;
                    float num213 = num205 - projectile.velocity.Y;
                    float num214 = (float)Math.Sqrt((double)(num212 * num212 + num213 * num213));
                    num214 = num209 / num214;
                    num212 *= num214;
                    num213 *= num214;
                    projectile.velocity.X = projectile.velocity.X * 0.98f;
                    projectile.velocity.Y = projectile.velocity.Y * 0.98f;
                    projectile.velocity.X = projectile.velocity.X + num212;
                    projectile.velocity.Y = projectile.velocity.Y + num213;
                }
                else
                {
                    if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 6f)
                    {
                        projectile.velocity.X = projectile.velocity.X * 0.96f;
                        projectile.velocity.Y = projectile.velocity.Y + 0.2f;
                    }
                    if (Main.player[projectile.owner].velocity.X == 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X * 0.96f;
                    }
                }
            }
            if (projectile.velocity.X < 0f)
            {
                projectile.rotation -= (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f;
            }
            else
            {
                projectile.rotation += (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y)) * 0.01f;
            }

            if (action != null)
            {
                action(); // Run the action specified.
            }
        }

        public static bool FlailTileCollide(int index, Vector2 oldVelocity)
        {
            Projectile projectile = Main.projectile[index];

            bool flag9 = false;
            if (oldVelocity.X != projectile.velocity.X)
            {
                if (Math.Abs(oldVelocity.X) > 4f)
                {
                    flag9 = true;
                }
                projectile.velocity.X = -oldVelocity.X * 0.2f;
            }
            if (oldVelocity.Y != projectile.velocity.Y)
            {
                if (Math.Abs(oldVelocity.Y) > 4f)
                {
                    flag9 = true;
                }
                projectile.velocity.Y = -oldVelocity.Y * 0.2f;
            }
            projectile.ai[0] = 1f;
            if (flag9)
            {
                projectile.netUpdate = true;
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">Index of the projectile in the Main.projecile array.</param>
        /// <param name="retractTime">The time (in ticks) before the boomerang returns to its owner.</param>
        /// <param name="speed">A speed modifier for the boomerang.</param>
        /// <param name="speedAcceleration">The speed at which the boomerang comes back to the player.</param>
        /// <param name="action">An action to perform when the projectile is out in front of the player.</param>
        /// <param name="initalize">An action to perform once when initializing.</param>
        public static void BoomerangAI(int index, float retractTime = 30, float speed = 9, float speedAcceleration = 0.4F, ExtraAction action = null, ExtraAction initialize = null)
        {
            Projectile projectile = Main.projectile[index];

            if (initialize != null && projectile.localAI[1] == 0)
            {
                projectile.localAI[1] = 1;
                initialize();
            }

            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = 8;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 7);
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= retractTime)
                {
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
            }
            else
            {
                projectile.tileCollide = false;

                Vector2 projectileCenter = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float dirX = Main.player[projectile.owner].position.X + (Main.player[projectile.owner].width / 2) - projectileCenter.X;
                float dirY = Main.player[projectile.owner].position.Y + (Main.player[projectile.owner].height / 2) - projectileCenter.Y;
                float length = (float)Math.Sqrt((double)(dirX * dirX + dirY * dirY));

                // Projectile reached max range, destroy it.
                if (length > 3000f) 
                {
                    projectile.Kill();
                }

                length = speed / length;
                dirX *= length;
                dirY *= length;

                if (projectile.velocity.X < dirX)
                {
                    projectile.velocity.X = projectile.velocity.X + speedAcceleration;
                    if (projectile.velocity.X < 0f && dirX > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + speedAcceleration;
                    }
                }
                else if (projectile.velocity.X > dirX)
                {
                    projectile.velocity.X = projectile.velocity.X - speedAcceleration;
                    if (projectile.velocity.X > 0f && dirX < 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X - speedAcceleration;
                    }
                }
                if (projectile.velocity.Y < dirY)
                {
                    projectile.velocity.Y = projectile.velocity.Y + speedAcceleration;
                    if (projectile.velocity.Y < 0f && dirY > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + speedAcceleration;
                    }
                }
                else if (projectile.velocity.Y > dirY)
                {
                    projectile.velocity.Y = projectile.velocity.Y - speedAcceleration;
                    if (projectile.velocity.Y > 0f && dirY < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - speedAcceleration;
                    }
                }

                if (Main.myPlayer == projectile.owner)
                {
                    Rectangle projectileCol = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    Rectangle playerCol = new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height);
                    if (projectileCol.Intersects(playerCol))
                    {
                        projectile.Kill();
                    }
                }
            }
            projectile.rotation += 0.4f * projectile.direction;

            if (action != null)
            {
                action(); // Run the action specified.
            }
        }

        public static bool BoomerangTileCollide(int index, Vector2 oldVelocity)
        {
            Projectile projectile = Main.projectile[index];
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);

            projectile.ai[0] = 1f;
            projectile.velocity.X = -oldVelocity.X;
            projectile.velocity.Y = -oldVelocity.Y;

            projectile.netUpdate = true;
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);

            return false;
        }
        public static void BoomerangOnHitEntity(int index)
        {
            Projectile projectile = Main.projectile[index];
            if (projectile.ai[0] == 0f)
            {
                projectile.velocity.X = -projectile.velocity.X;
                projectile.velocity.Y = -projectile.velocity.Y;
                projectile.netUpdate = true;
            }
            projectile.ai[0] = 1f;
        }
    }
}

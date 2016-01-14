using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class UnscratchedWispBeam_Friendly : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Unscratched Wisp Beam";
            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.tileCollide = false;
        }

        public override bool PreAI()
        {
            Vector2? vector68 = null;
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }

            if (!Main.projectile[(int)projectile.ai[1]].active || Main.projectile[(int)projectile.ai[1]].type != mod.ProjectileType("UnscratchedWispHandle_Friendly"))
            {
                projectile.Kill();
                return false;
            }
            float num802 = (float)((int)projectile.ai[0]) - 2.5f;
            Vector2 value35 = Vector2.Normalize(Main.projectile[(int)projectile.ai[1]].velocity);
            Projectile proj = Main.projectile[(int)projectile.ai[1]];
            float num803 = num802 * 0.5235988f;
            Vector2 value36 = Vector2.Zero;
            float num804;
            float y;
            float num805;
            float scaleFactor6 = 2;
            if (proj.ai[0] < 180f)
            {
                num804 = 1f - proj.ai[0] / 180f;
                y = 20f - proj.ai[0] / 180f * 14f;
                if (proj.ai[0] < 120f)
                {
                    num805 = 20f - 4f * (proj.ai[0] / 120f);
                    projectile.Opacity = proj.ai[0] / 120f * 0.4f;
                }
                else
                {
                    num805 = 16f - 10f * ((proj.ai[0] - 120f) / 60f);
                    projectile.Opacity = 0.4f + (proj.ai[0] - 120f) / 60f * 0.6f;
                }
            }
            else
            {
                num804 = 0f;
                num805 = 1.75f;
                y = 6f;
                projectile.Opacity = 1f;
            }
            float num806 = (proj.ai[0] + num802 * num805) / (num805 * 6f) * 6.28318548f;
            num803 = Vector2.UnitY.RotatedBy((double)num806, default(Vector2)).Y * 0.5235988f * num804;
            value36 = (Vector2.UnitY.RotatedBy((double)num806, default(Vector2)) * new Vector2(4f, y)).RotatedBy((double)proj.velocity.ToRotation(), default(Vector2));
            projectile.position = proj.Center + value35 * 16f - projectile.Size / 2f + new Vector2(0f, -Main.projectile[(int)projectile.ai[1]].gfxOffY);
            //projectile.position += proj.velocity.ToRotation().ToRotationVector2() * scaleFactor6;
            //projectile.position += value36;
            projectile.velocity = Vector2.Normalize(proj.velocity).RotatedBy((double)num803, default(Vector2));
            projectile.scale = 1.4f * (1f - num804);
            projectile.damage = proj.damage;
            if (!Collision.CanHitLine(Main.player[projectile.owner].Center, 0, 0, proj.Center, 0, 0))
            {
                vector68 = new Vector2?(Main.player[projectile.owner].Center);
            }
            projectile.friendly = (proj.ai[0] > 30f);

            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }

            float num810 = projectile.velocity.ToRotation();
            projectile.rotation = num810 - 1.57079637f;
            projectile.velocity = num810.ToRotationVector2();
            float num811 = 2f;
            float scaleFactor7 = 0f;
            Vector2 value37 = projectile.Center;
            if (vector68.HasValue)
            {
                value37 = vector68.Value;
            } 
            float[] array3 = new float[(int)num811];
            int num812 = 0;
            while ((float)num812 < num811)
            {
                float num813 = (float)num812 / (num811 - 1f);
                Vector2 value38 = value37 + projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * (num813 - 0.5f) * scaleFactor7 * projectile.scale;
                int num814 = (int)value38.X / 16;
                int num815 = (int)value38.Y / 16;
                Vector2 vector69 = value38 + projectile.velocity * 16f * 150f;
                int num816 = (int)vector69.X / 16;
                int num817 = (int)vector69.Y / 16;
                Tuple<int, int> tuple;
                float num818;
                if (!Collision.TupleHitLine(num814, num815, num816, num817, 0, 0, new List<Tuple<int, int>>(), out tuple))
                {
                    num818 = new Vector2((float)Math.Abs(num814 - tuple.Item1), (float)Math.Abs(num815 - tuple.Item2)).Length() * 16f;
                }
                else if (tuple.Item1 == num816 && tuple.Item2 == num817)
                {
                    num818 = 2400f;
                }
                else
                {
                    num818 = new Vector2((float)Math.Abs(num814 - tuple.Item1), (float)Math.Abs(num815 - tuple.Item2)).Length() * 16f;
                }
                array3[num812] = num818;
                num812++;
            }
            float num819 = 0f;
            for (int num820 = 0; num820 < array3.Length; num820++)
            {
                num819 += array3[num820];
            }
            num819 /= num811;
            float amount = 0.75f;
            projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num819, amount);

            float prismHue = projectile.GetPrismHue(projectile.ai[0]);
            Color color = Main.hslToRgb(prismHue, 1f, 0.5f);
            color.A = 0;
            Vector2 vector78 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14.5f * projectile.scale);
            float x = Main.rgbToHsl(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)).X;
            for (int num841 = 0; num841 < 2; num841++)
            {
                float num842 = projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                float num843 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                Vector2 vector79 = new Vector2((float)Math.Cos((double)num842) * num843, (float)Math.Sin((double)num842) * num843);
                int num844 = Dust.NewDust(vector78, 0, 0, 267, vector79.X, vector79.Y, 0, default(Color), 1f);
                Main.dust[num844].color = color;
                Main.dust[num844].scale = 1.2f;
                if (projectile.scale > 1f)
                {
                    Main.dust[num844].velocity *= projectile.scale;
                    Main.dust[num844].scale *= projectile.scale;
                }
                Main.dust[num844].noGravity = true;
                if (projectile.scale != 1.4f)
                {
                    Dust dust102 = Dust.CloneDust(Main.dust[num844]);
                    dust102.color = Color.White;
                    dust102.scale /= 2f;
                }
                float hue = (x + Main.rand.NextFloat() * 0.4f) % 1f;
                Main.dust[num844].color = Color.Lerp(color, Main.hslToRgb(hue, 1f, 0.75f), projectile.scale / 1.4f);
            }
            if (Main.rand.Next(5) == 0)
            {
                Vector2 value43 = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)projectile.width;
                int num845 = Dust.NewDust(vector78 + value43 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num845].velocity *= 0.5f;
                Main.dust[num845].velocity.Y = -Math.Abs(Main.dust[num845].velocity.Y);
            }
            DelegateMethods.v3_1 = color.ToVector3() * 0.3f;
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], (float)projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float num3 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], 22f * projectile.scale, ref num3))
            {
                return true;
            }
            return false;
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.velocity == Vector2.Zero)
                return false;
            Texture2D tex = Main.projectileTexture[projectile.type];
            float num2 = projectile.localAI[1];
            Color color1 = Main.hslToRgb(projectile.GetPrismHue(projectile.ai[0]), 1f, 0.5f);
            color1.A = (byte)0;
            Vector2 vector2_1 = Utils.Floor(projectile.Center) + projectile.velocity * projectile.scale * 10.5f;
            float num3 = num2 - projectile.scale * 14.5f * projectile.scale;
            Vector2 scale = new Vector2(projectile.scale);
            DelegateMethods.f_1 = 1f;
            DelegateMethods.c_1 = color1 * 0.75f * projectile.Opacity;
            Vector2 vector2_2 = projectile.oldPos[0] + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Utils.DrawLaser(Main.spriteBatch, tex, vector2_1 - Main.screenPosition, vector2_1 + projectile.velocity * num3 - Main.screenPosition, scale, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw));
            DelegateMethods.c_1 = new Microsoft.Xna.Framework.Color((int)byte.MaxValue, (int)byte.MaxValue, (int)byte.MaxValue, (int)sbyte.MaxValue) * 0.75f * projectile.Opacity;
            Utils.DrawLaser(Main.spriteBatch, tex, vector2_1 - Main.screenPosition, vector2_1 + projectile.velocity * num3 - Main.screenPosition, scale / 2f, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw));
            return false;
        }
    }
}

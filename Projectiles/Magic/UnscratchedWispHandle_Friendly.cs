﻿using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class UnscratchedWispHandle_Friendly : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Unscratched Wisp Beam";
            projectile.width = 14;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.ignoreWater = true;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            
            float num26 = 30f;
            if (projectile.ai[0] > 90f)
            {
                num26 = 15f;
            }
            if (projectile.ai[0] > 120f)
            {
                num26 = 5f;
            }
            projectile.damage = (int)((float)player.inventory[player.selectedItem].damage * player.magicDamage);
            projectile.ai[0]++;
            projectile.ai[1]++;
            bool flag9 = false;
            if (projectile.ai[0] % num26 == 0f)
            {
                flag9 = true;
            }
            int num27 = 10;
            bool flag10 = false;
            if (projectile.ai[0] % num26 == 0f)
            {
                flag10 = true;
            }
            if (projectile.ai[1] >= 1f)
            {
                projectile.ai[1] = 0f;
                flag10 = true;
                if (Main.myPlayer == projectile.owner)
                {
                    float scaleFactor5 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    Vector2 value12 = vector;
                    Vector2 value13 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - value12;
                    if (player.gravDir == -1f)
                    {
                        value13.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - value12.Y;
                    }
                    Vector2 vector11 = Vector2.Normalize(value13);
                    if (float.IsNaN(vector11.X) || float.IsNaN(vector11.Y))
                    {
                        vector11 = -Vector2.UnitY;
                    }
                    vector11 = Vector2.Normalize(Vector2.Lerp(vector11, Vector2.Normalize(projectile.velocity), 0.92f));
                    vector11 *= scaleFactor5;
                    if (vector11.X != projectile.velocity.X || vector11.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector11;
                }
            }

            projectile.frameCounter++;
            int num28 = (projectile.ai[0] < 120f) ? 4 : 1;
            if (projectile.soundDelay <= 0)
            {
                projectile.soundDelay = num27;
                projectile.soundDelay *= 2;
                if (projectile.ai[0] != 1f)
                {
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 15);
                }
            }
            if (flag10 && Main.myPlayer == projectile.owner)
            {
                bool flag11 = !flag9 || player.CheckMana(player.inventory[player.selectedItem].mana, true, false);
                bool flag12 = player.channel && flag11 && !player.noItems && !player.CCed;
                if (flag12)
                {
                    if (projectile.ai[0] == 1f)
                    {
                        Vector2 center3 = projectile.Center;
                        Vector2 vector12 = Vector2.Normalize(projectile.velocity);
                        if (float.IsNaN(vector12.X) || float.IsNaN(vector12.Y))
                        {
                            vector12 = -Vector2.UnitY;
                        }
                        int num29 = projectile.damage;
                        Projectile.NewProjectile(center3.X, center3.Y, vector12.X, vector12.Y, mod.ProjectileType("UnscratchedWispBeam_Friendly"),
                                num29, projectile.knockBack, projectile.owner, 0, (float)projectile.whoAmI);
                        projectile.netUpdate = true;
                    }
                }
                else
                {
                    projectile.Kill();
                }
            }

            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
    }
}

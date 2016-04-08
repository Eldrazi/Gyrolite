using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Pets
{
    public class DwarfFeederHead : ModProjectile
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
            projectile.netImportant = true;

            Main.projPet[projectile.type] = true;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            if (!player.active)
            {
                projectile.active = false;
                return false; ;
            }

            GyrolitePlayer modPlayer = (GyrolitePlayer)player.GetModPlayer(mod, "GyrolitePlayer");
            if (player.dead)
            {
                modPlayer.dwarfFeederPet = false;
                modPlayer.petKillStack = 0;
            }
            if (modPlayer.dwarfFeederPet)
            {
                if (modPlayer.petKillStack >= 12)
                {
                    int amount = Main.rand.Next(1, 3);
                    Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, ItemID.CursedFlame, amount);
                    modPlayer.petKillStack = 0;
                }
                projectile.timeLeft = 2;
            }

            if (Main.netMode != 1)
            {
                if (projectile.ai[0] == 0)
                {
                    int lastProj = projectile.whoAmI;
                    for (int i = 0; i < 6; ++i)
                    {
                        int newProj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("DwarfFeederBody"), projectile.damage, projectile.knockBack, projectile.owner, lastProj);
                        lastProj = newProj;
                    }
                    int newTailProj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("DwarfFeederTail"), projectile.damage, projectile.knockBack, projectile.owner, lastProj);
                    projectile.ai[0] = 1;
                }
            }

            int minTileX = (int)(projectile.position.X / 16) - 1;
            int maxTileX = (int)((projectile.position.X + projectile.width) / 16.0) + 2;
            int minTileY = (int)(projectile.position.Y / 16.0) - 1;
            int maxTileY = (int)((projectile.position.Y + projectile.height) / 16.0) + 2;

            if (minTileX < 0)
                minTileX = 0;
            if (maxTileX > Main.maxTilesX)
                maxTileX = Main.maxTilesX;
            if (minTileY < 0)
                minTileY = 0;
            if (maxTileY > Main.maxTilesY)
                maxTileY = Main.maxTilesY;

            bool collision = false;
            for (int x  = minTileX; x < maxTileX; x ++)
            {
                for (int y = minTileY; y < maxTileY; y++)
                {
                    if (Main.tile[x, y] != null && (Main.tile[x, y].nactive() && (Main.tileSolid[Main.tile[x, y].type] || Main.tileSolidTop[Main.tile[x, y].type] && Main.tile[x, y].frameY == 0) || Main.tile[x, y].liquid > 64))
                    {
                        Vector2 pos;
                        pos.X = x * 16;
                        pos.Y = y * 16;
                        if (projectile.position.X + projectile.width > pos.X && projectile.position.X < pos.X + 16 && 
                            projectile.position.Y + projectile.height > pos.Y && projectile.position.Y < pos.Y + 16)
                        {
                            collision = true;
                        }
                    }
                }
            }

            if (!collision)
            {
                Rectangle hitbox = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                int hitboxCheck = 160;
                bool notInRange = true;
                if (player.active && !player.dead)
                {
                    Rectangle playerHitbox = new Rectangle((int)player.position.X - hitboxCheck, (int)player.position.Y - hitboxCheck, hitboxCheck * 2, hitboxCheck * 2);
                    if (hitbox.Intersects(playerHitbox))
                    {
                        notInRange = false;
                    }
                }
                if (notInRange)
                    collision = true;
            }

            float maxVelocity = 8f;
            float acceleration = 0.07f;
            Vector2 center = new Vector2(projectile.position.X + (projectile.width * 0.5F), projectile.position.Y + (projectile.height * 0.5F));
            Vector2 ownerCenter = new Vector2(player.position.X + (player.width * 0.5F), player.position.Y + (player.height * 0.5F));

            float targetX = (int)(ownerCenter.X / 16) * 16;
            float targetY = (int)(ownerCenter.Y / 16) * 16;
            center.X = (int)(center.X / 16) * 16;
            center.Y = (int)(center.Y / 16) * 16;
            float dirX = targetX - center.X;
            float dirY = targetY - center.Y;

            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

            if (!collision)
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.11f;
                if (projectile.velocity.Y > maxVelocity)
                    projectile.velocity.Y = maxVelocity;
                if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < maxVelocity * 0.4)
                {
                    if (projectile.velocity.X < 0.0)
                        projectile.velocity.X = projectile.velocity.X - acceleration * 1.1f;
                    else
                        projectile.velocity.X = projectile.velocity.X + acceleration * 1.1f;
                }
                else if (projectile.velocity.Y == maxVelocity)
                {
                    if (projectile.velocity.X < maxVelocity)
                        projectile.velocity.X = projectile.velocity.X + acceleration;
                    else if (projectile.velocity.X > maxVelocity)
                        projectile.velocity.X = projectile.velocity.X - acceleration;
                }
                else if (projectile.velocity.Y > 4.0)
                {
                    if (projectile.velocity.X < 0.0)
                        projectile.velocity.X = projectile.velocity.X + acceleration * 0.9f;
                    else
                        projectile.velocity.X = projectile.velocity.X - acceleration * 0.9f;
                }
            }
            else
            {
                float num2 = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float num3 = Math.Abs(dirX);
                float num12 = Math.Abs(dirY);
                float num13 = maxVelocity / num2;
                float num14 = dirX * num13;
                float num15 = dirY * num13;

                if (projectile.velocity.X > 0.0 && num14 > 0.0 || projectile.velocity.X < 0.0 && num14 < 0.0 || (projectile.velocity.Y > 0.0 && num15 > 0.0 || projectile.velocity.Y < 0.0 && num15 < 0.0))
                {
                    if (projectile.velocity.X < num14)
                        projectile.velocity.X = projectile.velocity.X + acceleration;
                    else if (projectile.velocity.X > num14)
                        projectile.velocity.X = projectile.velocity.X - acceleration;
                    if (projectile.velocity.Y < num15)
                        projectile.velocity.Y = projectile.velocity.Y + acceleration;
                    else if (projectile.velocity.Y > num15)
                        projectile.velocity.Y = projectile.velocity.Y - acceleration;
                    if (Math.Abs(num15) < maxVelocity * 0.2 && (projectile.velocity.X > 0.0 && num14 < 0.0 || projectile.velocity.X < 0.0 && num14 > 0.0))
                    {
                        if (projectile.velocity.Y > 0.0)
                            projectile.velocity.Y = projectile.velocity.Y + acceleration * 2f;
                        else
                            projectile.velocity.Y = projectile.velocity.Y - acceleration * 2f;
                    }
                    if (Math.Abs(num14) < maxVelocity * 0.2 && (projectile.velocity.Y > 0.0 && num15 < 0.0 || projectile.velocity.Y < 0.0 && num15 > 0.0))
                    {
                        if (projectile.velocity.X > 0.0)
                            projectile.velocity.X = projectile.velocity.X + acceleration * 2f;
                        else
                            projectile.velocity.X = projectile.velocity.X - acceleration * 2f;
                    }
                }
                else if (num3 > num12)
                {
                    if (projectile.velocity.X < num14)
                        projectile.velocity.X = projectile.velocity.X + acceleration * 1.1f;
                    else if (projectile.velocity.X > num14)
                        projectile.velocity.X = projectile.velocity.X - acceleration * 1.1f;
                    if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < maxVelocity * 0.5)
                    {
                        if (projectile.velocity.Y > 0.0)
                            projectile.velocity.Y = projectile.velocity.Y + acceleration;
                        else
                            projectile.velocity.Y = projectile.velocity.Y - acceleration;
                    }
                }
                else
                {
                    if (projectile.velocity.Y < num15)
                        projectile.velocity.Y = projectile.velocity.Y + acceleration * 1.1f;
                    else if (projectile.velocity.Y > num15)
                        projectile.velocity.Y = projectile.velocity.Y - acceleration * 1.1f;
                    if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < maxVelocity * 0.5)
                    {
                        if (projectile.velocity.X > 0.0)
                            projectile.velocity.X = projectile.velocity.X + acceleration;
                        else
                            projectile.velocity.X = projectile.velocity.X - acceleration;
                    }
                }
            }

            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;

            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            ProjectileDrawing.DrawAroundOrigin(projectile.whoAmI, spriteBatch, lightColor);
            return false;
        }
    }
}

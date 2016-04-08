using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Summoner
{
    public class BloodyGeyserMinion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Bloody Geyser";
            projectile.width = 26;
            projectile.height = 32;

            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.alpha = 0;
            Main.projFrames[projectile.type] = 3;

            projectile.timeLeft *= 5;
            projectile.netImportant = true;
            projectile.minion = true;
            projectile.minionSlots = 1;

            projectile.ignoreWater = true;
        }

        public override bool PreAI()
        {
            GyrolitePlayer gp = (GyrolitePlayer)Main.player[projectile.owner].GetModPlayer(mod, "GyrolitePlayer");
            if (gp.player.dead)
            {
                gp.bloodyGeyserMinion = false;
            }
            if (gp.bloodyGeyserMinion)
            {
                projectile.timeLeft = 2;
            }

            projectile.ai[0]++;
            if (projectile.ai[0] >= 60)
            {
                projectile.frame++;
                projectile.ai[0] = 0;

                if (projectile.frame == Main.projFrames[projectile.type])
                {
                    int randAmount = Main.rand.Next(9, 17);
                    for (int i = 0; i < randAmount; ++i)
                    {
                        int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("BloodyGeyserProjectile"), projectile.damage, 0, projectile.owner, Main.rand.Next(0, 3));
                        Main.projectile[proj].position.X += Main.rand.Next(-10, 11);
                        Main.projectile[proj].position.Y += Main.rand.Next(-10, 11);
                        if (Main.projectile[proj].position != projectile.Center)
                        {
                            Main.projectile[proj].velocity = projectile.DirectionTo(Main.projectile[proj].position) * Main.rand.Next(4, 9);
                        }
                        Main.projectile[proj].localAI[0] = Main.rand.Next(-10, 11) / 80;
                    }

                    projectile.frame = 0;
                }
            }

            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return false;
        }

        public override void TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 26;
            height = projectile.height;
            fallThrough = false;
        }
        public override bool OnTileCollide(Microsoft.Xna.Framework.Vector2 oldVelocity)
        {
            return false;
        }
    }
}

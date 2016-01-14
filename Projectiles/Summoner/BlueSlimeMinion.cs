using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Summoner
{
    public class BlueSlimeMinion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Blue Slime Minion";
            projectile.width = 34;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.alpha = 75;
            Main.projFrames[projectile.type] = 6;

            projectile.timeLeft *= 5;
            projectile.netImportant = true;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.aiStyle = 26;
            aiType = 266;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class GreekFire : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Greek Fire";
            projectile.width = 14;
            projectile.height = 16;

            projectile.aiStyle = 14;
            aiType = 326;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 360;
        }
    }
}

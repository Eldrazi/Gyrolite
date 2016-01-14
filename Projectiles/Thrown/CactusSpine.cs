using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Thrown
{
    public class CactusSpine : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Cactus Spine";
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 14;
            aiType = 24;
            projectile.friendly = true;
            projectile.penetrate = 6;
            projectile.thrown = true;
            projectile.timeLeft = 600;
        }
    }
}

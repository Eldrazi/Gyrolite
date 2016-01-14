using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class VacuumHole : VacuumVortex
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.scale = 1;

            pullForce = 0.75F;
            pullDistance = 80;
        }
    }
}

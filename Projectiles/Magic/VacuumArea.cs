using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Magic
{
    public class VacuumArea : VacuumVortex
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.scale = 3;

            pullForce = 0.25F;
            pullDistance = 240;
        }
    }
}

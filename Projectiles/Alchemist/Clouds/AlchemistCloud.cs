using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Alchemist.Clouds
{
    public class AlchemistCloud : ModProjectile
    {
        protected int AoESizeX, AoESizeY;

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "Gyrolite/Projectiles/Alchemist/Clouds/EmptyCloud";
            return base.Autoload(ref name, ref texture);
        }

        public override void SetDefaults()
        {
            projectile.name = "Alchemist Cloud";
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.knockBack = 0;
            projectile.thrown = true;
            projectile.damage = 0;
        }

        public override void AI()
        {
            projectile.tileCollide = false;
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = AoESizeX;
            projectile.height = AoESizeY;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
            this.AoEEffect();
        }

        public virtual void AoEEffect() {}
    }
}

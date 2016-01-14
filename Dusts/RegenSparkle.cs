using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Dusts
{
    public class RegenSparkle : ModDust
    {
        int rotateDir;
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.velocity.X = 0;
            dust.velocity.Y = -0.5F;
            rotateDir = Main.rand.Next(-1, 2);
        }

        public override bool Update(Dust dust)
        {
            dust.rotation += 0.03F * rotateDir;
            dust.velocity.Y -= 0.03F; 
            dust.position += dust.velocity;
            dust.scale -= 0.01f;
            if (dust.scale <= 0.5f)
            {
                dust.active = false;
            }
            return false;
        }
    }
}

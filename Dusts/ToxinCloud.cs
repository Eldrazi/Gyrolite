using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Dusts
{
    public class ToxinCloud : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.frame.X = Main.rand.Next(2) * 8;
            dust.frame.Y = Main.rand.Next(2) * 8;
            dust.noLight = true;
            dust.scale = Main.rand.Next(1, 4);
        }

        public override bool Update(Dust dust)
        {
            dust.scale *= 0.98F;
            if (dust.scale <= 0.25F)
                dust.active = false;
            return false;
        }
    }
}

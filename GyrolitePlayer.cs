using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite
{
    public class GyrolitePlayer : ModPlayer
    {
        public bool yoyoStringBall = false;
        public override void ResetEffects()
        {
            yoyoStringBall = false;
        }
    }
}

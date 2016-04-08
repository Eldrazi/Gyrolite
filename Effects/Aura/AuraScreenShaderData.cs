using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;

namespace Gyrolite.Effects.Aura
{
    public class AuraScreenShaderData : ScreenShaderData
    {
        public AuraScreenShaderData(string passName) : base(passName)
		{
		}

		public override void Apply()
		{
			UseTargetPosition(Main.player[Main.myPlayer].Center);

			base.Apply();
		}
    }
}

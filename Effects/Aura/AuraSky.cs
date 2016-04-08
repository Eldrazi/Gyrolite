using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

namespace Gyrolite.Effects.Aura
{
    public class AuraSky : CustomSky
    {
        private bool isActive = false;
        private float intensity = 0f;

        public override void Update()
        {
            if (isActive)
            {
                intensity = MathHelper.Clamp(GyroliteWorld.auraTiles - GyroliteWorld.minAuraZoneTiles, 0, 300) / 600;
                Filters.Scene["Gyrolite:Aura"].GetShader().UseOpacity(intensity);
            }
            else if (!isActive && intensity > 0f)
            {
                intensity -= 0.01f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 0 && minDepth < 0)
            {
                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(200, 200, 200) * intensity);                
            }
        }

        public override float GetCloudAlpha()
        {
            return 0f;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override void Reset()
        {
            isActive = false;
        }

        public override bool IsActive()
        {
            return isActive || intensity > 0f;
        }
    }
}

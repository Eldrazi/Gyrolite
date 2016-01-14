using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Projectiles.Summoner
{
    public class HeavySlimeMinion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Heavy Slime Minion";
            projectile.width = 30;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.alpha = 150;
            Main.projFrames[projectile.type] = 6;
            projectile.scale = 1.5F;

            projectile.timeLeft *= 5;
            projectile.netImportant = true;
            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.aiStyle = 26;
            aiType = 266;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D centerTexture = ModLoader.GetTexture("Gyrolite/Projectiles/Summoner/HeavySlimeMinion2");
            Vector2 centerOrigin = new Vector2((centerTexture.Width / 2), 16);
            Vector2 centerPos = new Vector2(projectile.Center.X, projectile.Center.Y);

            Rectangle sourceRectangle = new Rectangle(0, (int)(centerTexture.Height / 6) * projectile.frame, 44, (int)(centerTexture.Height / 6));
            SpriteEffects effects = projectile.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.spriteBatch.Draw(centerTexture, centerPos - Main.screenPosition, sourceRectangle, lightColor, projectile.rotation, centerOrigin, projectile.scale, effects, 0);

            return base.PreDraw(spriteBatch, lightColor);
        }
    }
}

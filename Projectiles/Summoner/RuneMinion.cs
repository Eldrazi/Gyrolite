//using System;

//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework;

//namespace Gyrolite.Projectiles.Summoner
//{
//    public class RuneMinion : ModProjectile
//    {
//        public enum RuneType
//        {
//            Earth = 0,
//            Fire = 1,
//            Water = 2,
//            Air = 3,
//            Shadow = 4,
//            Light = 5,
//            Restoration = 6,
//            Spirit = 7
//        }

//        private float radius = 120;

//        public override void SetDefaults()
//        {
            
//        }

//        public override bool PreAI()
//        {
//            Player player = Main.player[projectile.owner];
		
//            return false;
//        }

//        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
//        {
//            return false;
//        }
//    }
//}

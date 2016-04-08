using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Gyrolite.Tiles.Permafrost
{
    public class PermafrostTree : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileAxe[Type] = true; // Can only be axed down
            Main.tileSolid[Type] = false;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileFrameImportant[Type] = true;

            drop = mod.ItemType("Aurawood");
        }

        public override bool PreDraw(int i, int j, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Vector2 vector2_1 = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
            if (Main.drawToScreen)
                vector2_1 = Vector2.Zero;

            short frameX = Main.tile[i, j].frameX;
            short frameY = Main.tile[i, j].frameY;

            if (frameY >= 198 && frameX >= 22)
            {
                int num7 = 0;
                if ((int)frameX == 22)
                {
                    if ((int)frameY == 220)
                        num7 = 1;
                    else if ((int)frameY == 242)
                        num7 = 2;
                    int width2 = 80;
                    int height = 80;
                    int num8 = 32;
                    int num9 = 0;
                    Texture2D treeTopTexture = mod.GetTexture("Effects/Trees/Permafrost/PermafrostTreetops");
                    Main.spriteBatch.Draw(treeTopTexture, new Vector2((float)(i * 16 - (int)Main.screenPosition.X - num8), (float)(j * 16 - (int)Main.screenPosition.Y - height + 16 + num9)) + vector2_1, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(num7 * (width2 + 2), 0, width2, height)), Lighting.GetColor(i, j), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                }
            }
            else
            {
                int x = i;
                int y = j;
                if ((int)frameX == 66 && (int)frameY <= 45)
                    ++x;
                if ((int)frameX == 88 && (int)frameY >= 66 && (int)frameY <= 110)
                    --x;
                if ((int)frameX == 22 && (int)frameY >= 132)
                    --x;
                if ((int)frameX == 44 && (int)frameY >= 132)
                    ++x;
                while (Main.tile[x, y].active() && (int)Main.tile[x, y].type == 5)
                    ++y;
                Main.spriteBatch.Draw(Main.tileTexture[Type], new Vector2((float)(i * 16 - (int)Main.screenPosition.X) - (float)(((double)20 - 16.0) / 2.0), (float)(j * 16 - (int)Main.screenPosition.Y)) + vector2_1, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int)frameX, (int)frameY, 20, 20)), Lighting.GetColor(i, j), 0.0f, new Vector2(), 1f, SpriteEffects.None, 0.0f);
                    
                return true;
            }
            /*
             * else if ((int) type == 5)
                    {
                      int treeVariant = Main.GetTreeVariant(x, y);
                      else if (Main.canDrawColorTree(index3, index2, treeVariant))
                        Main.spriteBatch.Draw((Texture2D) Main.woodAltTexture[treeVariant, (int) tile1.color()], new Vector2((float) (index3 * 16 - (int) Main.screenPosition.X) - (float) (((double) width1 - 16.0) / 2.0), (float) (index2 * 16 - (int) Main.screenPosition.Y + offsetY)) + vector2_1, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int) num4, (int) num5, width1, height1)), color1, 0.0f, new Vector2(), 1f, spriteEffects, 0.0f);
                      else
                        Main.spriteBatch.Draw(Main.woodTexture[treeVariant], new Vector2((float) (index3 * 16 - (int) Main.screenPosition.X) - (float) (((double) width1 - 16.0) / 2.0), (float) (index2 * 16 - (int) Main.screenPosition.Y + offsetY)) + vector2_1, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle((int) num4, (int) num5, width1, height1)), color1, 0.0f, new Vector2(), 1f, spriteEffects, 0.0f);
                    }*/
            return base.PreDraw(i, j, spriteBatch);
        }
    }
}

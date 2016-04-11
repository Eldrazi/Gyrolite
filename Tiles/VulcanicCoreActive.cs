using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Gyrolite.Tiles
{
    public class VulcanicCoreActive : ModTile
    {
        int curFrame;

        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileLighted[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.CoordinateHeights = new int[] { 18, 18, 18, 18, 18 };
            TileObjectData.addTile(Type);

            animationFrameHeight = 100;
        }

        public override void RightClick(int i, int j)
        {
            int baseX = i - (int)(Main.tile[i, j].frameX / 18);
            int baseY = j - (int)(Main.tile[i, j].frameY / 20);
            for (int x = baseX; x < baseX + 5; ++x)
            {
                for (int y = baseY; y < baseY + 5; ++y)
                {
                    Main.tile[x, y].type = (ushort)mod.TileType("VulcanicCoreInactive");
                }
            }
            for (int x = baseX; x < baseX + 5; ++x)
            {
                for (int y = baseY; y < baseY + 5; ++y)
                {
                    WorldGen.SquareTileFrame(x, y, true);
                }
            }
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter >= 30)
            {
                curFrame = (curFrame + 1) % 4;
                if (curFrame == 3) frame = 1;
                else frame = curFrame;

                frameCounter = 0;
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            switch (curFrame)
            {
                case 0:
                    r = 0.4f;
                    g = 0.2f;
                    b = 0.1f;
                    break;

                case 1: goto case 3;

                case 2:
                    r = 0.6f;
                    g = 0.5f;
                    b = 0.3f;
                    break;

                case 3:
                    r = 0.5f;
                    g = 0.3f;
                    b = 0.2f;
                    break;
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 80, 80, mod.ItemType("VulcanicCoreActive"));
        }
    }
}

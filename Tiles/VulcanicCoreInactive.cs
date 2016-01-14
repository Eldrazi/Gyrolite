using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;

namespace Gyrolite.Tiles
{
    public class VulcanicCoreInactive : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.CoordinateHeights = new int[] { 18, 18, 18, 18, 18 };
            TileObjectData.addTile(Type);
        }

        public override void RightClick(int i, int j)
        {
            int baseX = i - (int)(Main.tile[i, j].frameX / 18);
            int baseY = j - (int)(Main.tile[i, j].frameY / 20);
            for (int x = baseX; x < baseX + 5; ++x)
            {
                for (int y = baseY; y < baseY + 5; ++y)
                {
                    Main.tile[x, y].type = (ushort)mod.TileType("VulcanicCoreSlumber");
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

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 80, 80, mod.ItemType("VulcanicCore"));
        }
    }
}

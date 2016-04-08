using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Tiles.Permafrost
{
    public class PermafrostAsh : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            Main.tileMerge[Type][TileID.Ash] = true;
            Main.tileMerge[TileID.Ash][Type] = true;
            Main.tileMerge[mod.TileType("PermafrostHellstoneBrick")][Type] = true;
            Main.tileMerge[Type][mod.TileType("PermafrostHellstoneBrick")] = true;
            Main.tileMerge[mod.TileType("PermafrostObsidianBrick")][Type] = true;
            Main.tileMerge[Type][mod.TileType("PermafrostObsidianBrick")] = true;

            drop = mod.ItemType("PermafrostAsh");
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                Main.tile[i, j].type = TileID.Ash;
                WorldGen.SquareTileFrame(i, j, true);
                fail = true;
                Item.NewItem(i * 16, j * 16, 16, 16, mod.ItemType("PermafrostCrystal"));
            }
        }
    }
}

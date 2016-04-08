using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Tiles.Permafrost
{
    public class PermafrostObsidianBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            Main.tileMerge[Type][TileID.ObsidianBrick] = true;
            Main.tileMerge[TileID.ObsidianBrick][Type] = true;
            Main.tileMerge[Type][mod.TileType("PermafrostAsh")] = true;
            Main.tileMerge[mod.TileType("PermafrostAsh")][Type] = true;

            drop = mod.ItemType("PermafrostObsidianBrick");
        }
        
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                Main.tile[i, j].type = TileID.ObsidianBrick;
                WorldGen.SquareTileFrame(i, j, true);
                fail = true;
                Item.NewItem(i * 16, j * 16, 16, 16, mod.ItemType("PermafrostCrystal"));
            }
        }
    }
}

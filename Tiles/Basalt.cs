using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Tiles
{
    public class Basalt : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            drop = mod.ItemType("Basalt");
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            if (Main.tile[i, j - 1].type == TileID.ClayBlock)
            {
                Main.tile[i, j].type = (ushort)mod.TileType("MyNewTile");
                WorldGen.SquareTileFrame(i, j, true);
            }

            return base.TileFrame(i, j, ref resetFrame, ref noBreak);
        }
    }
}

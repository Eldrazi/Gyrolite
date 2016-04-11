using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Gyrolite.Tiles.Aura
{
    public class Auragrass : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            TileID.Sets.Conversion.Grass[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;
            AddMapEntry(new Color(200, 200, 200));
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                Main.tile[i, j].type = TileID.Dirt;
                WorldGen.SquareTileFrame(i, j, true);
                NetMessage.SendTileSquare(-1, i, j, 1);

                fail = true;
            }
        }
    }
}

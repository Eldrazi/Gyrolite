﻿using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Tiles
{
    public class AurastoneBrick : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            drop = mod.ItemType("AurastoneBrick");
        }
    }
}

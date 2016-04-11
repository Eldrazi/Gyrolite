using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Accessories
{
    public class FreezingBoots : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Shoes);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Freezing Boots";
            item.width = 18;
            item.height = 18;
            item.toolTip = "?";
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 2;

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.accRunSpeed = 6.75f;
            player.rocketBoots = 3;
            player.moveSpeed += 0.08f;
            player.iceSkate = true;

            if ((player.mount == null || player.mount.Type == 0 || !player.mount.Active) && !player.wet)
            {
                int minX = (int)(player.position.X / 16) - 1;
                int maxX = (int)(player.position.X / 16) + 3;
                int y = (int)(player.position.Y / 16) + 3;
                for (int x = minX; x < maxX; ++x)
                {
                    Tile tile = Main.tile[x, y];
                    if (tile.liquidType() == Tile.Liquid_Water && tile.liquid > 25 && !tile.active())
                    {
                        tile.active(true);
                        tile.type = TileID.BreakableIce;
                        tile.inActive(false);

                        Main.PlaySound(19, (int)player.position.X, (int)player.position.Y, 1);
                        Main.tile[x, y].liquid = 0;

                        WorldGen.SquareTileFrame(x, y, true);
                    }
                }
            }
        }
    }
}

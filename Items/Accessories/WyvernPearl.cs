using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Accessories
{
    public class WyvernPearl : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Pearl of the Wyvern";
            item.width = 18;
            item.height = 18;
            item.toolTip = "Summons a Wyvern into battle";
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = -12;

            item.useStyle = 1;
            item.useTime = 20;
            item.useAnimation = 20;

            item.noMelee = true;

            item.mountType = mod.MountType("Wyvern");

            item.useSound = 79;
        }
    }
}

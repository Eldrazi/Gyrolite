using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Gyrolite.Items.Misc
{
    public class PermafrostCrystal : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Permafrost Crystal";
            item.width = 22;
            item.height = 28;
            item.toolTip = "Iceyh!";
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 5;

            item.maxStack = 999;
        }

        public override DrawAnimation GetAnimation()
        {
            return new DrawAnimationVertical(5, 4);
        }
    }
}

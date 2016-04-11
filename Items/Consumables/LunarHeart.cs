using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Consumables
{
    public class LunarHeart : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Lunar Heart";
            item.width = 12;
            item.height = 12;
            item.toolTip = "?";

            item.useStyle = 4;
            item.useTime = 30;
            item.useAnimation = 30;

            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            GyrolitePlayer gp = player.GetModPlayer<GyrolitePlayer>(mod);
            if (gp.lunarHeartStack < 2)
            {
                return true;
            }
            return false;
        }
        public override bool UseItem(Player player)
        {
            GyrolitePlayer gp = player.GetModPlayer<GyrolitePlayer>(mod);
            player.statLifeMax += 50;
            gp.lunarHeartStack++;

            return true;
        }
    }
}

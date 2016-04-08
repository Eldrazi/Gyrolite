using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Pets
{
    public class IchorScentVial : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Ichor Scent Vial";
            item.width = 16;
            item.height = 24;
            item.toolTip = "Summons a Baby Ichor Sticker";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 3;

            item.damage = 0;
            item.useStyle = 4;
            item.useTime = 20;
            item.useAnimation = 20;

            item.noMelee = true;
            item.buffType = mod.BuffType("BabyIchorStickerBuff");
            item.buffTime = 3600;

            item.useSound = 2;
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}

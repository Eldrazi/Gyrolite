using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Consumables.Alchemist
{
    public class AnkhPotion : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Ankh Potion";
            item.width = 20;
            item.height = 20;
            item.toolTip = "Grants the Ankh buff for 10 minutes";
            item.rare = 3;

            item.maxStack = 99;

            item.useStyle = 2;
            item.useTime = 45;
            item.useAnimation = 45;

            item.buffType = mod.BuffType("AnkhBuff");
            item.buffTime = 36000;

            item.useSound = 2;
            item.consumable = true;
        }
    }
}

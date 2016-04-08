using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Consumables.Alchemist
{
    public class AmplifiedSensePotion : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Potion of Amplified Sense";
            item.width = 20;
            item.height = 20;
            item.toolTip = "Grants the Amplified Sense buff for 10 minutes";
            item.rare = 3;

            item.maxStack = 99;

            item.useStyle = 2;
            item.useTime = 45;
            item.useAnimation = 45;

            item.buffType = mod.BuffType("AmplifiedSenseBuff");
            item.buffTime = 36000;

            item.useSound = 2;
            item.consumable = true;
        }
    }
}

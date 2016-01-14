using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class LightShieldTome : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Light Shield Tome";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Surrounds the player with a shield of light, boosting defence while sapping mobility";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.useTime = 20;
            item.useAnimation = 17;
            item.useStyle = 5;

            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            item.buffType = mod.BuffType("LightShield");
            item.buffTime = 900; // 15 seconds buff timer.

            item.useSound = 20;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Tools
{
    public class GlacialPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Glacial Pickaxe";
            item.width = 24;
            item.height = 28;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 2;

            item.damage = 75;
            item.knockBack = 5.5f;
            item.useStyle = 1;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useTurn = true;
            item.autoReuse = true;
            item.scale = 1.1f;

            item.melee = true;
            item.pick = 150;

            item.useSound = 1;
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

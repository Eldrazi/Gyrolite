using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Melee
{
    public class FrostrunkSword : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Frostrunk Sword";
            item.width = 24;
            item.height = 28;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 2;

            item.damage = 10;
            item.knockBack = 5f;
            item.useStyle = 1;
            item.useTime = 21;
            item.useAnimation = 21;
            item.useTurn = false;
            item.autoReuse = true;
            item.scale = 1f;

            item.melee = true;

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

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Tools
{
    public class FrostrunkHammer : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Frostrunk Hammer";
            item.width = 24;
            item.height = 28;
            item.value = Item.sellPrice(0, 0, 0, 50);
            item.rare = 2;

            item.damage = 4;
            item.knockBack = 5.5f;
            item.useStyle = 1;
            item.useTime = 23;
            item.useAnimation = 33;
            item.useTurn = true;
            item.autoReuse = true;
            item.scale = 1.1f;

            item.melee = true;
            item.hammer = 35;

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

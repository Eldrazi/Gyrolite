using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Thrown
{
    public class CactusDagger : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Cactus Dagger";
            item.width = 18;
            item.height = 20;
            item.toolTip = "A dagger with nasty, sharp cactus points";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.maxStack = 999;
            item.damage = 20;
            item.knockBack = 2;
            item.autoReuse = true;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.noUseGraphic = true;

            item.thrown = true;
            item.noMelee = true;
            item.consumable = true;

            item.shoot = mod.ProjectileType("CactusDagger");
            item.shootSpeed = 10;

            item.useSound = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this, 999);
            recipe.AddRecipe();
        }
    }
}

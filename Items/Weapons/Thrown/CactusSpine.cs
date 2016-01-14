using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Thrown
{
    public class CactusSpine : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Cactus Spine";
            item.width = 10;
            item.height = 10;
            item.toolTip = "Auch..!";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.maxStack = 999;
            item.damage = 20;
            item.knockBack = 1;
            item.autoReuse = true;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.noUseGraphic = true;

            item.thrown = true;
            item.noMelee = true;
            item.consumable = true;

            item.shoot = mod.ProjectileType("CactusSpine");
            item.shootSpeed = 5;

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

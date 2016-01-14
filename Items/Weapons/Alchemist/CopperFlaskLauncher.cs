using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Alchemist
{
    public class CopperFlaskLauncher : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Copper Potion Launcher";
            item.width = 54;
            item.height = 24;
            item.toolTip = "Used to shoot Flask projectiles";
            item.value = Item.sellPrice(0, 2, 0, 0);
            item.rare = 5;

            item.damage = 20;
            item.knockBack = 2;
            item.autoReuse = true;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;

            item.noMelee = true;
            item.ranged = true;

            item.shoot = mod.ProjectileType("IrritationFlask");
            item.shootSpeed = 5;

            item.useAmmo = Gyrolite.flaskAmmo;
            item.useSound = 11;
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

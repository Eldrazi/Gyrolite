using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class ThickboltTome : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Thickbolt Tome";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Shoot a thick and slow bolt of water";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 20;
            item.knockBack = 10F;
            item.useTime = 30;
            item.useAnimation = 17;
            item.useStyle = 5;

            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            item.shoot = mod.ProjectileType("Thickbolt");
            item.shootSpeed = 2f; // The speed of the fired projectile.

            item.useSound = 21;
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

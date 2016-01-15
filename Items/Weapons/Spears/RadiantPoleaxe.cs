using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Gyrolite.Items.Weapons.Spears
{
    public class RadiantPoleaxe : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "RadiantPoleaxe";
            item.width = 40;
            item.height = 40;
            item.toolTip = "A poleaxe imbued with radiant forces.";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 10;

            item.damage = 40;
            item.knockBack = 6.4F;
            item.useStyle = 5;
            item.useTime = 22;
            item.useAnimation = 22;
            item.scale = 1.1f;

            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.shoot = mod.ProjectileType("RadiantPoleaxe");
            item.shootSpeed = 5.6F;

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

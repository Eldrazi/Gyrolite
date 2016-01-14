using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Flails
{
    public class NecroFlail : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Ball o' Bones";
            item.width = 30;
            item.height = 10;
            item.toolTip = "A ball of bones that is held together by mysterious dark forces.";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 10;

            item.damage = 80;
            item.knockBack = 7.5F;
            item.useStyle = 5;
            item.useTime = 40;
            item.useAnimation = 40;
            item.scale = 1.1F;

            item.melee = true;
            item.noMelee = true;
            item.channel = true;
            item.noUseGraphic = true;

            item.shoot = mod.ProjectileType("NecroFlail");
            item.shootSpeed = 12.5F;

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

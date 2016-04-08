using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Flails
{
    public class SoulsticeFlail : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Soulstice Flail";
            item.width = 30;
            item.height = 10;
            item.toolTip = "A flail imbued with the power of soulstice";
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

            item.shoot = mod.ProjectileType("SoulsticeFlail");
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

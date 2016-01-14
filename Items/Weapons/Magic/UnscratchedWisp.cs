using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Gyrolite.Items.Weapons.Magic
{
    public class UnscratchedWisp : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Unscratched Wisp";
            item.width = 16;
            item.height = 16;
            item.toolTip = "Fires a concentrated light beam";
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 5;

            item.damage = 5;
            item.knockBack = 0;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.reuseDelay = 5;

            item.magic = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.mana = 0;

            item.shoot = mod.ProjectileType("UnscratchedWispHandle_Friendly");
            item.shootSpeed = 30f;
        }

        public override DrawAnimation GetAnimation()
        {
            return new DrawAnimationVertical(5, 4);
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

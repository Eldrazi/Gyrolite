using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Gyrolite.Items.Weapons.Alchemist
{
    public class GaseousRegenerativeJar : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Gaseous Regenerative Jar";
            item.width = 28;
            item.height = 36;
            item.toolTip = "A jar that, upon impact, releases a healing cloud";
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 5;

            item.maxStack = 999;
            item.damage = 20;
            item.knockBack = 0;
            item.autoReuse = false;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.noUseGraphic = true;

            item.thrown = true;
            item.noMelee = true;
            item.consumable = true;

            item.shoot = mod.ProjectileType("GaseousRegenerativeJar");
            item.shootSpeed = 12f;

            item.useSound = 1;
        }

        public override DrawAnimation GetAnimation()
        {
            return new DrawAnimationVertical(5, 3);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GaseousRegenerativeMixture");
            recipe.SetResult(this, 999);
            recipe.AddRecipe();
        }
    }
}

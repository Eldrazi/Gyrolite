using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Misc
{
    public class GaseousStaticMixture : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Gaseous Static Mixture";
            item.width = 20;
            item.height = 30;
            item.toolTip = "A mixture that feels sparkly on the touch";
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 5;

            item.maxStack = 999;
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

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Gyrolite.Items.Misc
{
    public class WispSoul : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Wisp Soul";
            item.width = 12;
            item.height = 12;
            item.toolTip = "A wisp soul that is cold to the touch";
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 5;

            item.maxStack = 999;
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

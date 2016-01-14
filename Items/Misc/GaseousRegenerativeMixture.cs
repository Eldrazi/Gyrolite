using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Misc
{
    public class GaseousRegenerativeMixture : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Gaseous Regenerative Mixture";
            item.width = 20;
            item.height = 30;
            item.toolTip = "A mixture that feels tingly on the touch";
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 5;

            item.maxStack = 999;
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(ItemID.StardustHelmet);
            player.QuickSpawnItem(ItemID.StardustBreastplate);
            player.QuickSpawnItem(ItemID.StardustLeggings);
            player.QuickSpawnItem(ItemID.NecromanticScroll);
            player.QuickSpawnItem(ItemID.PapyrusScarab);
            base.RightClick(player);
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

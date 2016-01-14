using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Placeable
{
    public class DarkAurastoneBrick : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Dark Aurastone Brick";
            item.width = 12;
            item.height = 11;
            item.scale = 0.5F;
            item.toolTip = "A dark Aurastone brick.";
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 5;

            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("DarkAurastoneBrick");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Aurastone", 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

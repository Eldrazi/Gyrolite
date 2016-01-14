using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Placeable
{
    public class AurawoodWall : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Aurawood Wall";
            item.width = 12;
            item.height = 12;
            AddTooltip("A wall made out of pure Aurawood.");
            item.rare = 9;

            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 7;
            item.useStyle = 1;
            item.consumable = true;
            item.createWall = mod.WallType("AurawoodWall");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Aurawood", 1);
            recipe.SetResult(this, 4);
            recipe.AddRecipe();
        }
    }
}

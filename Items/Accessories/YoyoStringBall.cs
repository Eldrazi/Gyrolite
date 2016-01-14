using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Accessories
{
    public class YoyoStringBall : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Yoyo String Ball";
            item.width = 22;
            item.height = 24;
            item.toolTip = "A ball of yoyo string.";
            item.value = Item.sellPrice(0, 0, 25, 0);
            item.rare = 5;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player)
        {
            Gyrolite.GetPlayer(player).yoyoStringBall = true;
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

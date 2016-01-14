﻿using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Gyrolite.Items.Placeable
{
    public class MagmaCrystal : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Magma Crystal";
            item.width = 12;
            item.height = 11;
            item.scale = 0.5F;
            item.toolTip = "Hotty!";
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 5;

            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("MagmaCrystal");
        }

        public override DrawAnimation GetAnimation()
        {
            return new DrawAnimationVertical(5, 4);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this, 999);
            recipe.AddRecipe();
        }
    }
}

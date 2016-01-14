using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Yoyos
{
    public class Entruder : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Entruder";
            item.useStyle = 5;
            item.width = 30;
            item.height = 26;
            item.shoot = mod.ProjectileType("Entruder");
            item.noUseGraphic = true;
            item.useSound = 1;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 12f;
            item.damage = 28;
            item.knockBack = 4f;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 3;
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

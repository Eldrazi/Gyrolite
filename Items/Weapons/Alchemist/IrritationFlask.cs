using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Alchemist
{
    public class IrritationFlask : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Irritation Flask";
            item.width = 10;
            item.height = 20;
            item.scale = 0.5F;
            item.toolTip = "A flask which can be shot with a Flask Launcher\nGives enemies the Irritate debuff";
            item.value = Item.sellPrice(0, 0, 0, 50);
            item.rare = 5;

            item.maxStack = 999;
            item.damage = 20;
            item.knockBack = 2;
            item.consumable = true;
            item.ammo = Gyrolite.flaskAmmo;

            item.shoot = mod.ProjectileType("IrritationFlask");
            item.shootSpeed = 10;
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

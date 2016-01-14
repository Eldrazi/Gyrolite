using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class LifeLeechTome : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Life Leech Tome";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Fire a slow moving ball that leeches the life from its victims,\nto return it to its master";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 20;
            item.knockBack = 6f;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;

            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            item.shoot = mod.ProjectileType("LifeLeechSphere");
            item.shootSpeed = 1.2f; // The speed of the fired projectile.

            item.useSound = 20;
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

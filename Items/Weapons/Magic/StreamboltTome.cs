using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class StreamboltTome : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Streambolt Tome";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Shoot a quick ball of water that has a chance to slow its victims";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 8;
            item.knockBack = 2;
            item.autoReuse = true;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;

            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            item.shoot = mod.ProjectileType("Streambolt");
            item.shootSpeed = 16f;

            item.useSound = 21;
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

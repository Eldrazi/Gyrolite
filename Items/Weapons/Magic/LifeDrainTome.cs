using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class LifeDrainTome : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Life Drain Tome";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Send out a ball of blood which leeches life from your enemies";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 20;
            item.knockBack = 1;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;

            item.channel = true;
            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            item.shoot = mod.ProjectileType("LifeDrainSphere");
            item.shootSpeed = 14f;

            item.useSound = 10;
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

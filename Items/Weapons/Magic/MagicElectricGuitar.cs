using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class MagicElectricGuitar : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Magic Electric Guitar";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Shoot a quick ball of water that has a chance to slow its victims";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 20;
            item.knockBack = 2;
            item.autoReuse = true;
            item.useTime = 12;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.holdStyle = 3;

            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            item.shoot = 76;
            item.shootSpeed = 4.5f;

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

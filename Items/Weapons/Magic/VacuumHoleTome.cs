using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    /// <summary>
    /// WIP
    /// </summary>
    public class VacuumHoleTome : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Vacuum Hole Tome";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Fire a ravaging vortex to consume your enemies";
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

            item.shoot = mod.ProjectileType("VacuumHole");
            item.shootSpeed = 5f;

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

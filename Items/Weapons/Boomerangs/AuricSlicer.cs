using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Boomerangs
{
    public class AuricSlicer : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Auric Slicer";
            item.width = 14;
            item.height = 28;
            item.value = 5000;

            item.damage = 8;
            item.knockBack = 5f;
            item.useStyle = 1;
            item.useTime = 16;
            item.useAnimation = 16;

            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.shoot = mod.ProjectileType("AuricSlicer");
            item.shootSpeed = 12f;

            item.useSound = 1;
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
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

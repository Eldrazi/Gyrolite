using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class LifeSapTome : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Life Sap Tome";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Shoot life sapping magical darts";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 20;
            item.knockBack = 2;
            item.autoReuse = true;
            item.useTime = 25; // The time between two uses of this item.
            item.useAnimation = 17;
            item.useStyle = 5;

            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            item.shoot = mod.ProjectileType("LifeSapDart");
            item.shootSpeed = 10f; // The speed of the fired projectile.

            item.useSound = 20;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, Main.myPlayer);
            return false;
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

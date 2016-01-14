using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class GreekFireTome : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Greek Fire Tome";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Sprays Greek Fire on your foes";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 20;
            item.knockBack = 2;
            item.autoReuse = true;
            item.useTime = 5; // The time between two uses of this item.
            item.useAnimation = 17;
            item.useStyle = 5;

            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            item.shoot = mod.ProjectileType("GreekFire");
            item.shootSpeed = 10f; // The speed of the fired projectile.

            item.useSound = 20;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float num2 = 12f;
            Vector2 vector2 = new Vector2(player.position.X, player.position.Y);
            float num3 = Main.MouseWorld.X - vector2.X;
            float num4 = Main.MouseWorld.Y - vector2.Y;
            float num5 = num2 + Math.Abs(num3) * (1.0F / 500.0F);
            float num6 = num3 + (float)Main.rand.Next(-50, 51);
            float num7 = num4 - (float)Main.rand.Next(50, 201);
            float num8 = (float)Math.Sqrt((double)num6 * (double)num6 + (double)num7 * (double)num7);
            float num9 = num5 / num8;
            float num10 = num6 * num9;
            float num11 = num7 * num9;
            float SpeedX = num10 * (float)(1.0 + (double)Main.rand.Next(-30, 31) * 0.00499999988824129);
            float SpeedY = num11 * (float)(1.0 + (double)Main.rand.Next(-30, 31) * 0.00499999988824129);

            position = vector2;
            speedX = SpeedX;
            speedY = SpeedY;
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

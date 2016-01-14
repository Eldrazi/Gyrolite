using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class TriBoltTome : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Tri-Bolt Tome";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Casts three slow moving bolts of water";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 20;
            item.knockBack = 5;
            item.autoReuse = true;
            item.useTime = 17; // The time between two uses of this item.
            item.useAnimation = 17;
            item.useStyle = 5;

            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            item.shoot = 27; // Shoot a water bolt.
            item.shootSpeed = 4.5F; // The speed of the fired projectile.

            item.useSound = 21;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 direction = new Vector2(speedX, speedY);
            direction.Normalize();
            position += direction * item.width;

            float rotationOffset = 8;
            Vector2 projectilePos = player.Center;
            Vector2 spinningpoint = direction * rotationOffset;
            for (int i = 0; i < 3; ++i)
            {
                spinningpoint = Utils.RotatedBy(spinningpoint, Main.rand.NextDouble() * 0.25F);

                float angle = (float)Math.Atan2(speedY, speedX);
                position = new Vector2(projectilePos.X + 30 * (float)Math.Cos(angle), projectilePos.Y + 30 * (float)Math.Sin(angle));

                Projectile.NewProjectile(position.X, position.Y, spinningpoint.X, spinningpoint.Y, type, damage, knockBack, Main.myPlayer);
            }
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

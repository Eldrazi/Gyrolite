using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class GreekFireballTome : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Greek Fireball Tome";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Sprays Greek Fireballs on your foes";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 20;
            item.knockBack = 5.5F;
            item.useTime = 20;
            item.useAnimation = 17;
            item.useStyle = 5;

            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            item.shoot = mod.ProjectileType("GreekFireball");
            item.shootSpeed = 6f; // The speed of the fired projectile.

            item.useSound = 20;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 relativeCenter = player.RotatedRelativePoint(player.MountedCenter, true);

            float num5 = player.inventory[player.selectedItem].shootSpeed * item.scale;
            Vector2 vector2_3 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - relativeCenter;
            if ((double)player.gravDir == -1.0)
                vector2_3.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - relativeCenter.Y;
            Vector2 vector2_4 = Vector2.Normalize(vector2_3);
            if (float.IsNaN(vector2_4.X) || float.IsNaN(vector2_4.Y))
                vector2_4 = -Vector2.UnitY;
            vector2_4 *= num5;
            item.velocity = vector2_4;

            float rotationOffset = 8;
            Vector2 projectilePos = player.Center;
            Vector2 spinningpoint = Vector2.Normalize(item.velocity) * rotationOffset;
            for (int i = 0; i < 2; ++i)
            {
                spinningpoint = Utils.RotatedBy(spinningpoint, Main.rand.NextDouble() * 0.25F, new Vector2());
                if (float.IsNaN(spinningpoint.X) || float.IsNaN(spinningpoint.Y))
                    spinningpoint = -Vector2.UnitY;

                float angle = (float)Math.Atan(speedY / speedX);
                Vector2 vector2 = new Vector2(projectilePos.X + 30 * (float)Math.Cos(angle), projectilePos.Y + 30 * (float)Math.Sin(angle));
                float mouseX = Main.mouseX + Main.screenPosition.X;
                if (mouseX < projectilePos.X)
                {
                    vector2 = new Vector2(projectilePos.X - 30 * (float)Math.Cos(angle), projectilePos.Y - 30 * (float)Math.Sin(angle));
                }

                Projectile.NewProjectile(vector2.X, vector2.Y, spinningpoint.X, spinningpoint.Y, type, damage, knockBack, Main.myPlayer);
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

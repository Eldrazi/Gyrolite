using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Melee
{
    public class TrueMuramasa : ModItem
    {
        int shootCooldown;

        public override void SetDefaults()
        {
            item.name = "True Muramasa";
            item.width = 96;
            item.height = 96;
            item.value = Item.sellPrice(10, 0, 0, 0);
            item.rare = 10;

            item.damage = 150;
            item.knockBack = 7.5F;
            item.useStyle = 1;
            item.useTime = 13;
            item.useAnimation = 13;
            item.useTurn = true;
            item.autoReuse = true;
            item.scale = 1f;

            item.shoot = mod.ProjectileType("TrueMuramasaBeam");
            item.shootSpeed = 12F; // The speed at which the projectile will travel.

            item.melee = true;

            item.useSound = 1;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float angle = (float)Math.Atan(speedY / speedX);
            Vector2 vector2 = new Vector2(position.X + 55F * (float)Math.Cos(angle), position.Y + 55F * (float)Math.Sin(angle));
            float mouseX = Main.mouseX + Main.screenPosition.X;
            if (mouseX < vector2.X)
            {
                vector2 = new Vector2(position.X - 55F * (float)Math.Cos(angle), position.Y - 55F * (float)Math.Sin(angle));
            }

            if (shootCooldown >= 60)
            {
                position = vector2;
                shootCooldown = 0;
                return true;
            }
            return false;
        }

        public override void UpdateInventory(Player player)
        {
            shootCooldown++;
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

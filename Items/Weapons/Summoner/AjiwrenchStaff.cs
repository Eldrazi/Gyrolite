using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Summoner
{
    public class AjiwrenchStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Ajiwrench Staff";
            item.width = 26;
            item.height = 27;
            item.toolTip = "Summons an Ajiwrench minion to fight for you";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 0;
            item.useStyle = 1;
            item.useTime = 20;
            item.useAnimation = 20;

            item.noMelee = true;
            item.mana = 1;
            item.shoot = mod.ProjectileType("AjiwrenchMinion");
            item.buffType = mod.BuffType("AjiwrenchMinionBuff");
            item.buffTime = 3600;

            item.useSound = 2;

            item.summon = true;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position.X = (float)Main.mouseX + Main.screenPosition.X;
            position.Y = (float)Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(position.X, position.Y, 0, 0, type, 10, 0.5F, player.whoAmI, Main.rand.Next(1, 4), 0f);
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

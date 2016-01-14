//using System;

//using Microsoft.Xna.Framework;

//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;

//namespace Gyrolite.Items.Weapons.Summoner
//{
//    public class EarthRuneStaff : ModItem
//    {
//        public override void SetDefaults()
//        {
//            item.name = "Earth Rune Staff";
//            item.width = 26;
//            item.height = 28;
//            item.toolTip = "Summons an Earth Rune to fight for you";
//            item.value = Item.sellPrice(0, 5, 0, 0);
//            item.rare = 5;

//            item.damage = 8;
//            item.knockBack = 2f;
//            item.useStyle = 1;
//            item.useTime = 28;
//            item.useAnimation = 28;

//            item.noMelee = true;
//            item.mana = 1;
//            item.shoot = mod.ProjectileType("RuneMinion");
//            item.buffType = mod.BuffType("RuneMinionBuff");
//            item.buffTime = 3600;

//            item.shootSpeed = 10f;

//            item.useSound = 44;

//            item.summon = true;
//        }

//        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
//        {
//            int newRuneIndex = 0;
//            for (int i = 0; i < Main.projectile.Length; ++i)
//                if (Main.projectile[i].active && Main.projectile[i].type == type)
//                    newRuneIndex++;

//            Projectile.NewProjectile(position.X, position.Y, 0f, 0f, mod.ProjectileType("RuneMinion"), damage, knockBack, player.whoAmI, newRuneIndex);

//            return false;
//        }

//        public override void AddRecipes()
//        {
//            ModRecipe recipe = new ModRecipe(mod);
//            recipe.AddIngredient(ItemID.DirtBlock);
//            recipe.SetResult(this);
//            recipe.AddRecipe();
//        }
//    }
//}

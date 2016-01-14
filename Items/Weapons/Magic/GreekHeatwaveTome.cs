using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class GreekHeatwaveTome : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Greek Heatwave Tome";
            item.width = 28;
            item.height = 30;
            item.toolTip = "Fire a Greek Heatwave to burn your enemies to crisps.\nBounces 3 times";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 20;
            item.knockBack = 4;
            item.autoReuse = true;
            item.useTime = 30;
            item.useAnimation = 17;
            item.useStyle = 5;

            item.channel = true;
            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            item.shoot = mod.ProjectileType("GreekHeatwave");
            item.shootSpeed = 10f;

            item.useSound = 0; // Play the sound only in the Shoot function.
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(2, position, 82);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
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

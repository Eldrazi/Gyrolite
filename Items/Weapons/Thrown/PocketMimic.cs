using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Thrown
{
    public class PocketMimic : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Pocket Mimic";
            item.width = 18;
            item.height = 20;
            item.toolTip = "A small, throwable mimic";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.maxStack = 999;
            item.damage = 20;
            item.knockBack = 2;
            item.autoReuse = false;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.noUseGraphic = true;

            item.thrown = true;
            item.noMelee = true;
            item.consumable = true;

            item.shoot = 1;
            item.shootSpeed = 10;

            item.useSound = 1;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int newNPC = NPC.NewNPC((int)position.X, (int)position.Y, mod.NPCType("PocketMimic"), 0, 0, 0, 0, player.whoAmI);
            Main.npc[newNPC].velocity = new Microsoft.Xna.Framework.Vector2(speedX, speedY);
            Main.npc[newNPC].direction = speedX > 0 ? -1 : 1;

            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this, 999);
            recipe.AddRecipe();
        }
    }
}

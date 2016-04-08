using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Summoner
{
    public class BloodyGeyserWand : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Bloody Geyser Wand";
            item.width = 26;
            item.height = 28;
            item.toolTip = "?";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 8;
            item.knockBack = 2f;
            item.useStyle = 1;
            item.useTime = 28;
            item.useAnimation = 28;

            item.noMelee = true;
            item.mana = 1;

            item.shoot = mod.ProjectileType("BloodyGeyserMinion");
            item.buffType = mod.BuffType("BloodyGeyserMinionBuff");
            item.buffTime = 3600;

            item.useSound = 44;

            item.summon = true;
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int spawnX = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
            int spawnY = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                spawnY = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
            }
            while (spawnY < Main.maxTilesY - 10 && Main.tile[spawnX, spawnY] != null && !WorldGen.SolidTile2(spawnX, spawnY) && Main.tile[spawnX - 1, spawnY] != null && !WorldGen.SolidTile2(spawnX - 1, spawnY) && Main.tile[spawnX + 1, spawnY] != null && !WorldGen.SolidTile2(spawnX + 1, spawnY))
            {
                spawnY++;
            }
            spawnY--;
            Projectile.NewProjectile((float)Main.mouseX + Main.screenPosition.X, (float)(spawnY * 16 - 24), 0f, 15f, type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 8);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons.Magic
{
    public class Chloroblast : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Chloroblast";
            item.width = 30;
            item.height = 30;
            item.toolTip = "Creates a circle of spore sacs to shield you.";
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 5;

            item.damage = 20;
            item.knockBack = 5.5F;
            item.useTime = 4;
            item.useAnimation = 4;
            item.useStyle = 5;

            item.magic = true;
            item.noMelee = true;
            item.mana = 0;

            //item.shoot = mod.ProjectileType("ChloroblastSpore");
            //item.shootSpeed = 6f; // The speed of the fired projectile.

            item.useSound = 20;
        }
        public override bool UseItem(Player player)
        {
            int num = 0;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].type == mod.ProjectileType("ChloroblastSpore") && Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI)
				{
					num++;
				}
			}
            if (num < 24)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("ChlroblastSpore"), 40, 1f, Main.myPlayer);
                return true;
            }
            return false;
        }
    }
}

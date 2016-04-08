using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Accessories.Rings
{
    public class IchorRing : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Ring of Ichor";
            item.width = 22;
            item.height = 24;
            item.toolTip = "??";
            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 5;

            item.damage = 25;
            item.knockBack = 0.1F;
            item.useStyle = 4;
            item.useAnimation = 15;
            item.useTime = 15;

            item.useTurn = false;
            item.autoReuse = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.magic = true;
            item.mana = 0; // Around 5?

            item.accessory = true;

            item.shoot = mod.ProjectileType("RingProjectile_Ichor");
            item.shootSpeed = 14;

            item.useSound = 4;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 3; ++i)
            {
                position = new Vector2(player.position.X + (player.width * 0.5f) + (float)Main.rand.Next(201) * -player.direction + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                position.X = (position.X * 10f + player.Center.X) / 11f + (float)Main.rand.Next(-100, 101);
                position.Y -= (float)(150 * i);
                speedX = (float)Main.mouseX + Main.screenPosition.X - position.X;
                speedY = (float)Main.mouseY + Main.screenPosition.Y - position.Y;
                if (speedY < 0f)
                {
                    speedY *= -1f;
                }
                if (speedY < 20f)
                {
                    speedY = 20f;
                }
                float num80 = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                num80 = item.shootSpeed / num80;
                speedX *= num80;
                speedY *= num80;
                speedX = speedX + Main.rand.Next(-40, 41) * 0.03f;
                speedY = speedY + (float)Main.rand.Next(-40, 41) * 0.03f;
                speedX *= (float)Main.rand.Next(75, 150) * 0.01f;
                position.X += (float)Main.rand.Next(-50, 51);
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, player.Center.Y, 0f);
            }

            return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            GyrolitePlayer gp = (GyrolitePlayer)player.GetModPlayer(mod, "GyrolitePlayer");
            gp.ringType = 2;
            gp.ringEffects[0] = item.shoot;
            gp.ringEffects[1] = BuffID.Ichor;
        }
    }
}

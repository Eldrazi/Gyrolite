using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs.Pets
{
    public class BabyIchorStickerBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffName[Type] = "Baby Ichor Sticker";
            Main.buffTip[Type] = "??";
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            ((GyrolitePlayer)player.GetModPlayer(mod, "GyrolitePlayer")).babyIchorStickerPet = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("BabyIchorSticker")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, mod.ProjectileType("BabyIchorSticker"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}

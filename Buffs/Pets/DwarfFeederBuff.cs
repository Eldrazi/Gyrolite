using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs.Pets
{
    public class DwarfFeederBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffName[Type] = "Dwarf Feeder";
            Main.buffTip[Type] = "??";
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            ((GyrolitePlayer)player.GetModPlayer(mod, "GyrolitePlayer")).dwarfFeederPet = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("DwarfFeederHead")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, mod.ProjectileType("DwarfFeederHead"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}

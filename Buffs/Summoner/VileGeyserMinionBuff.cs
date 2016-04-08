using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs.Summoner
{
    public class VileGeyserMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffName[this.Type] = "Vile Geyser Summon";
            Main.buffTip[this.Type] = "??";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            GyrolitePlayer gp = (GyrolitePlayer)player.GetModPlayer(mod, "GyrolitePlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("VileGeyserMinion")] > 0)
            {
                gp.vileGeyserMinion = true;
            }
            if (!gp.vileGeyserMinion)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}

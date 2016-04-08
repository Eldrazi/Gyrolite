using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs.Summoner
{
    public class BloodyGeyserMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffName[this.Type] = "Bloody Geyser Summon";
            Main.buffTip[this.Type] = "??";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            GyrolitePlayer gp = (GyrolitePlayer)player.GetModPlayer(mod, "GyrolitePlayer");
            if (player.ownedProjectileCounts[mod.ProjectileType("BloodyGeyserMinion")] > 0)
            {
                gp.bloodyGeyserMinion = true;
            }
            if (!gp.bloodyGeyserMinion)
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

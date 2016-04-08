using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs.Summoner
{
    public class YellowSlimeMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffName[this.Type] = "Yellow Slime Summon";
            Main.buffTip[this.Type] = "A Yellow Slime that fights for you";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("YellowSlimeMinion")] > 0)
            {
                player.slime = true;
            }
            if (!player.slime)
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

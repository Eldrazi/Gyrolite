using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs.Summoner
{
    public class BlueSlimeMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffName[this.Type] = "Blue Slime Summon";
            Main.buffTip[this.Type] = "A Blue Slime that fights for you";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("BlueSlimeMinion")] > 0)
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

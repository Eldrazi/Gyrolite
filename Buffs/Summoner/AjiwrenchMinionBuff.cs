using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs.Summoner
{
    public class AjiwrenchMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffName[this.Type] = "Ajiwrench Summon";
            Main.buffTip[this.Type] = "An Ajiwrench that fights for you";
        }

        public override void Update(Player player, ref int buffIndex)
        {            
            if (player.ownedProjectileCounts[mod.ProjectileType("AjiwrenchMinion")] > 0)
            {
                player.sharknadoMinion = true;
            }
            if (!player.sharknadoMinion)
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

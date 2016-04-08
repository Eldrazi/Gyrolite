using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs.Alchemist
{
    public class PiercingBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffName[this.Type] = "Armor Piercing";
            Main.buffTip[this.Type] = "Gives additional armor piercing";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.armorPenetration = 4;
        }
    }
}

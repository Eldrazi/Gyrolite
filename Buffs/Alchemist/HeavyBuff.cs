using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs.Alchemist
{
    public class HeavyBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffName[this.Type] = "Heavy";
            Main.buffTip[this.Type] = "Grants immunity to all knockback";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.noKnockback = true;
        }
    }
}

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs.Alchemist
{
    public class AmplifiedSenseBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffName[this.Type] = "Amplified Sense";
            Main.buffTip[this.Type] = "Allows for detection of enemies, traps and treasures";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.dangerSense = true;
            player.findTreasure = true;
            player.detectCreature = true;
        }
    }
}

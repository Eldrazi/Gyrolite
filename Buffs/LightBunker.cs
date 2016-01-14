using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs
{
    public class LightBunker : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffName[this.Type] = "Light Bunker";
            Main.buffTip[this.Type] = "A shield that blocks 20% of all incomming damage, but lowers movespeed by 20%.";
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.2F; // Blocks 20% of all incomming damage.
            player.moveSpeed -= 0.2F; // Move 20% slower.
        }
    }
}

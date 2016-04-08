using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Buffs.Mounts
{
    public class WyvernMountBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffName[this.Type] = "Wyvern";
            Main.buffTip[this.Type] = "??";
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(mod.MountType("Wyvern"), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}

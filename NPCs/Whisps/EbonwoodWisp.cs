using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Whisps
{
    /// <summary>
    /// Only spawn this NPC in the Corruption.
    /// </summary>
    public class EbonwoodWisp : Wisp
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            return mod.Properties.Autoload;
        }

        public override void SetDefaults()
        {
            npc.name = "Ebonwood Whisp";
            base.SetDefaults();
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            if (NPC.AnyNPCs(mod.NPCType("WispQueen")) && spawnInfo.player.ZoneCorrupt)
                return 10;
            if (Gyrolite.NoInvasion(spawnInfo) && spawnInfo.player.ZoneCorrupt)
                return 0.5F;

            return 0;
        }
    }
}

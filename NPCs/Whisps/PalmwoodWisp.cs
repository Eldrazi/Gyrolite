using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Whisps
{
    public class PalmwoodWisp : Wisp
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            return mod.Properties.Autoload;
        }

        public override void SetDefaults()
        {
            npc.name = "Palmwood Whisp";
            base.SetDefaults();
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            if (NPC.AnyNPCs(mod.NPCType("WispQueen")) && spawnInfo.player.ZoneDesert)
                return 10;
            if (Gyrolite.NoInvasion(spawnInfo) && spawnInfo.player.ZoneDesert)
                return 0.5F;

            return 0;
        }
    }
}

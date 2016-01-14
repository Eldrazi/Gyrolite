using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Whisps
{
    public class RichMahoganyWisp : Wisp
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            return mod.Properties.Autoload;
        }

        public override void SetDefaults()
        {
            npc.name = "Rich Mahogany Whisp";
            base.SetDefaults();
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            if (NPC.AnyNPCs(mod.NPCType("WispQueen")) && spawnInfo.player.ZoneJungle)
                return 10;
            if (Gyrolite.NoInvasion(spawnInfo) && spawnInfo.player.ZoneJungle)
                return 0.5F;

            return 0;
        }
    }
}

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs
{
    public class AuraticMass : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Auratic Mass";
            npc.width = 44;
            npc.height = 32;
            Main.npcFrameCount[npc.type] = 6;
            npc.aiStyle = 1;
            npc.damage = 45;
            npc.defense = 20;
            npc.lifeMax = 250;
            npc.soundHit = 1;
            npc.soundKilled = 1;
            npc.alpha = 100;
            npc.value = (float)Item.buyPrice(0, 0, 20, 0);
            npc.knockBackResist = 0.3f;
        }

        public override void FindFrame(int frameHeight)
        {
            int num1 = 0;
            if (npc.aiAction == 0)
                num1 = (double)npc.velocity.Y >= 0.0 ? ((double)npc.velocity.Y <= 0.0 ? ((double)npc.velocity.X == 0.0 ? 0 : 1) : 3) : 2;
            else if (npc.aiAction == 1)
                num1 = 4;

            ++npc.frameCounter;
            if (num1 > 0)
                ++npc.frameCounter;
            if (num1 == 4)
                ++npc.frameCounter;
            if (npc.frameCounter >= 8.0)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y >= frameHeight * Main.npcFrameCount[npc.type])
                npc.frame.Y = 0;
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return 0;
        }
    }
}

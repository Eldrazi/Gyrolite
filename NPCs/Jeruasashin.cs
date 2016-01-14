using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs
{
    public class Jeruasashin : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Jeruasashin";
            npc.width = 60;
            npc.height = 38;
            Main.npcFrameCount[npc.type] = 3;
            npc.aiStyle = 1;
            npc.damage = 90;
            npc.defense = 30;
            npc.lifeMax = 400;
            npc.soundHit = 1;
            npc.soundKilled = 1;
            npc.alpha = 50;
            npc.value = (float)Item.buyPrice(0, 0, 20, 0);
            npc.knockBackResist = 0.3f;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = npc.direction;
            if ((double)npc.velocity.Y != 0.0)
            {
                npc.frame.Y = frameHeight * 2;
            }
            else
            {
                ++npc.frameCounter;
                if (npc.frameCounter >= 8.0)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    npc.frameCounter = 0.0;
                }
                if (npc.frame.Y > frameHeight)
                    npc.frame.Y = 0;
            }
        }

        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return 0;
        }
    }
}

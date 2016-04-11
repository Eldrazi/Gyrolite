using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Aetheris
{
    public class GigaAetheriaSlime : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Giga Aetheria Slime";
            npc.width = 60;
            npc.height = 42;

            npc.aiStyle = 1;
            npc.damage = 30;
            npc.lifeMax = 400;
            npc.defense = 10;
            npc.knockBackResist = 0.6f;

            npc.alpha = 80;

            Main.npcFrameCount[npc.type] = 4;

            npc.soundHit = 1;
            npc.soundKilled = 1;
        }

        public override void FindFrame(int frameHeight)
        {
            int action = 0;
            if (npc.aiAction == 0)
                action = npc.velocity.Y >= 0.0 ? (npc.velocity.Y <= 0.0 ? (npc.velocity.X == 0.0 ? 0 : 1) : 3) : 2;
            else if (npc.aiAction == 1)
                action = 4;

            ++npc.frameCounter;
            if (action > 0)
                ++npc.frameCounter;
            if (action == 4)
                ++npc.frameCounter;
            if (npc.frameCounter >= 8.0)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y >= frameHeight * Main.npcFrameCount[npc.type])
                npc.frame.Y = 0;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.Next(0, 2) == 0) // 50% chance to inflict the Ichor debuff for 3 seconds.
            {
                target.AddBuff(BuffID.Ichor, 180);
            }
        }
    }
}

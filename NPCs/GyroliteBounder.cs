using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs
{
    public class GyroliteBounder : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Gyrolite Bounder";
            npc.width = 20;
            npc.height = 10;
            Main.npcFrameCount[npc.type] = 4;

            npc.damage = 30;
            npc.defense = 15;
            npc.lifeMax = 120;
            npc.knockBackResist = 0.0f;

            npc.soundHit = 1;
            npc.soundKilled = 1;
        }

        public override bool PreAI()
        {
            if ((double)npc.velocity.Y == 0.0)
            {
                if ((double)npc.ai[0] == 1.0)
                {
                    if (npc.direction == 0)
                        npc.TargetClosest(true);
                    npc.direction = Main.player[npc.target].position.X < npc.position.X ? -1 : 1;

                    npc.velocity.X = (0.2F * (float)npc.direction) * 3F;
                }
                else
                    npc.velocity.X = 0.0f;
                if (Main.netMode != 1)
                {
                    --npc.localAI[1];
                    if ((double)npc.localAI[1] <= 0.0)
                    {
                        if ((double)npc.ai[0] == 1.0)
                        {
                            npc.ai[0] = 0.0f;
                            npc.localAI[1] = (float)Main.rand.Next(300, 900);
                        }
                        else
                        {
                            npc.ai[0] = 1f;
                            npc.localAI[1] = (float)Main.rand.Next(600, 1800);
                        }
                        npc.netUpdate = true;
                    }
                }
            }

            npc.spriteDirection = npc.direction;
            bool flag = false;
            for (int index = 0; index < (int)byte.MaxValue; ++index)
            {
                Player player = Main.player[index];
                if (player.active && !player.dead && (double)Vector2.Distance(player.Center, npc.Center) > 160.0) // 10 tile distance.
                {
                    flag = true;
                    break;
                }
            }
            int num1 = 90;
            if (flag && (double)npc.ai[1] < (double)num1)
                ++npc.ai[1];
            if ((double)npc.ai[1] != (double)num1 || Main.netMode == 1)
                return false;
            npc.position.Y = npc.position.Y + 16f;
            npc.Transform(mod.NPCType("GyroliteBounderDigger")); // Transform into the burrowing version.
            npc.netUpdate = true;
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.localAI[0] = -2f;
            if ((double)npc.velocity.Y == 0.0)
            {
                npc.rotation = 0.0f;
                if ((double)npc.velocity.X == 0.0)
                {
                    npc.frame.Y = 0;
                    npc.frameCounter = 0.0;
                }
                else
                {
                    ++npc.frameCounter;
                    if (npc.frameCounter > 6.0)
                    {
                        npc.frameCounter = 0.0;
                        npc.frame.Y = npc.frame.Y + frameHeight;
                        if (npc.frame.Y >= frameHeight * 4)
                            npc.frame.Y = frameHeight;
                    }
                }
            }
            else
            {
                npc.rotation += (float)npc.direction * 0.1f;
                ++npc.frameCounter;
                if (npc.frameCounter > 3.0)
                {
                    npc.frameCounter = 0.0;
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    if (npc.frame.Y >= frameHeight * 4)
                        npc.frame.Y = frameHeight;
                }
            }
        }
        
        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return 0;
        }
    }
}

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Sloman
{
    public class PurpleSloman_Phase3 : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Purple Sloman";
            npc.width = 28;
            npc.height = 44;

            npc.damage = 30;
            npc.lifeMax = 750;
            npc.defense = 10;
            npc.knockBackResist = 0.6f;

            npc.alpha = 60;

            Main.npcFrameCount[npc.type] = 16;

            npc.soundHit = 1;
            npc.soundKilled = 1;
        }

        public override bool PreAI()
        {
            NPCs.NPCAI.ExtraNPCAction action = delegate()
            {
                if ((npc.velocity.Y == 0 && Math.Abs((float)(npc.position.X + (npc.width / 2) - (Main.player[npc.target].position.X + (Main.player[npc.target].width / 2)))) < 160 && Math.Abs((float)(npc.position.Y + (npc.height / 2) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2)))) < 50.0 && (npc.direction > 0 && npc.velocity.X >= 1 || npc.direction < 0 && npc.velocity.X <= -1)))
                {
                    npc.velocity.X = npc.velocity.X * 4f;
                    if ((double)npc.velocity.X > 5)
                        npc.velocity.X = 5f;
                    if ((double)npc.velocity.X < -5)
                        npc.velocity.X = -5f;
                    npc.velocity.Y = -4f;
                    npc.netUpdate = true;
                }
            };
            NPCAI.FighterAI(npc.whoAmI, 5, 0.2F, null,  action);
            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.Y != 0) // The NPC is in the air.
            {
                npc.frame.Y = frameHeight;
                npc.frameCounter = 0.0F;
            }
            else if (npc.velocity.X == 0) // The NPC is on the ground and Idle.
            {
                npc.frame.Y = 0;
                npc.frameCounter = 0.0F;
            }
            else // The NPC is on the ground and moving
            {
                npc.frameCounter += Math.Abs(npc.velocity.X) / 4; // The faster the NPC is walking, the faster the animation will play.
                npc.frameCounter %= (Main.npcFrameCount[npc.type] - 2); // If the frameCounter exceeds the amount of frames available, reset it to 0.
                int frame = (int)(npc.frameCounter) + 2; // +2, because first 2 frames are Air and Idle frames.
                npc.frame.Y = frame * frameHeight;
            }

            npc.spriteDirection = npc.direction; // NPC needs to look towards the direction it's walking.
        }

        public override bool CheckDead()
        {
            for (int i = 0; i < 10; ++i)
            {
                int dust11 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Silver, 0f, -2f, 40, Color.MediumPurple, 1.5f);
                Main.dust[dust11].noGravity = false;
                Main.dust[dust11].position.X += Main.rand.Next(-30, 31) / 20 - 1.5f;
                Main.dust[dust11].position.Y -= 20;
                if (Main.dust[dust11].position != npc.Center)
                {
                    Main.dust[dust11].velocity = npc.DirectionTo(Main.dust[dust11].position) * Main.rand.Next(3, 6);
                }
            }
            return true;
        }
    }
}

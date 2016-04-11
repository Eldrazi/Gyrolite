using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Aetheris
{
    public class Itilisp : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Itilisp";
            npc.width = 22;
            npc.height = 22;

            npc.damage = 20;
            npc.defense = 5;
            npc.lifeMax = 300;
            npc.knockBackResist = 0.0f;

            npc.noTileCollide = false;

            npc.noGravity = true;

            Main.npcFrameCount[npc.type] = 6;
        }

        public override bool PreAI()
        {
            npc.TargetClosest(false);

            Player target = Main.player[npc.target];
            Vector2 center = new Vector2(npc.position.X + (npc.width * 0.5F), npc.position.Y + (npc.height * 0.5F));
            Vector2 targetCenter = new Vector2(target.position.X + (target.width * 0.5F), target.position.Y + (target.height * 0.5F));

            if (npc.ai[0] == 0)
            {
                npc.noTileCollide = false;

                npc.velocity.X = 0;
                npc.velocity.Y += 0.09F;
                npc.rotation = 0;

                if (Vector2.Distance(center, targetCenter) <= 240)
                {
                    npc.ai[0] = 1;
                }
            }
            else if (npc.ai[0] == 1)
            {
                npc.noTileCollide = true;
                npc.velocity.Y -= 0.06F;

                npc.ai[1]++;
                if (npc.ai[1] >= 30)
                {
                    npc.ai[0] = 2;
                    npc.ai[1] = 0;
                }
            }
            else if (npc.ai[0] == 2)
            {
                int tilePosX = (int)(npc.position.X / 16);
                int tilePosY = (int)(npc.position.X / 16);
                if (Vector2.Distance(center, targetCenter) >= 480 && !Main.tile[tilePosX, tilePosY].active())
                {
                    npc.ai[0] = 0;
                }

                Vector2 dir = targetCenter - center;
                float num3 = 12f / (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                float dirX = dir.X * num3;
                float dirY = dir.Y * num3;
                npc.velocity.X = (float)((npc.velocity.X * 100.0 + dirX) / 101.0);
                npc.velocity.Y = (float)((npc.velocity.Y * 100.0 + dirY) / 101.0);

                npc.rotation = npc.velocity.X / 20;
            }

            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] == 0)
                npc.frame.Y = 0;
            else if (npc.ai[0] == 1 || npc.ai[0] == 2)
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 10)
                {
                    npc.frame.Y = npc.frame.Y + frameHeight;
                    if (npc.frame.Y >= Main.npcFrameCount[npc.type] * frameHeight)
                    {
                        npc.frame.Y = 3 * frameHeight;
                    }
                    npc.frameCounter = 0;
                }
            }
        }
    }
}

using System;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs
{
    public class PocketMimic : ModNPC
    {
        int frame = 0;
        Vector2 pointOfImpact;

        public override void SetDefaults()
        {
            npc.name = "Pocket Mimic";
            npc.friendly = true;
            npc.damage = 20;
            npc.lifeMax = 1;
            npc.width = 32;
            npc.height = 46;
            npc.immortal = true;
            npc.knockBackResist = -1;
            npc.scale = 0.75F;
            npc.timeLeft = 120;

            Main.npcFrameCount[npc.type] = 6;
        }

        public override bool PreAI()
        {
            if (npc.ai[0] == 0)
            {
                npc.localAI[1] = 600;
                npc.ai[0] = 1;
            }
            if (npc.ai[0] == 1)
            {
                if (!npc.noTileCollide)
                {
                    if (npc.collideX)
                    {
                        npc.velocity.X = npc.oldVelocity.X * -0.8f;
                        npc.direction = -npc.direction;
                    }
                    if (npc.collideY)
                    {
                        if (npc.oldVelocity.Y < 0)
                            npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                        else
                        {
                            npc.velocity.X = 0;
                            npc.ai[0] = 2;
                        }
                    }
                }
                npc.velocity.X = npc.velocity.X * 0.99f;

                CheckEnemyCollision();
            }
            else if (npc.ai[0] == 2)
            {
                if (npc.velocity.Y == 0)
                {
                    npc.ai[2]++;
                    if (npc.ai[2] >= 30)
                    {
                        int dir = Main.rand.Next(2) == 0 ? 1 : -1;
                        npc.velocity.Y = -Main.rand.Next(2, 6);
                        npc.velocity.X = 4 * dir;
                        npc.direction = -dir;
                        npc.ai[2] = 0;
                        npc.ai[0] = 1;
                    }
                }

                CheckEnemyCollision();
            }
            else if (npc.ai[0] == 3)
            {
                int num996 = 15;
                bool flag52 = false;
                bool flag53 = false;
                npc.localAI[0]++;
                if (npc.localAI[0] % 30f == 0f)
                {
                    flag53 = true;
                }
                int num997 = (int)npc.ai[1];
                if (npc.localAI[0] >= (float)(60 * num996))
                {
                    flag52 = true;
                }
                else if (num997 < 0 || num997 >= 200)
                {
                    flag52 = true;
                }
                else if (Main.npc[num997].active && !Main.npc[num997].dontTakeDamage)
                {
                    npc.Center = Main.npc[num997].Center - npc.velocity * 2;
                    npc.gfxOffY = Main.npc[num997].gfxOffY;
                    if (flag53)
                    {
                        Main.npc[num997].HitEffect(0, 1.0);
                    }
                }
                else
                {
                    flag52 = true;
                }
                if (flag52)
                {
                    Main.npc[npc.whoAmI].active = false;
                }
            }

            npc.localAI[1]--;
            if (npc.localAI[1] <= 0)
            {
                npc.active = false;
            }
            return false;
        }

        public void CheckEnemyCollision()
        {
            for (int i = 0; i < 200; ++i)
            {
                if (Main.npc[i].friendly) continue; // If the indexed NPC is friendly, ignore it.
                if(!npc.Hitbox.Intersects(Main.npc[i].Hitbox)) continue; // If there is no collision between this NPC and the enemy one, continue.

                Main.npc[i].StrikeNPCNoInteraction(npc.damage, 2, (int)(Main.npc[i].Center.X - npc.Center.X));

                npc.ai[0] = 3;
                npc.ai[1] = i;
                npc.damage = 0;
                npc.noGravity = true;
                npc.noTileCollide = true;
                npc.velocity = (Main.npc[i].Center - npc.Center) * 0.75f;
                npc.netUpdate = true;
                break;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[0] == 1) // The NPC is in the air.
            {
                npc.frame.Y = frameHeight * 5;
                npc.frameCounter = 0.0F;
            }
            else
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 7)
                {
                    if (++frame > 4)
                        frame = 2;
                    npc.frameCounter = 0;
                }
                npc.frame.Y = frame * frameHeight;
            }

            npc.spriteDirection = npc.direction;
        }
    }
}

using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Aura
{
    public class GyroliteBounderDigger : ModNPC
    {
        public override void SetDefaults()
        {
            npc.displayName = "Gyrolite Bounder";
            npc.name = "Gyrolite Bounder Digger";
            npc.width = 10;
            npc.height = 10;
            Main.npcFrameCount[npc.type] = 4;

            npc.damage = 30;
            npc.defense = 15;
            npc.lifeMax = 120;
            npc.knockBackResist = 0.0f;

            npc.noGravity = true;
            npc.behindTiles = true;
            npc.noTileCollide = true;

            npc.soundHit = 1;
            npc.soundKilled = 1;
        }

        public override bool PreAI()
        {
            if (npc.ai[3] > 0.0F)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target >= (int)byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            int num4 = (int)(npc.position.X / 16.0) - 1;
            int num5 = (int)((npc.position.X + npc.width) / 16.0) + 2;
            int num6 = (int)(npc.position.Y / 16.0) - 1;
            int num7 = (int)((npc.position.Y + npc.height) / 16.0) + 2;
            if (num4 < 0)
                num4 = 0;
            if (num5 > Main.maxTilesX)
                num5 = Main.maxTilesX;
            if (num6 < 0)
                num6 = 0;
            if (num7 > Main.maxTilesY)
                num7 = Main.maxTilesY;
            bool flag1 = false;

            if (!flag1)
            {
                for (int i = num4; i < num5; ++i)
                {
                    for (int j = num6; j < num7; ++j)
                    {
                        if (Main.tile[i, j] != null && (Main.tile[i, j].nactive() && (Main.tileSolid[(int)Main.tile[i, j].type] || Main.tileSolidTop[(int)Main.tile[i, j].type] && (int)Main.tile[i, j].frameY == 0) || (int)Main.tile[i, j].liquid > 64))
                        {
                            Vector2 vector2;
                            vector2.X = (float)(i * 16);
                            vector2.Y = (float)(j * 16);
                            if ((double)npc.position.X + (double)npc.width > (double)vector2.X && (double)npc.position.X < (double)vector2.X + 16.0 && ((double)npc.position.Y + (double)npc.height > (double)vector2.Y && (double)npc.position.Y < (double)vector2.Y + 16.0))
                            {
                                flag1 = true;
                            }
                        }
                    }
                }
            }
            if (!flag1)
            {
                for (int index = 0; index < (int)byte.MaxValue; ++index)
                {
                    Player player = Main.player[index];
                    if (player.active && !player.dead && (double)Vector2.Distance(player.Center, npc.Center) <= 160) // 10 tiles distance.
                    {
                        npc.target = index;
                        npc.Transform(mod.NPCType("GyroliteBounder")); // Transform into the burrowing version.
                        npc.netUpdate = true;
                        return false;
                    }
                }
                Rectangle rectangle1 = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                int num1 = 1000;
                bool flag2 = true;
                for (int index = 0; index < (int)byte.MaxValue; ++index)
                {
                    if (Main.player[index].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[index].position.X - num1, (int)Main.player[index].position.Y - num1, num1 * 2, num1 * 2);
                        if (rectangle1.Intersects(rectangle2))
                        {
                            flag2 = false;
                            break;
                        }
                    }
                }
                if (flag2)
                    flag1 = true;
            }

            float num8 = 6F;
            float num9 = 0.15f;
            Vector2 vector2_3 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float num10 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
            float num11 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);

            float num16 = (float)((int)((double)num10 / 16.0) * 16);
            float num17 = (float)((int)((double)num11 / 16.0) * 16);
            vector2_3.X = (float)((int)((double)vector2_3.X / 16.0) * 16);
            vector2_3.Y = (float)((int)((double)vector2_3.Y / 16.0) * 16);
            float num18 = num16 - vector2_3.X;
            float num19 = num17 - vector2_3.Y;

            float num20 = (float)Math.Sqrt((double)num18 * (double)num18 + (double)num19 * (double)num19);
            if ((double)npc.ai[1] > 0.0)
            {
                if ((double)npc.ai[1] < (double)Main.npc.Length)
                {
                    try
                    {
                        vector2_3 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                        num18 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector2_3.X;
                        num19 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector2_3.Y;
                    }
                    catch
                    {
                    }
                    npc.rotation = (float)Math.Atan2((double)num19, (double)num18) + 1.57f;
                    float num1 = (float)Math.Sqrt((double)num18 * (double)num18 + (double)num19 * (double)num19);
                    int num2 = npc.width;
                    float num3 = (num1 - (float)num2) / num1;
                    float num12 = num18 * num3;
                    float num13 = num19 * num3;
                    npc.velocity = Vector2.Zero;
                    npc.position.X = npc.position.X + num12;
                    npc.position.Y = npc.position.Y + num13;
                }
            }

            if (!flag1)
            {
                npc.TargetClosest(true);
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                if ((double)npc.velocity.Y > (double)num8)
                    npc.velocity.Y = num8;
                if ((double)Math.Abs(npc.velocity.X) + (double)Math.Abs(npc.velocity.Y) < (double)num8 * 0.4)
                {
                    if ((double)npc.velocity.X < 0.0)
                        npc.velocity.X = npc.velocity.X - num9 * 1.1f;
                    else
                        npc.velocity.X = npc.velocity.X + num9 * 1.1f;
                }
                else if ((double)npc.velocity.Y == (double)num8)
                {
                    if ((double)npc.velocity.X < (double)num18)
                        npc.velocity.X = npc.velocity.X + num9;
                    else if ((double)npc.velocity.X > (double)num18)
                        npc.velocity.X = npc.velocity.X - num9;
                }
                else if ((double)npc.velocity.Y > 4.0)
                {
                    if ((double)npc.velocity.X < 0.0)
                        npc.velocity.X = npc.velocity.X + num9 * 0.9f;
                    else
                        npc.velocity.X = npc.velocity.X - num9 * 0.9f;
                }
            }
            else
            {
                if (npc.soundDelay == 0)
                {
                    float num1 = num20 / 40f;
                    if ((double)num1 < 10.0)
                        num1 = 10f;
                    if ((double)num1 > 20.0)
                        num1 = 20f;
                    npc.soundDelay = (int)num1;
                    Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 1);
                }
                float num2 = (float)Math.Sqrt((double)num18 * (double)num18 + (double)num19 * (double)num19);
                float num3 = Math.Abs(num18);
                float num12 = Math.Abs(num19);
                float num13 = num8 / num2;
                float num14 = num18 * num13;
                float num15 = num19 * num13;
                if ((double)npc.velocity.X > 0.0 && (double)num14 > 0.0 || (double)npc.velocity.X < 0.0 && (double)num14 < 0.0 || ((double)npc.velocity.Y > 0.0 && (double)num15 > 0.0 || (double)npc.velocity.Y < 0.0 && (double)num15 < 0.0))
                {
                    if ((double)npc.velocity.X < (double)num14)
                        npc.velocity.X = npc.velocity.X + num9;
                    else if ((double)npc.velocity.X > (double)num14)
                        npc.velocity.X = npc.velocity.X - num9;
                    if ((double)npc.velocity.Y < (double)num15)
                        npc.velocity.Y = npc.velocity.Y + num9;
                    else if ((double)npc.velocity.Y > (double)num15)
                        npc.velocity.Y = npc.velocity.Y - num9;
                    if ((double)Math.Abs(num15) < (double)num8 * 0.2 && ((double)npc.velocity.X > 0.0 && (double)num14 < 0.0 || (double)npc.velocity.X < 0.0 && (double)num14 > 0.0))
                    {
                        if ((double)npc.velocity.Y > 0.0)
                            npc.velocity.Y = npc.velocity.Y + num9 * 2f;
                        else
                            npc.velocity.Y = npc.velocity.Y - num9 * 2f;
                    }
                    if ((double)Math.Abs(num14) < (double)num8 * 0.2 && ((double)npc.velocity.Y > 0.0 && (double)num15 < 0.0 || (double)npc.velocity.Y < 0.0 && (double)num15 > 0.0))
                    {
                        if ((double)npc.velocity.X > 0.0)
                            npc.velocity.X = npc.velocity.X + num9 * 2f;
                        else
                            npc.velocity.X = npc.velocity.X - num9 * 2f;
                    }
                }
                else if ((double)num3 > (double)num12)
                {
                    if ((double)npc.velocity.X < (double)num14)
                        npc.velocity.X = npc.velocity.X + num9 * 1.1f;
                    else if ((double)npc.velocity.X > (double)num14)
                        npc.velocity.X = npc.velocity.X - num9 * 1.1f;
                    if ((double)Math.Abs(npc.velocity.X) + (double)Math.Abs(npc.velocity.Y) < (double)num8 * 0.5)
                    {
                        if ((double)npc.velocity.Y > 0.0)
                            npc.velocity.Y = npc.velocity.Y + num9;
                        else
                            npc.velocity.Y = npc.velocity.Y - num9;
                    }
                }
                else
                {
                    if ((double)npc.velocity.Y < (double)num15)
                        npc.velocity.Y = npc.velocity.Y + num9 * 1.1f;
                    else if ((double)npc.velocity.Y > (double)num15)
                        npc.velocity.Y = npc.velocity.Y - num9 * 1.1f;
                    if ((double)Math.Abs(npc.velocity.X) + (double)Math.Abs(npc.velocity.Y) < (double)num8 * 0.5)
                    {
                        if ((double)npc.velocity.X > 0.0)
                            npc.velocity.X = npc.velocity.X + num9;
                        else
                            npc.velocity.X = npc.velocity.X - num9;
                    }
                }
            }
            npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;

            return false;
        }

        public override void FindFrame(int frameHeight)
        {
            ++npc.frameCounter;
            if (npc.frameCounter > 4.0)
            {
                npc.frameCounter = 0.0;
                npc.frame.Y = npc.frame.Y + frameHeight;
                if (npc.frame.Y >= frameHeight * 4)
                    npc.frame.Y = 0;
            }
        }
    }
}

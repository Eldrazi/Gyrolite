using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs
{
    public static class NPCDrawing
    {
    }
    public static class NPCAI
    {
        public delegate void ExtraNPCAction();

        public static void FighterAI(int index, float speed = 0.1F, float acceleration = 0.1F, ExtraNPCAction initialize = null, ExtraNPCAction action = null)
        {
            NPC npc = Main.npc[index];

            if (initialize != null && npc.localAI[3] == 0)
            {
                initialize();
                npc.localAI[3] = 1;
            }

            bool idle = false;
            if ((double)npc.velocity.X == 0.0)
                idle = true;
            if (npc.justHit)
                idle = false;

            int cooldown = 60;
            bool moving = false;

            // If the NPC is grounded and moving.
            if ((double)npc.velocity.Y == 0.0 && ((double)npc.velocity.X > 0.0 && npc.direction < 0 || (double)npc.velocity.X < 0.0 && npc.direction > 0))
                moving = true;

            if ((double)npc.position.X == (double)npc.oldPosition.X || (double)npc.ai[3] >= (double)cooldown || moving)
                ++npc.ai[3];
            else if ((double)Math.Abs(npc.velocity.X) > 0.9 && (double)npc.ai[3] > 0.0)
                --npc.ai[3];

            if ((double)npc.ai[3] > (double)(cooldown * 10))
                npc.ai[3] = 0.0f;
            if (npc.justHit)
                npc.ai[3] = 0.0f;
            if ((double)npc.ai[3] == (double)cooldown)
                npc.netUpdate = true;

            // Find the closest target
            npc.TargetClosest(true);

            if ((double)npc.velocity.X < -(double)speed || (double)npc.velocity.X > (double)speed)
            {
                if ((double)npc.velocity.Y == 0.0)
                {
                    float vecX = npc.velocity.X * 0.8f;
                    float vecY = npc.velocity.Y * 0.8f;
                    npc.velocity.X = vecX;
                    npc.velocity.Y = vecY;
                }
            }
            else if ((double)npc.velocity.X < (double)speed && npc.direction == 1)
            {
                npc.velocity.X = npc.velocity.X + acceleration;
                if ((double)npc.velocity.X > (double)speed)
                    npc.velocity.X = speed;
            }
            else if ((double)npc.velocity.X > -(double)speed && npc.direction == -1)
            {
                npc.velocity.X = npc.velocity.X - acceleration;
                if ((double)npc.velocity.X < -(double)speed)
                    npc.velocity.X = -speed;
            }

            bool flag11 = false;

            if ((double)npc.velocity.Y == 0.0)
            {
                int index1 = (int)((double)npc.position.Y + (double)npc.height + 7.0) / 16;
                int num1 = (int)npc.position.X / 16;
                int num2 = (int)((double)npc.position.X + (double)npc.width) / 16;
                for (int index2 = num1; index2 < num2; ++index2)
                {
                    if (Main.tile[index2, index1] == null)
                        return;
                    if (Main.tile[index2, index1].nactive() && Main.tileSolid[(int)Main.tile[index2, index1].type])
                    {
                        flag11 = true;
                        break;
                    }
                }
            }

            if ((double)npc.velocity.Y >= 0.0)
            {
                int num1 = 0;
                if ((double)npc.velocity.X < 0.0)
                    num1 = -1;
                if ((double)npc.velocity.X > 0.0)
                    num1 = 1;
                float posX = npc.position.X;
                float posY = npc.position.Y;
                posX += npc.velocity.X;

                int index1 = (int)(((double)posX + (double)(npc.width / 2) + (double)((npc.width / 2 + 1) * num1)) / 16.0);
                int index2 = (int)(((double)posY + (double)npc.height - 1.0) / 16.0);
                if (Main.tile[index1, index2] == null)
                    Main.tile[index1, index2] = new Tile();
                if (Main.tile[index1, index2 - 1] == null)
                    Main.tile[index1, index2 - 1] = new Tile();
                if (Main.tile[index1, index2 - 2] == null)
                    Main.tile[index1, index2 - 2] = new Tile();
                if (Main.tile[index1, index2 - 3] == null)
                    Main.tile[index1, index2 - 3] = new Tile();
                if (Main.tile[index1, index2 + 1] == null)
                    Main.tile[index1, index2 + 1] = new Tile();
                if (Main.tile[index1 - num1, index2 - 3] == null)
                    Main.tile[index1 - num1, index2 - 3] = new Tile();

                if ((double)(index1 * 16) < (double)posX + (double)npc.width && (double)(index1 * 16 + 16) > (double)posX && (Main.tile[index1, index2].nactive() && !Main.tile[index1, index2].topSlope() && (!Main.tile[index1, index2 - 1].topSlope() && Main.tileSolid[(int)Main.tile[index1, index2].type]) && !Main.tileSolidTop[(int)Main.tile[index1, index2].type] || Main.tile[index1, index2 - 1].halfBrick() && Main.tile[index1, index2 - 1].nactive()) && ((!Main.tile[index1, index2 - 1].nactive() || !Main.tileSolid[(int)Main.tile[index1, index2 - 1].type] || Main.tileSolidTop[(int)Main.tile[index1, index2 - 1].type] || Main.tile[index1, index2 - 1].halfBrick() && (!Main.tile[index1, index2 - 4].nactive() || !Main.tileSolid[(int)Main.tile[index1, index2 - 4].type] || Main.tileSolidTop[(int)Main.tile[index1, index2 - 4].type])) && ((!Main.tile[index1, index2 - 2].nactive() || !Main.tileSolid[(int)Main.tile[index1, index2 - 2].type] || Main.tileSolidTop[(int)Main.tile[index1, index2 - 2].type]) && (!Main.tile[index1, index2 - 3].nactive() || !Main.tileSolid[(int)Main.tile[index1, index2 - 3].type] || Main.tileSolidTop[(int)Main.tile[index1, index2 - 3].type]) && (!Main.tile[index1 - num1, index2 - 3].nactive() || !Main.tileSolid[(int)Main.tile[index1 - num1, index2 - 3].type]))))
                {
                    float num2 = (float)(index2 * 16);
                    if (Main.tile[index1, index2].halfBrick())
                        num2 += 8f;
                    if (Main.tile[index1, index2 - 1].halfBrick())
                        num2 -= 8f;
                    if ((double)num2 < (double)posY + (double)npc.height)
                    {
                        float num3 = posY + (float)npc.height - num2;
                        float num4 = 16.1f;

                        if ((double)num3 <= (double)num4)
                        {
                            npc.gfxOffY += npc.position.Y + (float)npc.height - num2;
                            npc.position.Y = num2 - (float)npc.height;
                            npc.stepSpeed = (double)num3 >= 9.0 ? 2f : 1f;
                        }
                    }
                }
            }

            if (flag11)
            {
                int index1 = (int)(((double)npc.position.X + (double)(npc.width / 2) + (double)(15 * npc.direction)) / 16.0);
                int index2 = (int)(((double)npc.position.Y + (double)npc.height - 15.0) / 16.0);

                if (Main.tile[index1, index2] == null)
                    Main.tile[index1, index2] = new Tile();
                if (Main.tile[index1, index2 - 1] == null)
                    Main.tile[index1, index2 - 1] = new Tile();
                if (Main.tile[index1, index2 - 2] == null)
                    Main.tile[index1, index2 - 2] = new Tile();
                if (Main.tile[index1, index2 - 3] == null)
                    Main.tile[index1, index2 - 3] = new Tile();
                if (Main.tile[index1, index2 + 1] == null)
                    Main.tile[index1, index2 + 1] = new Tile();
                if (Main.tile[index1 + npc.direction, index2 - 1] == null)
                    Main.tile[index1 + npc.direction, index2 - 1] = new Tile();
                if (Main.tile[index1 + npc.direction, index2 + 1] == null)
                    Main.tile[index1 + npc.direction, index2 + 1] = new Tile();
                if (Main.tile[index1 - npc.direction, index2 + 1] == null)
                    Main.tile[index1 - npc.direction, index2 + 1] = new Tile();
                Main.tile[index1, index2 + 1].halfBrick();
                if (Main.tile[index1, index2 - 1].nactive() && ((int)Main.tile[index1, index2 - 1].type == 10 || (int)Main.tile[index1, index2 - 1].type == 388))
                {
                    ++npc.ai[2];
                    npc.ai[3] = 0.0f;
                    if ((double)npc.ai[2] >= 60.0)
                    {
                        npc.velocity.X = 0.5f * -(float)npc.direction;
                        int num1 = 5;
                        if ((int)Main.tile[index1, index2 - 1].type == 388)
                            num1 = 2;
                        npc.ai[1] += (float)num1;
                        npc.ai[2] = 0.0f;
                        bool flag6 = false;
                        if ((double)npc.ai[1] >= 10.0)
                        {
                            flag6 = true;
                            npc.ai[1] = 10f;
                        }
                        WorldGen.KillTile(index1, index2 - 1, true, false, false);
                        if ((Main.netMode != 1 || !flag6) && (flag6 && Main.netMode != 1))
                        {
                            if (TileLoader.OpenDoorID(Main.tile[index1, index2 - 1]) >= 0)
                            {
                                bool flag7 = WorldGen.OpenDoor(index1, index2 - 1, npc.direction);
                                if (!flag7)
                                {
                                    npc.ai[3] = (float)cooldown;
                                    npc.netUpdate = true;
                                }
                                if (Main.netMode == 2 && flag7)
                                    NetMessage.SendData(19, -1, -1, "", 0, (float)index1, (float)(index2 - 1), (float)npc.direction, 0, 0, 0);
                            }
                            if ((int)Main.tile[index1, index2 - 1].type == 388)
                            {
                                bool flag7 = WorldGen.ShiftTallGate(index1, index2 - 1, false);
                                if (!flag7)
                                {
                                    npc.ai[3] = (float)cooldown;
                                    npc.netUpdate = true;
                                }
                                if (Main.netMode == 2 && flag7)
                                    NetMessage.SendData(19, -1, -1, "", 4, (float)index1, (float)(index2 - 1), 0.0f, 0, 0, 0);
                            }
                        }
                    }
                }
                else
                {
                    if ((double)npc.velocity.X < 0.0 && npc.spriteDirection == -1 || (double)npc.velocity.X > 0.0 && npc.spriteDirection == 1)
                    {
                        if (npc.height >= 32 && Main.tile[index1, index2 - 2].nactive() && Main.tileSolid[(int)Main.tile[index1, index2 - 2].type])
                        {
                            if (Main.tile[index1, index2 - 3].nactive() && Main.tileSolid[(int)Main.tile[index1, index2 - 3].type])
                            {
                                npc.velocity.Y = -8f;
                                npc.netUpdate = true;
                            }
                            else
                            {
                                npc.velocity.Y = -7f;
                                npc.netUpdate = true;
                            }
                        }
                        else if (Main.tile[index1, index2 - 1].nactive() && Main.tileSolid[(int)Main.tile[index1, index2 - 1].type])
                        {
                            npc.velocity.Y = -6f;
                            npc.netUpdate = true;
                        }
                        else if ((double)npc.position.Y + (double)npc.height - (double)(index2 * 16) > 20.0 && Main.tile[index1, index2].nactive() && (!Main.tile[index1, index2].topSlope() && Main.tileSolid[(int)Main.tile[index1, index2].type]))
                        {
                            npc.velocity.Y = -5f;
                            npc.netUpdate = true;
                        }
                        else if (npc.directionY < 0 && (!Main.tile[index1, index2 + 1].nactive() || !Main.tileSolid[(int)Main.tile[index1, index2 + 1].type]) && (!Main.tile[index1 + npc.direction, index2 + 1].nactive() || !Main.tileSolid[(int)Main.tile[index1 + npc.direction, index2 + 1].type]))
                        {
                            npc.velocity.Y = -8f;
                            npc.velocity.X = npc.velocity.X * 1.5f;
                            npc.netUpdate = true;
                        }
                        else
                        {
                            npc.ai[1] = 0.0f;
                            npc.ai[2] = 0.0f;
                        }
                        if ((double)npc.velocity.Y == 0.0 && idle && (double)npc.ai[3] == 1.0)
                            npc.velocity.Y = -5f;
                    }
                }
            }
            else
            {
                npc.ai[1] = 0.0f;
                npc.ai[2] = 0.0f;
            }

            if (action != null)
                action();
        }  
    }
}

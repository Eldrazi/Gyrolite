using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Bosses.WispQueen
{
    public class WispQueen : ModNPC
    {
        private Vector2 centerPos;
        private float centerRot;

        public override void SetDefaults()
        {
            npc.name = "Wisp Queen";
            npc.width = 62;
            npc.height = 62;
            npc.alpha = 30;

            npc.damage = 20;
            npc.knockBackResist = 0.0f;
            npc.lifeMax = 1500;
            npc.noGravity = true;
            npc.noTileCollide = true;

            npc.soundHit = 1;
            npc.soundKilled = 1;

            npc.boss = true;
            music = 13;
        }

        public override bool PreAI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (npc.ai[0] == 0)
            {
                Vector2 dir = Main.player[npc.target].position - npc.position;
                dir.Normalize();
                npc.velocity = dir * 1.5F;

                if (npc.position.X < Main.player[npc.target].position.X)
                    npc.localAI[1] = MathHelper.Clamp(npc.localAI[1] + 0.01F, -0.05F, 0.05F);
                else
                    npc.localAI[1] = MathHelper.Clamp(npc.localAI[1] - 0.01F, -0.05F, 0.05F);

                npc.rotation += npc.localAI[1] * npc.direction;
                centerRot -= npc.localAI[1] * npc.direction;

                if (npc.life < (npc.lifeMax / 3) * 2)
                {
                    npc.ai[0] = 1;                    
                }
            }
            else if (npc.ai[0] == 1)
            {
                Vector2 dir = Main.player[npc.target].position - npc.position;
                dir.Normalize();
                npc.velocity = dir * 2.25F;

                if (npc.position.X < Main.player[npc.target].position.X)
                    npc.localAI[1] = MathHelper.Clamp(npc.localAI[1] + 0.01F, -0.08F, 0.08F);
                else
                    npc.localAI[1] = MathHelper.Clamp(npc.localAI[1] - 0.01F, -0.08F, 0.08F);

                npc.rotation += npc.localAI[1] * npc.direction;
                centerRot -= npc.localAI[1] * npc.direction;

                if (npc.life < (npc.lifeMax / 3))
                {
                    npc.ai[0] = 2;
                }
            }
            else if (npc.ai[0] == 2)
            {
                Vector2 dir = Main.player[npc.target].position - npc.position;
                dir.Normalize();
                npc.velocity = dir * 2.5F;                    

                if (npc.position.X < Main.player[npc.target].position.X)
                    npc.localAI[1] = MathHelper.Clamp(npc.localAI[1] + 0.01F, -0.08F, 0.08F);
                else
                    npc.localAI[1] = MathHelper.Clamp(npc.localAI[1] - 0.01F, -0.08F, 0.08F);

                npc.rotation += npc.localAI[1] * npc.direction;
                centerRot -= npc.localAI[1] * npc.direction;

                npc.ai[2]++;
                if (npc.ai[2] == 300)
                {
                    npc.ai[0] = 3;
                    npc.ai[2] = player.position.X;
                    npc.ai[3] = player.position.Y - 200;
                    
                    /*if (npc.ai[2] >= 420 && npc.ai[2] < 450)
                    {
                        npc.velocity = Vector2.Zero;
                        npc.scale += 0.02F;
                    }
                    else if (npc.ai[2] >= 450 && npc.ai[2] < 480)
                    {
                        npc.scale -= 0.02F;
                    }
                    else if (npc.ai[2] == 480)
                        npc.ai[2] = 0;*/
                }
            }
            else if (npc.ai[0] == 3)
            {
                npc.velocity = Vector2.Zero;
                npc.ai[1]++;
                if(npc.ai[1] < 30) // Half a second
                {
                    Vector2 targetPos = new Vector2((int)npc.ai[2], (int)npc.ai[3]);
                    npc.position = Vector2.Lerp(npc.position, targetPos, 0.1F);
                    if (npc.ai[1] == 29)
                        npc.ai[2] = 0;
                }
                else if (npc.ai[1] < 60)
                {
                    npc.scale += 0.02F; // Grow in scale for half a second.
                }
                else if (npc.ai[1] < 360)
                {
                    // Fuse with minions for 5 seconds 
                    npc.ai[2] = MathHelper.Clamp(npc.ai[2] + 0.001F, -0.5F, 0.5F);
                    npc.rotation += npc.ai[2] * npc.direction;
                    centerRot -= npc.ai[2] * npc.direction;
                }
                else if (npc.ai[1] < 390)
                {
                    npc.scale -= 0.02F; // Shrink in scale for half a second.

                    npc.ai[2] = MathHelper.Clamp(npc.ai[2] - 0.02F, 0, 0.5F);
                    npc.rotation += npc.ai[2] * npc.direction;
                    centerRot -= npc.ai[2] * npc.direction;
                }
                else
                {
                    for (int i = 0; i < 2; ++i) // Spawn aditional Wisps
                    {
                        int minX = Main.rand.Next(-600, -399);
                        int maxX = Main.rand.Next(400, 601);
                        int minY = Main.rand.Next(-600, -399);
                        int maxY = Main.rand.Next(400, 601);
                        NPC.NewNPC(npc.position.X + Main.rand.Next(0, 2) == 0 ? minX : maxX,
                            npc.position.Y + Main.rand.Next(0, 2) == 0 ? minY : maxY, mod.NPCType("AurawoodWisp"));
                    }

                    npc.ai[0] = 2; // Go back to AI state number 2.
                    npc.ai[1] = 0;
                    npc.ai[2] = 0; // Reset the rest of the AI cache's
                    npc.ai[3] = 0;
                }
            }

            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            npc.spriteDirection = 1;
            Texture2D centerTexture = ModLoader.GetTexture("Gyrolite/NPCs/Bosses/WispQueen/WispQueen_Center");
            Vector2 centerOrigin = new Vector2((centerTexture.Width / 2), (centerTexture.Height / 2));
            centerPos = new Vector2(npc.Center.X, npc.Center.Y + 4 - (30 * (npc.scale - 1)));

            Color color1 = Lighting.GetColor((int)centerPos.X / 16, (int)((double)centerPos.Y / 16.0));
            color1 = npc.GetAlpha(color1);

            Rectangle? sourceRectangle = new Rectangle?();
            Main.spriteBatch.Draw(centerTexture, centerPos - Main.screenPosition, sourceRectangle, color1, centerRot, centerOrigin, npc.scale, SpriteEffects.None, 0);

            base.PostDraw(spriteBatch, drawColor);
        }
        
    }
}

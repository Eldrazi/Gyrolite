using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Whisps
{
    public class Wisp : ModNPC
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            return false;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
            npc.width = 14;
            npc.height = 14;

            npc.lifeMax = 30;
            npc.damage = 10;
            npc.defense = 3;
            npc.knockBackResist = 1;
            npc.value = Item.buyPrice(0, 0, 1, 0); // The drop value of this NPC.
            npc.npcSlots = 1;
            npc.soundHit = 1;
            npc.soundKilled = 1;
            npc.scale = 1.5F;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.scale = 0.8F;
            npc.alpha = 50;
        }

        public override bool PreAI()
        {
            for (int index = 5; index > 0; --index)
            {
                npc.oldPos[index] = npc.oldPos[index - 1];
                npc.oldRot[index] = npc.oldRot[index - 1];
            }
            npc.oldPos[0] = npc.position;
            npc.oldRot[0] = npc.rotation;

            if (NPC.AnyNPCs(mod.NPCType("WispQueen")))
            {
                npc.ai[0] = NPC.FindFirstNPC(mod.NPCType("WispQueen"));
                if (Main.npc[(int)npc.ai[0]].ai[0] == 3 && (Main.npc[(int)npc.ai[0]].ai[1] < 360 && Main.npc[(int)npc.ai[0]].ai[1] >= 60))
                {
                    goto WispQueenPresent;
                }
            }

            if (!npc.noTileCollide)
            {
                if (npc.collideX)
                {
                    npc.velocity.X = npc.oldVelocity.X * -0.5f;
                    if (npc.direction == -1 && (double)npc.velocity.X > 0.0 && (double)npc.velocity.X < 2.0)
                        npc.velocity.X = 2f;
                    if (npc.direction == 1 && (double)npc.velocity.X < 0.0 && (double)npc.velocity.X > -2.0)
                        npc.velocity.X = -2f;
                }
                if (npc.collideY)
                {
                    npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                    if ((double)npc.velocity.Y > 0.0 && (double)npc.velocity.Y < 1.0)
                        npc.velocity.Y = 1f;
                    if ((double)npc.velocity.Y < 0.0 && (double)npc.velocity.Y > -1.0)
                        npc.velocity.Y = -1f;
                }
            }
            npc.TargetClosest(true);

            float num1 = 2f;
            float num2 = 1.5f;
            float num3 = num1;
            float num4 = num2;
            if (npc.direction == -1 && (double)npc.velocity.X > -(double)num3)
            {
                npc.velocity.X = npc.velocity.X - 0.1f;
                if ((double)npc.velocity.X > (double)num3)
                    npc.velocity.X = npc.velocity.X - 0.1f;
                else if ((double)npc.velocity.X > 0.0)
                    npc.velocity.X = npc.velocity.X + 0.05f;
                if ((double)npc.velocity.X < -(double)num3)
                    npc.velocity.X = -num3;
            }
            else if (npc.direction == 1 && (double)npc.velocity.X < (double)num3)
            {
                npc.velocity.X = npc.velocity.X + 0.1f;
                if ((double)npc.velocity.X < -(double)num3)
                    npc.velocity.X = npc.velocity.X + 0.1f;
                else if ((double)npc.velocity.X < 0.0)
                    npc.velocity.X = npc.velocity.X - 0.05f;
                if ((double)npc.velocity.X > (double)num3)
                    npc.velocity.X = num3;
            }
            if (npc.directionY == -1 && (double)npc.velocity.Y > -(double)num4)
            {
                npc.velocity.Y = npc.velocity.Y - 0.04f;
                if ((double)npc.velocity.Y > (double)num4)
                    npc.velocity.Y = npc.velocity.Y - 0.05f;
                else if ((double)npc.velocity.Y > 0.0)
                    npc.velocity.Y = npc.velocity.Y + 0.03f;
                if ((double)npc.velocity.Y < -(double)num4)
                    npc.velocity.Y = -num4;
            }
            else if (npc.directionY == 1 && (double)npc.velocity.Y < (double)num4)
            {
                npc.velocity.Y = npc.velocity.Y + 0.04f;
                if ((double)npc.velocity.Y < -(double)num4)
                    npc.velocity.Y = npc.velocity.Y + 0.05f;
                else if ((double)npc.velocity.Y < 0.0)
                    npc.velocity.Y = npc.velocity.Y - 0.03f;
                if ((double)npc.velocity.Y > (double)num4)
                    npc.velocity.Y = num4;
            }

            if (Main.rand.Next(40) == 0)
            {
                int index = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((double)npc.height * 0.5), 5, npc.velocity.X, 2f, 0, new Color(), 1f);
                Dust dust1 = Main.dust[index];
                dust1.velocity.X = dust1.velocity.X * 0.5f;
                Dust dust2 = Main.dust[index];
                dust2.velocity.Y = dust2.velocity.Y * 0.1f;
            }
            if (!npc.wet)
                return false;
            if ((double)npc.velocity.Y > 0.0)
                npc.velocity.Y = npc.velocity.Y * 0.95f;
            npc.velocity.Y = npc.velocity.Y - 0.5f;
            if ((double)npc.velocity.Y < -4.0)
                npc.velocity.Y = -4f;
            npc.TargetClosest(true);

            return false;
        WispQueenPresent:
            NPC queen = Main.npc[(int)npc.ai[0]];

            Vector2 dir = queen.position - npc.position;
            dir.Normalize();
            npc.velocity = dir * 3.5F;

            if (Vector2.Distance(queen.position, npc.position) < (queen.width + queen.height) / 2)
            {
                queen.life += npc.life * 4;
                npc.active = false;
            }
            return false;
        }

        public override void FindFrame(int frameHeight)
        {            
            npc.frameCounter += 0.1F;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;

            npc.spriteDirection = npc.direction;
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            for (int i = 1; i < npc.oldPos.Length; ++i)
            {
                Vector2 vector2_2 = npc.oldPos[i];
                Microsoft.Xna.Framework.Color color2 = Color.White * npc.Opacity;
                color2.R = (byte)(0.5 * (double)color2.R * (double)(10 - i) / 20.0);
                color2.G = (byte)(0.5 * (double)color2.G * (double)(10 - i) / 20.0);
                color2.B = (byte)(0.5 * (double)color2.B * (double)(10 - i) / 20.0);
                color2.A = (byte)(0.5 * (double)color2.A * (double)(10 - i) / 20.0);
                Main.spriteBatch.Draw(Main.npcTexture[npc.type], new Vector2(npc.oldPos[i].X - Main.screenPosition.X + (npc.width / 2),
                    npc.oldPos[i].Y - Main.screenPosition.Y + npc.height / 2), new Rectangle?(npc.frame), color2, npc.oldRot[i], origin, npc.scale, effects, 0.0f);
            }

            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), Color.White * npc.Opacity, npc.rotation, origin, npc.scale, effects, 0);

            return false;
        }
    }
}

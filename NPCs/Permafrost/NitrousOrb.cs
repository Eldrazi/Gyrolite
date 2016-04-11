using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Permafrost
{
    public class NitrousOrb : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Nitrous Orb";
            npc.width = 22;
            npc.height = 22;

            npc.damage = 20;
            npc.defense = 30;
            npc.lifeMax = 250;
            npc.knockBackResist = 0.1f;

            npc.noGravity = true;
            npc.noTileCollide = true;

            Main.npcFrameCount[npc.type] = 6;
        }

        public override bool PreAI()
        {
            npc.TargetClosest(true);

            Player target = Main.player[npc.target];
            Vector2 center = new Vector2(npc.position.X + (npc.width * 0.5F), npc.position.Y + (npc.height * 0.5F));
            Vector2 targetCenter = new Vector2(target.position.X + (target.width * 0.5F), target.position.Y + (target.height * 0.5F));

            Vector2 dir = targetCenter - center;
            float num3 = 6f / (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
            float dirX = dir.X * num3;
            float dirY = dir.Y * num3;
            npc.velocity.X = (float)((npc.velocity.X * 100.0 + dirX) / 101.0);
            npc.velocity.Y = (float)((npc.velocity.Y * 100.0 + dirY) / 101.0);

            return false;
        }

        public override bool CheckDead()
        {
            if (Main.netMode != 1)
            {
                for (int i = 0; i < 4; ++i)
                {
                    int newNPC = NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-50, 51), (int)npc.Center.Y + Main.rand.Next(-50, 51), mod.NPCType("NitrousGas"), 0, 0, 0, 0, 0, npc.target);

                    Main.npc[newNPC].justHit = true;
                    if (Main.npc[newNPC].Center != npc.Center)
                    {
                        Main.npc[newNPC].velocity = npc.DirectionTo(Main.npc[newNPC].Center) * 3f;
                    }
                }
            }

            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.05F;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;

            npc.spriteDirection = npc.direction;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (!target.frozen && Main.rand.Next(0, 2) == 0) // 50% chance to freeze the player for 1.5 seconds.
            {
                target.AddBuff(BuffID.Frozen, 90);
            }
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[npc.type]) * 0.5f);
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), drawColor * npc.Opacity, npc.rotation, origin, npc.scale, effects, 0);

            return false;
        }
    }
}

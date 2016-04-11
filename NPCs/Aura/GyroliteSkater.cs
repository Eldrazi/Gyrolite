using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Aura
{
    public class GyroliteSkater : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Gyrolite Skater";
            npc.width = 34;
            npc.height = 34;
            npc.alpha = 100;
            npc.behindTiles = true;

            npc.damage = 20;

            npc.lifeMax = 150;
            npc.knockBackResist = -1;
            npc.noGravity = true;

            npc.npcSlots = 0.3F;

            npc.soundHit = 1;
            npc.soundKilled = 1;
            npc.scale = 1.2F;
        }

        public override bool PreAI()
        {
            if (npc.ai[0] == 0.0)
            {
                npc.TargetClosest(true);
                npc.directionY = 1;
                npc.ai[0] = 1f;
            }
            int num = 6;
            if ((double)npc.ai[1] == 0.0)
            {
                npc.rotation += (float)(npc.direction * npc.directionY) * 0.13f;
                if (npc.collideY)
                    npc.ai[0] = 2f;
                if (!npc.collideY && (double)npc.ai[0] == 2.0)
                {
                    npc.direction = -npc.direction;
                    npc.ai[1] = 1f;
                    npc.ai[0] = 1f;
                }
                if (npc.collideX)
                {
                    npc.directionY = -npc.directionY;
                    npc.ai[1] = 1f;
                }
            }
            else
            {
                npc.rotation -= (float)(npc.direction * npc.directionY) * 0.13f;
                if (npc.collideX)
                    npc.ai[0] = 2f;
                if (!npc.collideX && (double)npc.ai[0] == 2.0)
                {
                    npc.directionY = -npc.directionY;
                    npc.ai[1] = 0.0f;
                    npc.ai[0] = 1f;
                }
                if (npc.collideY)
                {
                    npc.direction = -npc.direction;
                    npc.ai[1] = 0.0f;
                }
            }
            npc.velocity.X = (float)(num * npc.direction);
            npc.velocity.Y = (float)(num * npc.directionY);
            Lighting.AddLight((int)(npc.position.X + (npc.width / 2)) / 16, (int)(npc.position.Y + (npc.height / 2)) / 16, 0.9f, 0.3f + (float)(270 - (int)Main.mouseTextColor) / 400f, 0.2f);
            return false;
        }
        
        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(), lightColor * npc.alpha, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}

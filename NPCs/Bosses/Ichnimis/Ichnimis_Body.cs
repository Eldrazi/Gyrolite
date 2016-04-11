using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Gyrolite.NPCs.Bosses.Ichnimis
{
    public class Ichnimis_Body : ModNPC
    {
        private Vector2 armOrigin = new Vector2(34, 18);

        private int[] arms = new int[2];

        public override void SetDefaults()
        {
            npc.displayName = "Ichnimis";
            npc.name = "Ichnimis Body";
            npc.width = 160;
            npc.height = 142;

            npc.damage = 20;
            npc.defense = 20;
            npc.lifeMax = 2000;
            npc.knockBackResist = 0;
        }

        public override bool PreAI()
        {
            if (npc.ai[0] == 0) // Spawning Phase.
            {
                if (Main.netMode != 1)
                {
                    for (int i = 0; i < arms.Length; ++i)
                    {
                        arms[i] = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType("Ichnimis_Arm"), npc.whoAmI - 1);
                        Main.npc[arms[i]].realLife = npc.whoAmI;
                        Main.npc[arms[i]].ai[2] = i;
                        Main.npc[arms[i]].ai[3] = npc.whoAmI;
                    }
                }

                npc.ai[0] = 1;
            }
            else if (npc.ai[0] == 1)
            {

            }

            if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                npc.spriteDirection = 1;
            else
                npc.spriteDirection = -1;
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Texture2D armTexture = Main.npcTexture[Main.npc[arms[0]].type];
            Texture2D bodyTexture = Main.npcTexture[npc.type];

            // Draw back arm
            NPC backArm = Main.npc[arms[0]];
            spriteBatch.Draw(armTexture, new Vector2(backArm.position.X + (backArm.width * 0.5F), backArm.position.Y + (backArm.height * 0.5F))
                - Main.screenPosition, new Rectangle?(), drawColor, backArm.rotation, armOrigin, backArm.scale, effects, 0);

            Vector2 origin = new Vector2(bodyTexture.Width * 0.5F, bodyTexture.Height * 0.5F);
            spriteBatch.Draw(bodyTexture, new Vector2(npc.position.X + (npc.width * 0.5F), npc.position.Y + (npc.height * 0.5F)) 
                - Main.screenPosition, new Rectangle?(), drawColor, npc.rotation, origin, npc.scale, effects, 0);

            // Draw front arm
            NPC frontArm = Main.npc[arms[1]];
            spriteBatch.Draw(armTexture, new Vector2(frontArm.position.X + (frontArm.width * 0.5F), frontArm.position.Y + (frontArm.height * 0.5F))
                - Main.screenPosition, new Rectangle?(), drawColor, frontArm.rotation, armOrigin, frontArm.scale, effects, 0);

            return false;
        }
    }
}

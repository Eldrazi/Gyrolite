using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Permafrost
{
    public class PermafrostMass : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Permafrost Mass";
            npc.width = 44;
            npc.height = 33;
            npc.value = 90f;

            npc.aiStyle = 1;
            npc.damage = 30;
            npc.lifeMax = 65;
            npc.defense = 10;
            npc.knockBackResist = 0.6f;

            npc.alpha = 80;

            Main.npcFrameCount[npc.type] = 2;

            npc.soundHit = 1;
            npc.soundKilled = 1;
        }

        public override void FindFrame(int frameHeight)
        {
            Framing.Slime(npc, frameHeight);
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
    }
}

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Sloman
{
    public class BlueSloman_Phase1 : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Blue Sloman";
            npc.width = 44;
            npc.height = 32;

            npc.aiStyle = 1;
            npc.damage = 30;
            npc.lifeMax = 250;
            npc.defense = 10;
            npc.knockBackResist = 0.6f;

            npc.alpha = 60;

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
            npc.Transform(mod.NPCType("BlueSloman_Phase2"));
            npc.life = npc.lifeMax;
            return false;
        }
    }
}

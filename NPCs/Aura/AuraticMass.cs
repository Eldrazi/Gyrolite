using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Aura
{
    public class AuraticMass : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Auratic Mass";
            npc.width = 44;
            npc.height = 32;
            npc.value = Item.buyPrice(0, 0, 20, 0);

            npc.aiStyle = 1;
            npc.damage = 45;
            npc.defense = 20;
            npc.lifeMax = 250;
            npc.knockBackResist = 0.3f;

            npc.soundHit = 1;
            npc.soundKilled = 1;

            npc.alpha = 100;
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void FindFrame(int frameHeight)
        {
            Framing.Slime(npc, frameHeight);
        }
    }
}

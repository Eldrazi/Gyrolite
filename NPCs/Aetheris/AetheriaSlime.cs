using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Aetheris
{
    public class AetheriaSlime : ModNPC
    {
        public override void SetDefaults()
        {
            npc.name = "Aetheria Slime";
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

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.Next(0, 3) == 0) // 33% chance to inflict the Ichor debuff for 3 seconds.
            {
                target.AddBuff(BuffID.Ichor, 180);
            }
        }
    }
}

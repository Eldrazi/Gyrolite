using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite
{
    public class GyroliteGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.BlueSlime)
            {
                if (Main.rand.Next(5)==0)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulBlueSlime"), 1);
            }
            if (npc.type == NPCID.LavaSlime)
            {
                if (Main.rand.Next(5) == 0)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulMagmaSlime"), 1);
            }
            if (npc.type == NPCID.YellowSlime)
            {
                if (Main.rand.Next(5) == 0)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulYellowSlime"), 1);
            }
            if (npc.type == NPCID.BlackSlime)
            {
                if (Main.rand.Next(5) == 0)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulBlackSlime"), 1);
            }
        }
    }
}

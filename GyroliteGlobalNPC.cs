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
            if (npc.type == NPCID.IchorSticker)
            {
                if (Main.rand.Next(150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("IchorScentVial"));
                }
            }
            if (npc.type == NPCID.Pixie || npc.type == NPCID.Unicorn || npc.type == NPCID.LightMummy || npc.type == NPCID.Gastropod)
            {
                if (Main.rand.Next(150) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SugarCrystal"));
                }
            }
            if (npc.type == NPCID.WyvernHead || npc.type == NPCID.Harpy || npc.type == NPCID.AngryNimbus)
            {
                if (Main.rand.Next(80) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SkyJello"));
                }
            }
            if (npc.type == NPCID.DevourerHead || npc.type == NPCID.EaterofWorldsHead)
            {
                int chance;
                if (npc.type == NPCID.DevourerHead)
                    chance = Main.rand.Next(0, 30);
                else
                    chance = Main.rand.Next(0, 20);
                if (chance == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CursedMistVial"));
                }
            }

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

            GyrolitePlayer gp = (GyrolitePlayer)Main.player[Main.myPlayer].GetModPlayer(mod, "GyrolitePlayer");
            if (gp.babyIchorStickerPet || gp.crystalSpiritPet || gp.skyJellyPet)
            {
                gp.petKillStack++;
                CombatText.NewText(new Rectangle((int)gp.player.position.X, (int)gp.player.position.Y, gp.player.width, gp.player.height), new Color(255, 255, 255, 200),
                    "Pet stack: " + gp.petKillStack);            
            }
        }
    }
}

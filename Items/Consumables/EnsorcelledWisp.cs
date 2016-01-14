﻿using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Gyrolite.Items.Consumables
{
    public class EnsorcelledWisp : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Wisp Egg";
            item.width = 20;
            item.height = 20;
            item.maxStack = 20;
            item.toolTip = "Used to summon the fearsome Wisp Queen";
            item.rare = 3;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.useSound = 44;
            item.consumable = true;
        }

        public override DrawAnimation GetAnimation()
        {
            return new DrawAnimationVertical(5, 4);
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(mod.NPCType("WispQueen"));
        }
        public override bool UseItem(Player player)
        {
            //NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("WispQueen"));
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("HeavySlime"));
            return true;
        }
        private void SpawnOre()
        {
            int lx = 200;
            int hx = Main.maxTilesX - 200;
            int ly = (int)Main.worldSurface;
            int hy = Main.maxTilesY - 200;

            int x = WorldGen.genRand.Next(lx, hx);
            int y = WorldGen.genRand.Next(ly, hy);

            int minSpread = 2;
            int maxSpread = 8;

            int minFreq = 6;
            int maxFreq = 8;

            int s = WorldGen.genRand.Next(minSpread, maxSpread + 1);
            int f = WorldGen.genRand.Next(minFreq, maxFreq + 1);

            WorldGen.OreRunner(x, y, s, f, (ushort)mod.TileType("GyroliteOre"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
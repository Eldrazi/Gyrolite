using System;

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
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("WispQueen"));
            player.QuickSpawnItem(ItemID.Terrarian);
            player.QuickSpawnItem(ItemID.WoodYoyo);
            player.QuickSpawnItem(mod.ItemType("YoyoStringBall"));
            //NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("WispQueen"));
            return true;
        }

        private void SpawnOre()
        {
            int minX = 200;
            int maxX = Main.maxTilesX - 200;
            int minY = (int)Main.worldSurface;
            int maxY = Main.maxTilesY - 200;

            int x = WorldGen.genRand.Next(minX, maxX);
            int y = WorldGen.genRand.Next(minY, maxY);

            int minSpread = 2;
            int maxSpread = 8;

            int minFreq = 6;
            int maxFreq = 8;

            int s = WorldGen.genRand.Next(minSpread, maxSpread + 1);
            int f = WorldGen.genRand.Next(minFreq, maxFreq + 1);

            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
            {
                WorldGen.OreRunner(x, y, s, f, (ushort)mod.TileType("GyroliteOre"));
            }
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

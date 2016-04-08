using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

namespace Gyrolite
{
    public class GyroliteWorld : ModWorld
    {
        public static int minAuraZoneTiles = 50;
        public static int auraTiles = 0;

        public static int minPermafrostZoneTiles = 80;
        public static int permafrostTiles = 0;

        public override void ResetNearbyTileEffects()
        {
            auraTiles = 0;
            permafrostTiles = 0;
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            auraTiles = tileCounts[mod.TileType("Aurasand")] + tileCounts[mod.TileType("Aurastone")] +
                tileCounts[mod.TileType("GyroliteOre")] + tileCounts[mod.TileType("AurastoneBrick")] +
                tileCounts[mod.TileType("Aurawood")] + tileCounts[mod.TileType("LightAurastoneBrick")] +
                tileCounts[mod.TileType("DarkAurastoneBrick")] + tileCounts[mod.TileType("SoulIce")];

            permafrostTiles = tileCounts[mod.TileType("PermafrostObsidianBrick")] +
                tileCounts[mod.TileType("PermafrostHellstoneBrick")] + tileCounts[mod.TileType("PermafrostAsh")] +
                tileCounts[mod.TileType("GlacialStoneOre")];
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int PermafrostIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes")) + 1;
            tasks.Insert(PermafrostIndex, new PassLegacy("Permafrost", delegate(GenerationProgress progress)
            {
                progress.Message = "Generating Permafrost";

                int randBiomeDiameter = Main.rand.Next(160, 320);
                int minY = Main.maxTilesY - 200;
                int maxY = Main.maxTilesY;
                int minX = (Main.maxTilesX / 2) - randBiomeDiameter;//Main.rand.Next(0, Main.maxTilesX - randBiomeDiameter + 1);
                int maxX = minX + randBiomeDiameter;

                Dictionary<int, int> permaTypes = new Dictionary<int, int>()
                {
                    {TileID.Hellstone, mod.TileType("GlacialStoneOre")},
                    {TileID.Ash, mod.TileType("PermafrostAsh")},
                    {TileID.HellstoneBrick, mod.TileType("PermafrostHellstoneBrick")},
                    {TileID.ObsidianBrick, mod.TileType("PermafrostObsidianBrick")},
                };

                for (int x = minX; x < maxX; ++x)
                {
                    for (int y = minY; y < maxY; ++y)
                    {
                        if (Main.tile[x, y].liquid > 0)
                            Main.tile[x, y].liquidType(0);
                        if (Main.tile[x, y].type == 0)
                            continue;

                        int newTileType;
                        if (permaTypes.TryGetValue(Main.tile[x, y].type, out newTileType))
                        {
                            Main.tile[x, y].type = (ushort)newTileType;
                            WorldGen.SquareTileFrame(x, y, true);
                        }
                    }
                }

                #region Tree Generation
                for (int x = minX; x < maxX; ++x)
                {
                    int yStart = minY;
                    while (yStart < maxY)
                    {
                        this.GrowTree(x, minY, (ushort)mod.TileType("PermafrostTree"), new int[] 
                            {
                                mod.TileType("PermafrostAsh"), mod.TileType("PermafrostHellstoneBrick"), mod.TileType("PermafrostObsidianBrick")
                            });
                        yStart++;
                    }
                    if (WorldGen.genRand.Next(3) == 0)
                    {
                        x++;
                    }
                    if (WorldGen.genRand.Next(4) == 0)
                    {
                        x++;
                    }
                }
                #endregion
            }));
        }

        public void GrowTree(int x, int minY, ushort treeType, int[] tilesAllowGrowth, int sapplingType = 0, int minTreeHeight = 5, int maxTreeHeight = 16)
        {
            int y = minY;
            while (Main.tile[x, y] == null || Main.tile[x, y].type == sapplingType)
            {
                y++;
            }

            if (Main.tile[x, y].nactive() && !Main.tile[x, y].halfBrick() && Main.tile[x, y].slope() == 0 &&
                ((Main.tile[x - 1, y].active() && Array.Exists(tilesAllowGrowth, element => Main.tile[x - 1, y].type == element)) || 
                 (Main.tile[x + 1, y].active() && Array.Exists(tilesAllowGrowth, element => Main.tile[x + 1, y].type == element))))
            {
				int width = 2;
                if (WorldGen.EmptyTileCheck(x - width, x + width, y - maxTreeHeight, y - 1, sapplingType))
                {
                    bool flag = false;
                    bool flag2 = false;
                    int height = WorldGen.genRand.Next(minTreeHeight, maxTreeHeight + 1);
                    int num5;
                    for (int j = y - height; j < y; j++)
                    {
                        Main.tile[x, j].frameNumber((byte)WorldGen.genRand.Next(3));
                        Main.tile[x, j].active(true);
                        Main.tile[x, j].type = treeType;
                        num5 = WorldGen.genRand.Next(3);
                        int num6 = WorldGen.genRand.Next(10);

                        if (j == y - 1 || j == y - height)
                        {
                            num6 = 0;
                        }
                        while (((num6 == 5 || num6 == 7) && flag) || ((num6 == 6 || num6 == 7) && flag2))
                        {
                            num6 = WorldGen.genRand.Next(10);
                        }
                        flag = false;
                        flag2 = false;
                        if (num6 == 5 || num6 == 7)
                        {
                            flag = true;
                        }
                        if (num6 == 6 || num6 == 7)
                        {
                            flag2 = true;
                        }
                        if (num6 == 1)
                        {
                            if (num5 == 0)
                            {
                                Main.tile[x, j].frameX = 0;
                                Main.tile[x, j].frameY = 66;
                            }
                            if (num5 == 1)
                            {
                                Main.tile[x, j].frameX = 0;
                                Main.tile[x, j].frameY = 88;
                            }
                            if (num5 == 2)
                            {
                                Main.tile[x, j].frameX = 0;
                                Main.tile[x, j].frameY = 110;
                            }
                        }
                        else if (num6 == 2)
                        {
                            if (num5 == 0)
                            {
                                Main.tile[x, j].frameX = 22;
                                Main.tile[x, j].frameY = 0;
                            }
                            if (num5 == 1)
                            {
                                Main.tile[x, j].frameX = 22;
                                Main.tile[x, j].frameY = 22;
                            }
                            if (num5 == 2)
                            {
                                Main.tile[x, j].frameX = 22;
                                Main.tile[x, j].frameY = 44;
                            }
                        }
                        else if (num6 == 3)
                        {
                            if (num5 == 0)
                            {
                                Main.tile[x, j].frameX = 44;
                                Main.tile[x, j].frameY = 66;
                            }
                            if (num5 == 1)
                            {
                                Main.tile[x, j].frameX = 44;
                                Main.tile[x, j].frameY = 88;
                            }
                            if (num5 == 2)
                            {
                                Main.tile[x, j].frameX = 44;
                                Main.tile[x, j].frameY = 110;
                            }
                        }
                        else if (num6 == 4)
                        {
                            if (num5 == 0)
                            {
                                Main.tile[x, j].frameX = 22;
                                Main.tile[x, j].frameY = 66;
                            }
                            if (num5 == 1)
                            {
                                Main.tile[x, j].frameX = 22;
                                Main.tile[x, j].frameY = 88;
                            }
                            if (num5 == 2)
                            {
                                Main.tile[x, j].frameX = 22;
                                Main.tile[x, j].frameY = 110;
                            }
                        }
                        else if (num6 == 5)
                        {
                            if (num5 == 0)
                            {
                                Main.tile[x, j].frameX = 88;
                                Main.tile[x, j].frameY = 0;
                            }
                            if (num5 == 1)
                            {
                                Main.tile[x, j].frameX = 88;
                                Main.tile[x, j].frameY = 22;
                            }
                            if (num5 == 2)
                            {
                                Main.tile[x, j].frameX = 88;
                                Main.tile[x, j].frameY = 44;
                            }
                        }
                        else if (num6 == 6)
                        {
                            if (num5 == 0)
                            {
                                Main.tile[x, j].frameX = 66;
                                Main.tile[x, j].frameY = 66;
                            }
                            if (num5 == 1)
                            {
                                Main.tile[x, j].frameX = 66;
                                Main.tile[x, j].frameY = 88;
                            }
                            if (num5 == 2)
                            {
                                Main.tile[x, j].frameX = 66;
                                Main.tile[x, j].frameY = 110;
                            }
                        }
                        else if (num6 == 7)
                        {
                            if (num5 == 0)
                            {
                                Main.tile[x, j].frameX = 110;
                                Main.tile[x, j].frameY = 66;
                            }
                            if (num5 == 1)
                            {
                                Main.tile[x, j].frameX = 110;
                                Main.tile[x, j].frameY = 88;
                            }
                            if (num5 == 2)
                            {
                                Main.tile[x, j].frameX = 110;
                                Main.tile[x, j].frameY = 110;
                            }
                        }
                        else
                        {
                            if (num5 == 0)
                            {
                                Main.tile[x, j].frameX = 0;
                                Main.tile[x, j].frameY = 0;
                            }
                            if (num5 == 1)
                            {
                                Main.tile[x, j].frameX = 0;
                                Main.tile[x, j].frameY = 22;
                            }
                            if (num5 == 2)
                            {
                                Main.tile[x, j].frameX = 0;
                                Main.tile[x, j].frameY = 44;
                            }
                        }

                        if (num6 == 5 || num6 == 7)
                        {
                            Main.tile[x - 1, j].active(true);
                            Main.tile[x - 1, j].type = 5;
                            num5 = WorldGen.genRand.Next(3);
                            if (WorldGen.genRand.Next(3) < 2)
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[x - 1, j].frameX = 44;
                                    Main.tile[x - 1, j].frameY = 198;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[x - 1, j].frameX = 44;
                                    Main.tile[x - 1, j].frameY = 220;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[x - 1, j].frameX = 44;
                                    Main.tile[x - 1, j].frameY = 242;
                                }
                            }
                            else
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[x - 1, j].frameX = 66;
                                    Main.tile[x - 1, j].frameY = 0;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[x - 1, j].frameX = 66;
                                    Main.tile[x - 1, j].frameY = 22;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[x - 1, j].frameX = 66;
                                    Main.tile[x - 1, j].frameY = 44;
                                }
                            }
                        }
                        if (num6 == 6 || num6 == 7)
                        {
                            Main.tile[x + 1, j].active(true);
                            Main.tile[x + 1, j].type = 5;
                            num5 = WorldGen.genRand.Next(3);
                            if (WorldGen.genRand.Next(3) < 2)
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[x + 1, j].frameX = 66;
                                    Main.tile[x + 1, j].frameY = 198;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[x + 1, j].frameX = 66;
                                    Main.tile[x + 1, j].frameY = 220;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[x + 1, j].frameX = 66;
                                    Main.tile[x + 1, j].frameY = 242;
                                }
                            }
                            else
                            {
                                if (num5 == 0)
                                {
                                    Main.tile[x + 1, j].frameX = 88;
                                    Main.tile[x + 1, j].frameY = 66;
                                }
                                if (num5 == 1)
                                {
                                    Main.tile[x + 1, j].frameX = 88;
                                    Main.tile[x + 1, j].frameY = 88;
                                }
                                if (num5 == 2)
                                {
                                    Main.tile[x + 1, j].frameX = 88;
                                    Main.tile[x + 1, j].frameY = 110;
                                }
                            }
                        }
                    }

                    int num7 = WorldGen.genRand.Next(3);
                    bool flag3 = false;
                    bool flag4 = false;
                    if (Main.tile[x - 1, y].nactive() && !Main.tile[x - 1, y].halfBrick() && 
                        Main.tile[x - 1, y].slope() == 0 && Array.Exists(tilesAllowGrowth, element => Main.tile[x - 1, y].type == element))
                    {
                        flag3 = true;
                    }
                    if (Main.tile[x + 1, y].nactive() && !Main.tile[x + 1, y].halfBrick() && Main.tile[x + 1, y].slope() == 0 && 
                        Array.Exists(tilesAllowGrowth, element => Main.tile[x + 1, y].type == element))
                    {
                        flag4 = true;
                    }

                    if (!flag3)
                    {
                        if (num7 == 0)
                        {
                            num7 = 2;
                        }
                        if (num7 == 1)
                        {
                            num7 = 3;
                        }
                    }
                    if (!flag4)
                    {
                        if (num7 == 0)
                        {
                            num7 = 1;
                        }
                        if (num7 == 2)
                        {
                            num7 = 3;
                        }
                    }
                    if (flag3 && !flag4)
                    {
                        num7 = 2;
                    }
                    if (flag4 && !flag3)
                    {
                        num7 = 1;
                    }
                    if (num7 == 0 || num7 == 1)
                    {
                        Main.tile[x + 1, y - 1].active(true);
                        Main.tile[x + 1, y - 1].type = 5;
                        num5 = WorldGen.genRand.Next(3);
                        if (num5 == 0)
                        {
                            Main.tile[x + 1, y - 1].frameX = 22;
                            Main.tile[x + 1, y - 1].frameY = 132;
                        }
                        if (num5 == 1)
                        {
                            Main.tile[x + 1, y - 1].frameX = 22;
                            Main.tile[x + 1, y - 1].frameY = 154;
                        }
                        if (num5 == 2)
                        {
                            Main.tile[x + 1, y - 1].frameX = 22;
                            Main.tile[x + 1, y - 1].frameY = 176;
                        }
                    }
                    if (num7 == 0 || num7 == 2)
                    {
                        Main.tile[x - 1, y - 1].active(true);
                        Main.tile[x - 1, y - 1].type = 5;
                        num5 = WorldGen.genRand.Next(3);
                        if (num5 == 0)
                        {
                            Main.tile[x - 1, y - 1].frameX = 44;
                            Main.tile[x - 1, y - 1].frameY = 132;
                        }
                        if (num5 == 1)
                        {
                            Main.tile[x - 1, y - 1].frameX = 44;
                            Main.tile[x - 1, y - 1].frameY = 154;
                        }
                        if (num5 == 2)
                        {
                            Main.tile[x - 1, y - 1].frameX = 44;
                            Main.tile[x - 1, y - 1].frameY = 176;
                        }
                    }

                    num5 = WorldGen.genRand.Next(3);
                    if (num7 == 0)
                    {
                        if (num5 == 0)
                        {
                            Main.tile[x, y - 1].frameX = 88;
                            Main.tile[x, y - 1].frameY = 132;
                        }
                        if (num5 == 1)
                        {
                            Main.tile[x, y - 1].frameX = 88;
                            Main.tile[x, y - 1].frameY = 154;
                        }
                        if (num5 == 2)
                        {
                            Main.tile[x, y - 1].frameX = 88;
                            Main.tile[x, y - 1].frameY = 176;
                        }
                    }
                    else if (num7 == 1)
                    {
                        if (num5 == 0)
                        {
                            Main.tile[x, y - 1].frameX = 0;
                            Main.tile[x, y - 1].frameY = 132;
                        }
                        if (num5 == 1)
                        {
                            Main.tile[x, y - 1].frameX = 0;
                            Main.tile[x, y - 1].frameY = 154;
                        }
                        if (num5 == 2)
                        {
                            Main.tile[x, y - 1].frameX = 0;
                            Main.tile[x, y - 1].frameY = 176;
                        }
                    }
                    else if (num7 == 2)
                    {
                        if (num5 == 0)
                        {
                            Main.tile[x, y - 1].frameX = 66;
                            Main.tile[x, y - 1].frameY = 132;
                        }
                        if (num5 == 1)
                        {
                            Main.tile[x, y - 1].frameX = 66;
                            Main.tile[x, y - 1].frameY = 154;
                        }
                        if (num5 == 2)
                        {
                            Main.tile[x, y - 1].frameX = 66;
                            Main.tile[x, y - 1].frameY = 176;
                        }
                    }
                    if (WorldGen.genRand.Next(8) != 0)
                    {
                        num5 = WorldGen.genRand.Next(3);
                        if (num5 == 0)
                        {
                            Main.tile[x, y - height].frameX = 22;
                            Main.tile[x, y - height].frameY = 198;
                        }
                        if (num5 == 1)
                        {
                            Main.tile[x, y - height].frameX = 22;
                            Main.tile[x, y - height].frameY = 220;
                        }
                        if (num5 == 2)
                        {
                            Main.tile[x, y - height].frameX = 22;
                            Main.tile[x, y - height].frameY = 242;
                        }
                    }
                    else
                    {
                        num5 = WorldGen.genRand.Next(3);
                        if (num5 == 0)
                        {
                            Main.tile[x, y - height].frameX = 0;
                            Main.tile[x, y - height].frameY = 198;
                        }
                        if (num5 == 1)
                        {
                            Main.tile[x, y - height].frameX = 0;
                            Main.tile[x, y - height].frameY = 220;
                        }
                        if (num5 == 2)
                        {
                            Main.tile[x, y - height].frameX = 0;
                            Main.tile[x, y - height].frameY = 242;
                        }
                    }
                    WorldGen.RangeFrame(x - 2, y - height - 1, x + 2, y + 1);
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendTileSquare(-1, x, (int)((double)y - (double)height * 0.5), height + 1);
                    }
                }
            }
        }
    }
}

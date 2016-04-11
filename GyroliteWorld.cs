using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

namespace Gyrolite
{
    public class GyroliteWorld : ModWorld
    {
        public enum LiquidType
        {
            Water,
            Lava
        }
        public enum Direction
        {
            Left,
            Right
        }

        public static Direction RandomDirection()
        {
            return Main.rand.Next(2) == 0 ? Direction.Left : Direction.Right;
        }

        public static class Shape
        {
            public static void Circle(int x, int y, int radius, ushort tileType, bool chanceEdges = true, int chanceEdgeChance = 3, bool missTopHalf = false, bool wall = false, int wallType = 0) //chance edges means that it will occasionally not place tiles nearer the edge = not perfect circle
            {
                int yStart = y - radius;
                if (missTopHalf)
                {
                    yStart = y - 1;
                }
                for (int i = x - radius; i < x + radius; i++)
                {
                    for (int j = yStart; j < y + radius; j++)
                    {
                        float distance = Math.Abs(Vector2.Distance(new Vector2(i, j), new Vector2(x, y)));
                        if (distance > radius - 1.1f)
                        {
                            if (Main.rand.Next(chanceEdgeChance) == 0)
                            {
                                goto Fin;
                            }
                        }
                        if (distance < radius)
                        {
                            if (j == y - 1 && missTopHalf && Main.rand.Next(3) == 0)
                            {
                                goto Fin;
                            }
                            if (Main.tile[i, j] == null)
                                Main.tile[i, j] = new Tile();
                            Main.tile[i, j].active(false);
                            Main.tile[i, j].halfBrick(false);
                            Main.tile[i, j].slope(0);
                            Main.tile[i, j].liquid = 0;
                            if (tileType == 9999)
                            {
                                goto Fin;
                            }
                            WorldGen.PlaceTile(i, j, tileType, false, true);
                            if (wall && distance < radius - 1.04f) //not the last tile around the circle hopefully.
                            {
                                WorldGen.KillWall(i, j);
                                WorldGen.PlaceWall(i, j, wallType);
                            }
                        }
                        Fin:;
                    }
                }
            }
            
            //Made a new method for this because I felt like it :P
            public static void CircleOfLiquid(int x, int y, int radius, LiquidType liquidType, bool chanceEdges = true, int chanceEdgeChance = 3)
            {
                int yStart = y - radius;
                for (int i = x - radius; i < x + radius; i++)
                {
                    for (int j = yStart; j < y + radius; j++)
                    {
                        float distance = Math.Abs(Vector2.Distance(new Vector2(i, j), new Vector2(x, y)));
                        if (distance > radius - 1.1f)
                        {
                            if (Main.rand.Next(chanceEdgeChance) == 0)
                            {
                                goto Fin;
                            }
                        }
                        if (distance < radius)
                        {
                            if (Main.tile[i, j] == null)
                                Main.tile[i, j] = new Tile();
                            Main.tile[i, j].active(false);
                            if (liquidType == LiquidType.Lava)
                            {
                                Main.tile[i, j].lava(true);
                            }
                            else
                            {
                                Liquid.AddWater(i, j);
                            }
                            Main.tile[i, j].liquid = 255;
                        }
                    Fin: ;
                    }
                }
            }
        }

        //@Eldrazi
        //Wasn't really sure where to put this but it annoys me when there isn't one so: (can't do extensions :/)
        public static float NextFloat(Random rand, float minimum, float maximum)
        {
            if (maximum < minimum)
            {
                throw new ArgumentException("Maximum must be higher than the minimum.");
            }
            return (float)rand.NextDouble() * (maximum - minimum) + minimum;
        } 

        public static int minAuraZoneTiles = 50;
        public static int auraTiles = 0;

        public static int minPermafrostZoneTiles = 80;
        public static int permafrostTiles = 0;

        public const int AuraConversionType = 12;

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
            tasks.Insert(PermafrostIndex, new PassLegacy("Volcano", delegate(GenerationProgress progress)
            {
                progress.Message = "Generating Volcano...";

                Mod mod = ModLoader.GetMod("Gyrolite");
                int xPoint = 2600; //randomize in future
                Begin:
                int yPoint = 0;
                for (int y = 0; y < 800; y++)
                {
                    if (Main.tile[xPoint, y] == null) Main.tile[xPoint, y] = new Tile();
                    if (Main.tile[xPoint, y].active())
                    {
                        for (int newY = y; newY < y + 60; newY++)
                        {
                            if (Main.tile[xPoint, newY].type == TileID.Cloud || Main.tile[xPoint - 3, newY].type == TileID.Cloud || Main.tile[xPoint + 3, newY].type == TileID.Cloud)
                            {
                                //xPoint = Main.rand.Next(800, Main.maxTilesX - 800);
                                xPoint = 1600;
                                goto Begin;
                            }
                        }
                        yPoint = y - 90;
                        if (yPoint < 60)
                        {
                            yPoint += 30;
                        }
                        break;
                    }
                }
                float radius = Main.rand.Next(25, 32);
                float radiusAdditive = Main.rand.Next(12, 19); //add to the radius to widen the volcano
                int num = 0;
                int height = Main.rand.Next(280, 360);
                for (int i = yPoint; i < yPoint + height; i += 7)
                {
                    progress.Set(0.5f);
                    num += 4;
                    radius += radiusAdditive * 0.062f * (num / 10);
                    if (radius > 70)
                    {
                        radius -= ((radiusAdditive * 0.062f * (num / 10)) / 2); //only add half radius if above 45
                    }
                    if (radius > 90)
                    {
                        radius = 90 + Main.rand.Next(-7, 8);
                    }
                    if (i < yPoint + 15)
                    {
                        Shape.Circle(xPoint, i, (int)radius, (ushort)mod.TileType("Basalt"), true, 4, true, true, mod.WallType("BasaltWall"));
                        i += 5;
                    }
                    else
                        Shape.Circle(xPoint, i, (int)radius, (ushort)mod.TileType("Basalt"), true, 3, true, true, mod.WallType("BasaltWall"));
                    if (i < yPoint + height - 90 && Main.rand.Next(14) == 0)
                        SideVent(xPoint, i, RandomDirection(), Main.rand.Next(3, 5), yPoint);
                }

                //CHASMS

                VolcanoChasm(xPoint + Main.rand.Next(-30, -14), yPoint + height - Main.rand.Next(40, 70), Direction.Left);
                VolcanoChasm(xPoint + Main.rand.Next(13, 31), yPoint + height - Main.rand.Next(50, 80), Direction.Right);
                if (Main.rand.Next(2) == 0)
                {
                    Direction newDirection = RandomDirection();
                    VolcanoChasm(xPoint + Main.rand.Next(13, 31), yPoint + height - Main.rand.Next(20, 30),  newDirection, 18, -2, 3);
                    if (Main.rand.Next(5) == 0)
                    {
                        Direction newerDirection = newDirection == Direction.Left ? Direction.Right : Direction.Left;
                        VolcanoChasm(xPoint + Main.rand.Next(13, 31), yPoint + height - Main.rand.Next(20, 30) , newerDirection, 18, -2, 3);
                    }
                }

                //

                radius = Main.rand.Next(8, 14);
                for (int i = yPoint; i < yPoint + height; i += 7)
                {
                    if (i < yPoint + height - 60)
                        Shape.Circle(xPoint, i, (int)radius + Main.rand.Next(-3, 4), (ushort)(9999), true, 2); //inside tunnel :P
                    else
                        Shape.Circle(xPoint + Main.rand.Next(-10, 11), i, (int)(radius * 4) + Main.rand.Next(-18, 7), (ushort)9999, true, 4);
                    if (i > yPoint + height - 60)
                    {
                        Shape.CircleOfLiquid(xPoint, i, (int)radius - 4, LiquidType.Lava, false, 4);
                    }
                }
            }));
            #region permafrost
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
            #endregion
        }

        public static void SideVent(int x, int y, Direction direction, int radius, int maxHeight, int min = -5, int max = 6)
        {
            int amount = Main.rand.Next(80, 150);
            for (int i = 0; i < amount; i++)
            {
                if (i < 12)
                {
                    Shape.CircleOfLiquid(x, y, radius, LiquidType.Lava, true, Main.rand.Next(3, 5));
                    if (!Main.tile[x - radius, y-radius].active() || !Main.tile[x + radius, y-radius].active())
                    {
                        break;
                    }
                }
                else
                {
                    Shape.Circle(x, y, radius, (ushort)9999, true, Main.rand.Next(3, 5));
                    if (!Main.tile[x - radius, y - radius].active() || !Main.tile[x + radius, y - radius].active())
                    {
                        break;
                    }
                }
                int xOffsetMinimum = direction == Direction.Left ? min : -1;
                int xOffsetMaximum = direction == Direction.Left ? 1 : max;
                int xOffset = Main.rand.Next(xOffsetMinimum, xOffsetMaximum);
                x += xOffset;
                y -= Main.rand.Next(1, 4);
                if (y < maxHeight + 20)
                {
                    break;
                }
            }
        }

        public static void VolcanoChasm(int x, int y, Direction favouredDirection, int baseRadius = 16, int min = -4, int max = 5)
        {
            Mod mod = ModLoader.GetMod("Gyrolite");
            int amount = Main.rand.Next(350, 450);
            List<Vector2> TheMostUnOptimizedWayOfDoingThisPossibleButIDontReallyCareAtThisPoint = new List<Vector2>();
            TheMostUnOptimizedWayOfDoingThisPossibleButIDontReallyCareAtThisPoint.Add(new Vector2(x, y));
            for (int i = 0; i < amount; i++)
            {
                Shape.Circle(x, y, baseRadius + Main.rand.Next(-3, 4), (ushort)mod.TileType("Basalt"));
                int xOffsetMinimum = favouredDirection == Direction.Left ? min : -1;
                int xOffsetMaximum = favouredDirection == Direction.Left ? 1 : max;
                int xOffset = Main.rand.Next(xOffsetMinimum, xOffsetMaximum);
                x += xOffset;
                y += 3;
                if (y > Main.maxTilesY - 230)
                {
                    break;
                }
                TheMostUnOptimizedWayOfDoingThisPossibleButIDontReallyCareAtThisPoint.Add(new Vector2(x, y));
            }
            foreach(Vector2 v in TheMostUnOptimizedWayOfDoingThisPossibleButIDontReallyCareAtThisPoint)
            {
                Shape.CircleOfLiquid((int)v.X, (int)v.Y, 10 + Main.rand.Next(-4, 3), LiquidType.Lava);
            }
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
        
        public static void Convert(int centerX, int centerY, int conversionType, int size = 4)
        {
            Mod gm = ModLoader.GetMod("Gyrolite");

            for (int x = centerX - size; x <= centerX + size; ++x)
            {
                for (int y = centerY - size; y <= centerY + size; ++y)
                {
                    if (WorldGen.InWorld(x, y, 1) && Math.Abs(x - centerX) + Math.Abs(y - centerY) < 6)
                    {
                        int type = (int)Main.tile[x, y].type;
                        int wall = (int)Main.tile[x, y].wall;

                        if (conversionType == GyroliteWorld.AuraConversionType)
                        {
                            /*if (WallID.Sets.Conversion.Grass[wall] && wall != 70)
                            {
                                Main.tile[x, y].wall = 70;
                                WorldGen.SquareWallFrame(x, y, true);
                                NetMessage.SendTileSquare(-1, x, y, 1);
                            }
                            else if (WallID.Sets.Conversion.Stone[wall] && wall != 28)
                            {
                                Main.tile[x, y].wall = 28;
                                WorldGen.SquareWallFrame(x, y, true);
                                NetMessage.SendTileSquare(-1, x, y, 1);
                            }
                            else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != 219)
                            {
                                Main.tile[x, y].wall = 219;
                                WorldGen.SquareWallFrame(x, y, true);
                                NetMessage.SendTileSquare(-1, x, y, 1);
                            }
                            else if (WallID.Sets.Conversion.Sandstone[wall] && wall != 222)
                            {
                                Main.tile[x, y].wall = 222;
                                WorldGen.SquareWallFrame(x, y, true);
                                NetMessage.SendTileSquare(-1, x, y, 1);
                            }*/
                            if ((Main.tileMoss[type] || TileID.Sets.Conversion.Stone[type]) && type != gm.TileType("Aurastone"))
                            {
                                Main.tile[x, y].type = (ushort)gm.TileType("Aurastone");
                                WorldGen.SquareTileFrame(x, y, true);
                                NetMessage.SendTileSquare(-1, x, y, 1);
                            }
                            else if (TileID.Sets.Conversion.Grass[type] && type != gm.TileType("Auragrass"))
                            {
                                Main.tile[x, y].type = (ushort)gm.TileType("Auragrass");
                                WorldGen.SquareTileFrame(x, y, true);
                                NetMessage.SendTileSquare(-1, x, y, 1);
                            }
                            else if (TileID.Sets.Conversion.Ice[type] && type != gm.TileType("SoulIce"))
                            {
                                Main.tile[x, y].type = (ushort)gm.TileType("SoulIce");
                                WorldGen.SquareTileFrame(x, y, true);
                                NetMessage.SendTileSquare(-1, x, y, 1);
                            }
                            else if (TileID.Sets.Conversion.Sand[type] && type != gm.TileType("Aurasand"))
                            {
                                Main.tile[x, y].type = (ushort)gm.TileType("Aurasand");
                                WorldGen.SquareTileFrame(x, y, true);
                                NetMessage.SendTileSquare(-1, x, y, 1);
                            }
                            /*else if (TileID.Sets.Conversion.HardenedSand[type] && type != 402)
                            {
                                Main.tile[x, y].type = 402;
                                WorldGen.SquareTileFrame(x, y, true);
                                NetMessage.SendTileSquare(-1, x, y, 1);
                            }
                            else if (TileID.Sets.Conversion.Sandstone[type] && type != 403)
                            {
                                Main.tile[x, y].type = 403;
                                WorldGen.SquareTileFrame(x, y, true);
                                NetMessage.SendTileSquare(-1, x, y, 1);
                            }
                            else if (TileID.Sets.Conversion.Thorn[type])
                            {
                                WorldGen.KillTile(x, y, false, false, false);
                                if (Main.netMode == 1)
                                {
                                    NetMessage.SendData(17, -1, -1, "", 0, (float)x, (float)y, 0f, 0, 0, 0);
                                }
                            }*/
                            /*if (type == 59 && (Main.tile[x - 1, y].type == gm.TileType("Auragrass") || Main.tile[x + 1, y].type == gm.TileType("Auragrass") || Main.tile[x, y - 1].type == gm.TileType("Auragrass") || Main.tile[x, y + 1].type == gm.TileType("Auragrass")))
                            {
                                Main.tile[x, y].type = 0;
                                WorldGen.SquareTileFrame(x, y, true);
                                NetMessage.SendTileSquare(-1, x, y, 1);
                            }*/
                        }
                    }
                }
            }
        }
    }
}

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Mounts
{
    public class Wyvern : ModMountData
    {
        public override void SetDefaults()
        {
            mountData.buff = mod.BuffType("WyvernMountBuff");

            mountData.heightBoost = 16;
            mountData.flightTimeMax = -1;
            mountData.fatigueMax = 320;
            mountData.fallDamage = 0f;
            mountData.usesHover = true;
            mountData.runSpeed = 8f;
            mountData.dashSpeed = 8f;
            mountData.acceleration = 0.16f;
            mountData.jumpHeight = 10;
            mountData.jumpSpeed = 4f;
            mountData.blockExtraJumps = true;
            mountData.totalFrames = 1;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 20;
            }
            mountData.playerYOffsets = array;

            mountData.yOffset = 8;
            mountData.xOffset = 14;

            mountData.bodyFrame = 3;
            mountData.playerHeadOffset = 22;

            mountData.standingFrameCount = 1;
            mountData.standingFrameDelay = 0;
            mountData.standingFrameStart = 0;

            mountData.runningFrameCount = 1;
            mountData.runningFrameDelay = 0;
            mountData.runningFrameStart = 0;

            mountData.flyingFrameCount = 1;
            mountData.flyingFrameDelay = 0;
            mountData.flyingFrameStart = 0;

            mountData.inAirFrameCount = 1;
            mountData.inAirFrameDelay = 0;
            mountData.inAirFrameStart = 0;

            mountData.idleFrameCount = 1;
            mountData.idleFrameDelay = 0;
            mountData.idleFrameStart = 0;
            mountData.idleFrameLoop = true;

            mountData.swimFrameCount = mountData.inAirFrameCount;
            mountData.swimFrameDelay = mountData.inAirFrameDelay;
            mountData.swimFrameStart = mountData.inAirFrameStart;

            if (Main.netMode != 2)
            {
                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
            }
        }

        public override void UpdateEffects(Player player)
        {
            GyrolitePlayer modPlayer = (GyrolitePlayer)player.GetModPlayer(mod, "GyrolitePlayer");
            modPlayer.wyvernMount = true;
        }
    }
}

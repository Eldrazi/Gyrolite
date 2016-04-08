using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Gyrolite
{
    public class GyrolitePlayer : ModPlayer
    {
        public bool babyIchorStickerPet;
        public bool crystalSpiritPet;
        public bool dwarfFeederPet;
        public bool skyJellyPet;

        public int petKillStack;

        public bool vileGeyserMinion;
        public bool bloodyGeyserMinion;

        public bool wyvernMount;

        public bool yoyoStringBall;

        public int ringType;
        public int[] ringEffects = new int[2];
        public int ringFireCooldown;

        public bool frostBite;
        public int frostbiteTime;
        public int frostbiteMax;
        public bool frostbiteImmune;

        public bool ZoneAura;
        public bool ZonePermafrost;

        public override void ResetEffects()
        {
            this.babyIchorStickerPet = false;
            this.crystalSpiritPet = false;
            this.dwarfFeederPet = false;
            this.skyJellyPet = false;

            this.vileGeyserMinion = false;
            this.bloodyGeyserMinion = false;

            this.wyvernMount = false;

            this.yoyoStringBall = false;

            this.ringType = 0;
            this.ringEffects[0] = this.ringEffects[1] = 0;

            this.frostBite = false;
            this.frostbiteMax = 300; // Defaults to 5 seconds.
        }

        public override void PostUpdateBuffs()
        {
            if (!this.babyIchorStickerPet && !this.crystalSpiritPet && !this.skyJellyPet && !this.dwarfFeederPet)
            {
                this.petKillStack = 0;
            }
        }

        public override void PostUpdateEquips()
        {
            if (this.ringType > 0)
            {
                int target = -1;
                for (int i = 0; i < 200; ++i)
                {
                    float dist = 480;
                    if (Main.npc[i].active && !Main.npc[i].friendly && Main.npc[i].lifeMax > 5 && !Main.npc[i].townNPC)
                    {
                        float curDist = Vector2.Distance(player.Center, Main.npc[i].Center);
                        if (curDist < 160) // If distance is smaller than 10 tiles
                        {
                            if(!Main.npc[i].boss)
                                Main.npc[i].velocity *= 0.99F; // Slows nearby NPCs over time.

                            Main.npc[i].AddBuff(ringEffects[1], 120); // Apply a debuff corresponding to the ring type for 2 seconds.
                        }
                        if (Collision.CanHit(player.position, player.width, player.height, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                        {
                            if (curDist < dist)
                            {
                                target = i;
                                dist = curDist;
                            }
                        }
                    }
                }
                if (++ringFireCooldown >= 30 && target != -1)
                {
                    Vector2 dir = Main.npc[target].Center - player.Center;
                    float speed = 9f;
                    float mag = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                    if (mag > speed)
                    {
                        mag = speed / mag;
                    }
                    dir *= mag;
                    ringFireCooldown = 0;
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, dir.X, dir.Y, ringEffects[0], 25, 0, player.whoAmI, 0);
                }
            }

            if (Main.myPlayer == player.whoAmI)
            {
                if (!this.frostbiteImmune && this.ZonePermafrost && !player.immune)
                {
                    if (this.frostbiteTime > 0)
                        this.frostbiteTime--;
                    else
                    {
                        this.frostBite = true;
                    }
                }
                else
                {
                    if (this.frostbiteTime < this.frostbiteMax)
                    {
                        this.frostbiteTime++;
                        if (this.frostbiteTime > this.frostbiteMax)
                        {
                            this.frostbiteTime = this.frostbiteMax;
                        }
                    }
                }

                if (this.frostBite)
                {
                    player.moveSpeed -= 0.75F;
                    //this.Hurt(50, 0, false, false, Lang.deathMsg(-1, -1, -1, 2), false, -1);
                    //this.AddBuff(24, 210, true);
                    // Apply frostbite effects.
                }
            }
        }

        public override void UpdateBiomes()
        {
            this.ZoneAura = (GyroliteWorld.auraTiles >= GyroliteWorld.minAuraZoneTiles);
            this.ZonePermafrost = (GyroliteWorld.permafrostTiles >= GyroliteWorld.minPermafrostZoneTiles);
        }

        public override void UpdateBiomeVisuals()
        {
            player.ManageSpecialBiomeVisuals("Gyrolite:Aura", this.ZoneAura);
        }

        #region Visuals
        int[] ringAnimation = new int[2];
        float ringOrbRot = 0;
        float maxRingOrbRot = 6.28318548f;

        public readonly PlayerLayer RingEffect = new PlayerLayer("Gyrolite", "RingEffect", PlayerLayer.MiscEffectsBack, delegate(PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("Gyrolite");
            GyrolitePlayer effects = (GyrolitePlayer)drawPlayer.GetModPlayer(mod, "GyrolitePlayer");
            int drawX = (int)(drawInfo.position.X + (drawPlayer.width * 0.5F) - Main.screenPosition.X);
            int drawY = (int)(drawInfo.position.Y + (drawPlayer.height * 0.5F) - Main.screenPosition.Y);

            if (drawPlayer.active && !drawPlayer.outOfRange)
            {
                if (effects.ringOrbRot > effects.maxRingOrbRot)
                {
                    effects.ringOrbRot -= effects.maxRingOrbRot;
                }
                if (effects.ringOrbRot < -effects.maxRingOrbRot)
                {
                    effects.ringOrbRot += effects.maxRingOrbRot;
                }

                if (++effects.ringAnimation[0] > 6)
                {
                    effects.ringAnimation[0] = 0;
                    if (++effects.ringAnimation[1] > 2)
                    {
                        effects.ringAnimation[1] = 0;
                    }
                }
                
                if (!Main.gamePaused && Main.hasFocus)
                {
                    effects.ringOrbRot += 0.05f;
                }

                Texture2D texture = mod.GetTexture("Effects/Rings/RingEffect_" + effects.ringType);
                int frameHeight = texture.Height / 3;
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, effects.ringAnimation[1] * frameHeight, texture.Width, frameHeight), Color.White * 0.4f * Main.essScale, effects.ringOrbRot + 1.04719758f * (float)2f, new Vector2(texture.Width / 2f, (texture.Height / 3) / 2f), 0.8f, SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);
            }
        });

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (ringType != 0)
            {
                RingEffect.visible = true;
                layers.Insert(0, RingEffect);
            }
        }
        #endregion
    }
}

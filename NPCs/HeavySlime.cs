using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs
{
    public class HeavySlime : ModNPC
    {
        private void Explode()
        {
            Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 62);
            for (int num648 = 0; num648 < 20; num648++)
            {
                int num649 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num649].velocity *= 1.4f;
            }
            for (int num650 = 0; num650 < 10; num650++)
            {
                int num651 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, 6, 0f, 0f, 100, default(Color), 2.5f);
                Main.dust[num651].noGravity = true;
                Main.dust[num651].velocity *= 5f;
                num651 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num651].velocity *= 3f;
            }
            int num652 = Gore.NewGore(new Vector2(npc.Center.X, npc.Center.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num652].velocity *= 0.4f;
            Main.gore[num652].velocity.X += 1f;
            Main.gore[num652].velocity.Y += 1f;
            num652 = Gore.NewGore(new Vector2(npc.Center.X, npc.Center.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num652].velocity *= 0.4f;
            Main.gore[num652].velocity.X -= 1f;
            Main.gore[num652].velocity.Y += 1f;
            num652 = Gore.NewGore(new Vector2(npc.Center.X, npc.Center.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num652].velocity *= 0.4f;
            Main.gore[num652].velocity.X += 1f;
            Main.gore[num652].velocity.Y -= 1f;
            num652 = Gore.NewGore(new Vector2(npc.Center.X, npc.Center.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Main.gore[num652].velocity *= 0.4f;
            Main.gore[num652].velocity.X -= 1f;
            Main.gore[num652].velocity.Y -= 1f;
            for (int k = 0; k < 200; k++)
            {
                if (Main.player[k].active)
                {
                    if (Vector2.Distance(npc.Center, Main.player[k].Center) < 32f)
                    {
                        Main.player[k].Hurt(Main.rand.Next(65, 70), npc.direction * -1, false, false, " was blown to smithereens");
                    }
                }
            }
            npc.life = -1;
            npc.active = false;
        }
        public override void SetDefaults()
        {
            npc.name = "Heavy Slime";
            npc.width = 44;
            npc.height = 33;
            Main.npcFrameCount[npc.type] = 2;
            npc.aiStyle = 1;
            npc.damage = 1; //explosion does the damage
            npc.lifeMax = 65;
            npc.value = 90f; //90 copper
            npc.defense = 10;
            npc.soundHit = 1;
            npc.soundKilled = 1;
            npc.alpha = 80;
            npc.knockBackResist = 0.6f;
        }
        public override void FindFrame(int frameHeight)
        {
            Framing.Slime(npc, frameHeight);
        }
        public override float CanSpawn(NPCSpawnInfo spawnInfo)
        {
            return 0;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Explode();
            }
            else
            {
                int num431 = 0;
                while ((double)num431 < damage / (double)npc.lifeMax * 100.0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)hitDirection, -1f, npc.alpha, npc.color, npc.scale);
                    num431++;
                }
                return;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Explode();
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D tx = Main.itemTexture[ItemID.Grenade];
            SpriteEffects effect = npc.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(tx, new Vector2(npc.Center.X - Main.screenPosition.X, npc.Center.Y - Main.screenPosition.Y), null, Color.White * 0.3f, npc.rotation, default(Vector2), 1f, effect, 0f);
        }
    }
}

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.NPCs.Bosses.Ichnimis
{
    public class Ichnimis_Arm : ModNPC
    {
        // npc.ai[0] contains the current AI state of this NPC.
        // npc.ai[1] contains the current AI state of this NPC.
        // npc.ai[2] contains the index this NPC in the arms array of the parent (determines if arm = left or right).
        // npc.ai[3] contains the index of the parent (Ichnimis_Body).

        public int ArmSide
        {
            get { return npc.ai[2] == 0 ? -1 : 1; }
        }

        public override void SetDefaults()
        {
            npc.displayName = "Ichnimis";
            npc.name = "Ichnimis Arm";
            npc.width = 48;
            npc.height = 114;

            npc.damage = 20;
            npc.defense = 20;
            npc.lifeMax = 2000;
            npc.knockBackResist = 0;

            npc.noTileCollide = true;
            npc.noGravity = true;
        }

        public override bool PreAI()
        {
            NPC parent = Main.npc[(int)npc.ai[3]];
            npc.realLife = parent.whoAmI;

            // Idle
            if (npc.ai[0] == 0)
            {
                npc.rotation += 0.05F;
                npc.position = parent.Center + new Vector2((34 * ArmSide) * -parent.spriteDirection, -74);
            }

            return false;
        }

        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Microsoft.Xna.Framework.Color drawColor)
        {
            return false;
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Gyrolite.Items.Misc
{
    public class SoulMagmaSlime : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Magma Slime Soul";
            item.width = 12;
            item.height = 22;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.toolTip = "A soul from a magma slime.";
            item.rare = 1;
            item.maxStack = 999;
        }
        public override DrawAnimation GetAnimation()
        {
            return new DrawAnimationVertical(5, 4);
        }
    }
}

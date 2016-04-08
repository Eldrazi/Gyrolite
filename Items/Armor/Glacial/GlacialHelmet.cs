using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Armor.Glacial
{
    public class GlacialHelmet : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Head);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Glacial Helmet";
            item.width = 18;
            item.height = 18;
            item.toolTip = "?";
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 2;

            item.defense = 2;      
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {            
            return body.type == mod.ItemType("GlacialShardmail") && legs.type == mod.ItemType("GlacialGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            GyrolitePlayer p = player.GetModPlayer<GyrolitePlayer>(mod);
            p.frostbiteMax += 900;

            player.magicDamage += 0.08F;
            player.meleeDamage += 0.08F;
            player.rangedDamage += 0.08F;
            player.minionDamage += 0.08F;
            player.thrownDamage += 0.08F;
        }
    }
}

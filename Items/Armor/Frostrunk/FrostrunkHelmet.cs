using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Armor.Frostrunk
{
    public class FrostrunkHelmet : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Head);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Frostrunk Helmet";
            item.width = 18;
            item.height = 18;
            item.toolTip = "?";
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 2;
            item.defense = 2;      
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("FrostrunkBreastplate") && legs.type == mod.ItemType("FrostrunkGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.statDefense += 7;
            player.setBonus = "7 defence";
        }
    }
}

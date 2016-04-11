using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Armor.Radiant
{
    public class RadiantHelmet : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Head);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Radiant Helmet";
            item.width = 18;
            item.height = 18;
            item.toolTip = "A helmet made from Radiant Ore";
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 2;
            item.defense = 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("RadiantChestplate") && legs.type == mod.ItemType("RadiantLeggings");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "?";            
        }
    }
}

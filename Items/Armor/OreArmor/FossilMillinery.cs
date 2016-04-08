using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Armor.OreArmor
{
    public class FossilMillinery : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Head);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Fossil Millinery";
            item.width = 18;
            item.height = 18;
            item.toolTip = "+2 extra minions and +9% minion damage";
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 1;
            item.defense = 2;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.FossilShirt && legs.type == ItemID.FossilPants;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.maxMinions += 1;
            player.minionDamage += 0.06F;
            player.setBonus = "+1 extra minion and +6% minion damage";
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 2; // 
            player.minionDamage += 0.09F; // 9% increased minion damage.
        }
    }
}

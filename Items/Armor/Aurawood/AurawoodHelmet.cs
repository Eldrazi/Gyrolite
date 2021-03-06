﻿using System;
using System.Collections.Generic;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Armor.Aurawood
{
    public class AurawoodHelmet : ModItem
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Head);
            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Aurawood Helmet";
            item.width = 18;
            item.height = 18;
            item.toolTip = "A helmet made from Aurawood";
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 2;
            item.defense = 2;      
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("AurawoodBreastplate") && legs.type == mod.ItemType("AurawoodGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.statDefense += 7;
            player.setBonus = "7 defence";
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Aurawood", 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Gyrolite.Items.Weapons
{
    public class NavySolution : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Navy Solutiuon";
            item.width = 10;
            item.height = 12;
            item.toolTip = "Used by the Clentaminator";
            item.toolTip2 = "Spreads the aura";
            item.value = Item.buyPrice(0, 0, 25, 0);
            item.rare = 3;

            item.maxStack = 999;

            item.shoot = mod.ProjectileType("NavySpray") - ProjectileID.PureSpray;
            item.ammo = 780;

            item.consumable = true;
        }
    }
}

using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Gyrolite.Items.Weapons.Magic;

namespace Gyrolite
{
    public class Gyrolite : Mod
    {
        public static int flaskAmmo = 2000;

        public override void SetModInfo(out string name, ref ModProperties properties)
        {
            name = "Gyrolite";
            properties.Autoload = true;
            properties.AutoloadGores = true;
            properties.AutoloadSounds = true;
        }

        public static bool NoInvasion(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.invasion && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);
        }
        public static bool NoBiome(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            return !player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneHoly && !player.ZoneSnow && !player.ZoneUndergroundDesert;
        }
        public static GyrolitePlayer GetPlayer(Player player)
        {
            return (GyrolitePlayer)player.GetModPlayer(ModLoader.GetMod("Gyrolite"), "GyrolitePlayer");
        }
    }
}

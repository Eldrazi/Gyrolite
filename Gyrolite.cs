using System;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

using Gyrolite.Effects.Aura;
using Gyrolite.Items.Weapons.Magic;

namespace Gyrolite
{
    public class Gyrolite : Mod
    {
        public static int flaskAmmo = 2000;
        public static Effect effect;
        public static ContentManager manager;

        public Gyrolite()
        {
	        Properties = new ModProperties()
	        {                
		        Autoload = true,
		        AutoloadGores = true,
		        AutoloadSounds = true
	        };
        }

        public override void Load()
        {
            manager = new ContentManager(Main.instance.Content.ServiceProvider, ModLoader.ModPath);
            if (!Main.dedServ)
            {
                Filters.Scene["Gyrolite:Aura"] = new Filter(new AuraScreenShaderData("FilterMiniTower").UseColor(0.4f, 0.4f, 0.9f).UseOpacity(0.6f), EffectPriority.VeryHigh);
                SkyManager.Instance["Gyrolite:Aura"] = new AuraSky();
            }
            effect = manager.Load<Effect>("gyrolite_e");
        }

        public override void Unload()
        {
            manager.Unload();
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

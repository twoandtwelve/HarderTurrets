using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarderTurrets.Patches;
using HarmonyLib;
using LethalConfig.ConfigItems.Options;
using LethalConfig.ConfigItems;
using LethalConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarderTurrets
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("ainavt.lc.lethalconfig")]
    public class HarderTurrets : BaseUnityPlugin
    
    
    {
        private const string modGUID = "Jacky.HarderTurrets";
        private const string modName = "HarderTurrets";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static HarderTurrets Instance;

        public ConfigEntry<int> turretSpawnMultiplier;

        public static ManualLogSource logger;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            logger = base.Logger;

            logger.LogInfo("Harder turrets has awaken");

            turretSpawnMultiplier = Config.Bind("HarderTurrets Config", "Turret Spawn Multiplier", 2, "This value multiplies the number of Turret spawn!");
            var turretSpawnMultiplierSlider = new IntSliderConfigItem(turretSpawnMultiplier, new IntSliderOptions
            {
                Min = 0,
                Max = 30
            });
            LethalConfigManager.AddConfigItem(turretSpawnMultiplierSlider);


            harmony.PatchAll(typeof(RoundManagerPatch));
        }
    }
}

using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using HarmonyLib;
using System.Reflection;
using BS_Utils.Gameplay;
using BeatSaberMarkupLanguage.GameplaySetup;
using AntiCheese.UI;
using BeatSaberMarkupLanguage.Util;
using AntiCheese.Configuration;

namespace AntiCheese
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Harmony _harmony = new Harmony("com.renschi.anticheese");

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Log.Info("AntiCheese initialized.");
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
            
        }
        
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("OnApplicationStart");
            new GameObject("AntiCheeseController").AddComponent<AntiCheeseController>();
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            
        }

        [OnEnable]
        public async void OnEnable()
        {
            await MainMenuAwaiter.WaitForMainMenuAsync();
            GameplaySetup.Instance.AddTab("Anti Cheese", "AntiCheese.UI.MainView", MainView.instance);
        }


        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");
            _harmony.UnpatchSelf();
        }


        public static readonly string ModName = "Anti Cheese";
    }
}

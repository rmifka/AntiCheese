using AntiCheese.Configuration;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Util;
using BeatSaberMarkupLanguage.ViewControllers;
using System;
using System.Collections.Generic;


namespace AntiCheese.UI
{
    internal class MainView : NotifiableSingleton<MainView>
    {
        [UIValue("is-enabled")]
        public bool IsEnabled
        {
            get => PluginConfig.Instance.Enabled;
            set {
                PluginConfig.Instance.Enabled = value;
                if(value)
                {
                    Plugin.Log?.Debug(BS_Utils.Gameplay.ScoreSubmission.ProlongedModString);

                    BS_Utils.Gameplay.ScoreSubmission.ProlongedDisableSubmission(Plugin.ModName);
                    Plugin.Log?.Debug("Disabled Score Submission");

                }
                else
                {
                    Plugin.Log?.Debug(BS_Utils.Gameplay.ScoreSubmission.ProlongedModString);

                    BS_Utils.Gameplay.ScoreSubmission.RemoveProlongedDisable(Plugin.ModName);
                    Plugin.Log?.Debug("Enabled Score Submission");

                }
            }
        }
    }
}

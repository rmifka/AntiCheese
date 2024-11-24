using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BS_Utils.Gameplay;
using AntiCheese.Configuration;

namespace AntiCheese.Patches
{
    // Token: 0x02000009 RID: 9
    [HarmonyPatch(typeof(BoxCuttableBySaber), "RefreshRadius")]
    internal static class ChangeColliderSize
    {
        // Token: 0x0600009D RID: 157 RVA: 0x00002D74 File Offset: 0x00000F74
        private static void Prefix(BoxCollider ____collider)
        {
            if (____collider.gameObject.name.Contains("Small") && PluginConfig.Instance.Enabled)
            {
                ____collider.size = new Vector3(0.65f, 0.5f, 0.6f);
            }
        }
    }
    
}

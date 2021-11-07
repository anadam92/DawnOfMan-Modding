using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;

namespace IndependentPause {

    [HarmonyPatch(typeof(GameGuiManager), "setTimeScale", MethodType.Normal)]
    static class GameGuiManager_setTimeScale_Patch {

        [HarmonyPrefix]
        static bool Prefix(GameGuiManager __instance, float timeScale) {
            TransientSingleton<TimeManager>.CurrentInstance.setTimeScale(timeScale);
            Singleton<GuiManager>.Instance.showToast(StringList.get("speed") + " x" + TransientSingleton<TimeManager>.CurrentInstance.getTimeScale());
            __instance.requestRefresh();
            return false;
        }

    }

}

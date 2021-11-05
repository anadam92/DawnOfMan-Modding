using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;

namespace IndependentPause
{

    [HarmonyPatch(typeof(GameGuiManager), "togglePause", MethodType.Normal)]
    static class GameGuiManager_togglePause_Patch
    {
        static bool Prefix()
        {
            TimeManager currentInstance2 = TransientSingleton<TimeManager>.CurrentInstance;
            if (currentInstance2.getTimeScale() == 0f)
            {
                currentInstance2.unpause();
                Singleton<GuiManager>.Instance.showToast(StringList.get("speed") + " x" + TransientSingleton<TimeManager>.CurrentInstance.getTimeScale());
            }
            else
            {
                currentInstance2.fakePause();
                Singleton<GuiManager>.Instance.showToast(StringList.get("pause"));
            }
            return false;
        }
    }

}

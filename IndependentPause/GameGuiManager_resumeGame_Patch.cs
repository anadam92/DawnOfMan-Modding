using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using UnityEngine.Audio;

namespace IndependentPause
{

    [HarmonyPatch(typeof(GameGuiManager), "resumeGame", MethodType.Normal)]
    static class GameGuiManager_resumeGame_Patch
    {

        static MethodInfo method_showMainPanel
            = AccessTools.DeclaredMethod(typeof(GameGuiManager), "showMainPanel");

        static MethodInfo method_getUnpausedAudioSnapshot
            = AccessTools.DeclaredMethod(typeof(GameGuiManager), "getUnpausedAudioSnapshot");
        
        static bool Prefix(GameGuiManager __instance)
        {
            Traverse t = Traverse.Create(__instance);
            if (t.Field<GuiPanel>("mGameMenu").Value != null)
            {
                Singleton<GuiManager>.Instance.removePanel(t.Field<GuiPanel>("mGameMenu").Value);
            }
            t.Field<GuiPanel>("mGameMenu").Value = null;
            t.Field<GuiConfirmDialog>("mConfirmDialog").Value = null;
            t.Field<GuiLoadSavePanel>("mLoadSavePanel").Value = null;
            TransientSingleton<TimeManager>.CurrentInstance.unpause();
            ((AudioMixerSnapshot)MethodInvoker.GetHandler(method_getUnpausedAudioSnapshot)(__instance, null)).TransitionTo(1f);
            if (TransientSingleton<GameModeManager>.CurrentInstance.getCurrentGameMode().isPauseAllowed())
            {
                TransientSingleton<TimeManager>.CurrentInstance.fakePause();
            }
            if (TransientSingleton<Selection>.CurrentInstance.getCount() == 0)
            {
                MethodInvoker.GetHandler(method_showMainPanel)(__instance, null);
            }
            __instance.requestRefresh();
            return false;
        }
    }

}

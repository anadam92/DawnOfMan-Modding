using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;

namespace IndependentPause
{

    [HarmonyPatch(typeof(PrimalVisionManager), "disable", MethodType.Normal)]
    static class PrimalVisionManager_disable_Patch
    {

        static FieldInfo fi_mExtraObject
            = AccessTools.DeclaredField(typeof(PrimalVisionManager), "mExtraObject");

        static MethodInfo method_restoreMaterials
            = AccessTools.DeclaredMethod(typeof(PrimalVisionManager), "restoreMaterials");

        static bool Prefix(PrimalVisionManager __instance)
        {
            Shader.SetGlobalFloat(ShaderId.PrimalVisionMode, 0f);
            Singleton<MusicPlayer>.Instance.unpause();
            Singleton<SfxPlayer>.Instance.play2D(AudioList<AudioListGameUi>.Instance.PrimalVisionDisable);
            MethodInvoker.GetHandler(method_restoreMaterials)(__instance, null);
            AudioSetup.Instance.DefaultSnapshot.TransitionTo(0.5f);
            UnityEngine.Object.Destroy((GameObject)fi_mExtraObject.GetValue(__instance));
            TransientSingleton<GameCamera>.CurrentInstance.setDefaultLayerDistances();
            return false;
        }
    }

}

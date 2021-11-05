using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;

namespace IndependentPause
{

    [HarmonyPatch(typeof(PrimalVisionManager), "enable", MethodType.Normal)]
    static class PrimalVisionManager_enable_Patch
    {

        static FieldInfo fi_mPostEffectMaterial
            = AccessTools.DeclaredField(typeof(PrimalVisionManager), "mPostEffectMaterial");

        static bool Prefix(PrimalVisionManager __instance)
        {
            Singleton<MusicPlayer>.Instance.pause();
            Singleton<SfxPlayer>.Instance.play2D(AudioList<AudioListGameUi>.Instance.PrimalVisionEnable);
            PostEffectBehaviour.enable((Material)fi_mPostEffectMaterial.GetValue(__instance));
            __instance.setTransition(0f);
            AudioSetup.Instance.PrimalVisionSnapshot.TransitionTo(0.5f);
            return false;
        }
    }

}

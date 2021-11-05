using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;

namespace IndependentPause
{

    [HarmonyPatch(typeof(GuiTradePanel), "onClose", MethodType.Normal)]
    static class GuiTradePanel_onClose_Patch
    {

        static MethodInfo method_clear
            = AccessTools.DeclaredMethod(typeof(PrimalVisionManager), "clear");

        static bool Prefix(GuiTradePanel __instance)
        {
            MethodInvoker.GetHandler(method_clear)(__instance, null);
            return false;
        }
    }

}
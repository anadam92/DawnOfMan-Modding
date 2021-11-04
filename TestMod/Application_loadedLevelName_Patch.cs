using System;
using HarmonyLib;
using UnityEngine;

namespace TestMod
{
    [HarmonyPatch(typeof(Application), "loadedLevelName", MethodType.Getter)]
    static class Application_loadedLevelName_Patch
    {
        static void Postfix(ref string __result)
        {
            if (!Main.enabled)
                return;

            __result = "New Level Name";
        }
    }
}

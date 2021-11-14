using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;

namespace RaiderAlert {

    [HarmonyPatch(typeof(TitleStateSelectScenario), "isCategoryEnabled", MethodType.Normal)]
    static class TitleStateSelectScenario_isCategoryEnabled_Patch {

        [HarmonyPostfix]
        public static void Postfix(ref bool __result, ScenarioCategory category) {
            __result |= (category == ScenarioCategory.Development);
        }

    }

}

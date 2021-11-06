using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;

namespace IndependentPause {

    [HarmonyPatch(typeof(GuiTradePanel), "init", MethodType.Normal)]
    static class GuiTradePanel_init_Patch {

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler_removeRealPause(this IEnumerable<CodeInstruction> instructions) {
            MethodInfo propertyGetter_CurrentInstance
               = AccessTools.PropertyGetter(typeof(TransientSingleton<TimeManager>), "CurrentInstance");
            MethodInfo method_realPause
               = AccessTools.DeclaredMethod(typeof(TimeManager), "realPause");
            foreach (var instruction in instructions) {
                if (instruction.Calls(propertyGetter_CurrentInstance) || instruction.Calls(method_realPause)) {
                    continue;
                }
                else {
                    yield return instruction;
                }
            }
        }
    }

}

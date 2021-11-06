using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;

namespace IndependentPause {

    [HarmonyPatch(typeof(GuiTradePanel), "onClose", MethodType.Normal)]
    static class GuiTradePanel_onClose_Patch {

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(this IEnumerable<CodeInstruction> instructions) {
            MethodInfo propertyGetter_CurrentInstance 
                = AccessTools.PropertyGetter(typeof(TransientSingleton<>), "CurrentInstance");
            bool omit = false;
            foreach (var instruction in instructions) {
                omit =
                    instruction.Calls(propertyGetter_CurrentInstance) ||
                    (omit && !(instruction.opcode == System.Reflection.Emit.OpCodes.Ret));

                if (omit) {
                    continue;
                }
                else {
                    yield return instruction;
                }
            }
        }

    }

}
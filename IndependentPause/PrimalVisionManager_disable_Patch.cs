using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;

namespace IndependentPause {

    [HarmonyPatch(typeof(PrimalVisionManager), "disable", MethodType.Normal)]
    static class PrimalVisionManager_disable_Patch {

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(this IEnumerable<CodeInstruction> instructions) {
            MethodInfo propertyGetter_CurrentInstance
                = AccessTools.PropertyGetter(typeof(TransientSingleton<GameModeManager>), "CurrentInstance");
            MethodInfo method_unpause
                = AccessTools.DeclaredMethod(typeof(TimeManager), "unpause");
            IEnumerable<Predicate<CodeInstruction>> codeAnchors = new Predicate<CodeInstruction>[]{
                new Predicate<CodeInstruction>(ci => ci.Calls(propertyGetter_CurrentInstance)),
                new Predicate<CodeInstruction>(ci => ci.Calls(method_unpause)),
                new Predicate<CodeInstruction>(ci => ci.opcode == System.Reflection.Emit.OpCodes.Nop)
            };
            IEnumerator<Predicate<CodeInstruction>> enumerator_codeAnchors = codeAnchors.GetEnumerator();
            enumerator_codeAnchors.MoveNext();
            bool omit = false;
            bool stop = false;
            foreach (var instruction in instructions) {
                if (!stop && enumerator_codeAnchors.Current.Invoke(instruction)) {
                    omit = true;
                    stop = !enumerator_codeAnchors.MoveNext();
                }
                else if (stop) {
                    omit = false;
                }

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

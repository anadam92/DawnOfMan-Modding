using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;

namespace IndependentPause
{

    [HarmonyPatch(typeof(PrimalVisionManager), "disable", MethodType.Normal)]
    static class PrimalVisionManager_disable_Patch
    {

        static MethodInfo propertyGetter_CurrentInstance
            = AccessTools.PropertyGetter(typeof(TransientSingleton<>), "CurrentInstance");

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(this IEnumerable<CodeInstruction> instructions)
        {
            bool omit = false;
            foreach (var instruction in instructions)
            {
                if (!omit && instruction.Calls(propertyGetter_CurrentInstance))
                {
                    omit = true;
                    continue;
                }
                else if (omit && instruction.Is(System.Reflection.Emit.OpCodes.Nop, null))
                {
                    omit = false;
                    continue;
                }
                else if (omit)
                {
                    continue;
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }

}

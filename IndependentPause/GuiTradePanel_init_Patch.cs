using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;

namespace IndependentPause
{

    [HarmonyPatch(typeof(GuiTradePanel), "init", MethodType.Normal)]
    static class GuiTradePanel_init_Patch
    {

        static MethodInfo method_realPause
            = AccessTools.DeclaredMethod(typeof(TimeManager), "realPause");

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler_removeRealPause(this IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.Calls(method_realPause))
                {
                    continue;
                }
                yield return instruction;
            }
        }
    }

}

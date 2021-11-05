using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;

namespace IndependentPause
{

    [HarmonyPatch(typeof(GuiTradePanel), "onClose", MethodType.Normal)]
    static class GuiTradePanel_onClose_Patch
    {

        static MethodInfo propertyGetter_CurrentInstance
            = AccessTools.PropertyGetter(typeof(TransientSingleton<>), "CurrentInstance");

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(this IEnumerable<CodeInstruction> instructions)
        {
            bool omit = false;
            foreach (var instruction in instructions)
            {
                omit =
                    instruction.Calls(propertyGetter_CurrentInstance) ||
                    (omit && !instruction.Is(System.Reflection.Emit.OpCodes.Ret, null));

                if (omit)
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
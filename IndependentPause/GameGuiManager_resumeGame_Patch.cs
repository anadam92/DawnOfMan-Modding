using System;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace IndependentPause
{

    [HarmonyPatch(typeof(GameGuiManager), "resumeGame", MethodType.Normal)]
    static class GameGuiManager_resumeGame_Patch
    {

        static MethodInfo propertyGetter_CurrentInstance1
            = AccessTools.PropertyGetter(typeof(TransientSingleton<PrimalVisionManager>), "CurrentInstance");
        static MethodInfo propertyGetter_CurrentInstance2
            = AccessTools.PropertyGetter(typeof(TransientSingleton<GameModeManager>), "CurrentInstance");
        static MethodInfo method_fakePause
            = AccessTools.DeclaredMethod(typeof(TimeManager), "fakePause");

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(this IEnumerable<CodeInstruction> instructions)
        {
            bool omit = false;
            foreach (var instruction in instructions)
            {
                if (!omit && (
                        instruction.Calls(propertyGetter_CurrentInstance1) ||
                        instruction.Calls(propertyGetter_CurrentInstance2) ||
                        instruction.Calls(method_fakePause)
                    ))
                {
                    omit = true;
                }
                else if (omit && (instruction.opcode == System.Reflection.Emit.OpCodes.Nop))
                {
                    omit = false;
                }
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

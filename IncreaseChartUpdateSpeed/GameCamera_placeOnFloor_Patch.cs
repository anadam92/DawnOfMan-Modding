using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;
using DawnOfMan;

namespace IncreaseChartUpdateSpeed {

    [HarmonyPatch(typeof(ChartsManager), "tick", MethodType.Normal)]
    public static class ChartsManager_tick_Patch {

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
            foreach (CodeInstruction instruction in instructions) {
                if (400f.Equals (instruction.operand)) {
                    instruction.operand = 40f;
                }
                yield return instruction;
            }
        }

    }

}

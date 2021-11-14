using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;
using DawnOfMan;

namespace CameraHigh {

    [HarmonyPatch(typeof(GameCamera) , "placeOnFloor", MethodType.Normal )]
    public static class GameCamera_placeOnFloor_Patch {

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
            foreach (CodeInstruction instruction in instructions) {
                if (200f.Equals (instruction.operand)) {
                    instruction.operand = 2000f;
                }
                yield return instruction;
            }
        }

    }

}

using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;

namespace RaiderAlert {

    [HarmonyPatch(typeof(AttackManager), "startTension", MethodType.Normal)]
    static class AttackManager_startTension_Patch {

        [HarmonyPostfix]
        public static void Postfix(CombatState combatState) {
            if (combatState == CombatState.ExtremeTension) {
                TransientSingleton<SettlementManager>.CurrentInstance.setAlertLevel(AlertLevel.Alert);
            }
        }

    }

}

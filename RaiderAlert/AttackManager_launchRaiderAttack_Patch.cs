using System;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;

namespace RaiderAlert {

    [HarmonyPatch(typeof(AttackManager), "launchRaiderAttack", MethodType.Normal)]
    static class AttackManager_launchRaiderAttack_Patch {

        [HarmonyPostfix]
        public static void Prefix() {
            TransientSingleton<SettlementManager>.CurrentInstance.setAlertLevel(AlertLevel.Alert);
        }

    }

}

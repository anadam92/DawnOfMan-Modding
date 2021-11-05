using System;
using HarmonyLib;
using UnityEngine;

namespace MoreInputKeys
{
    [HarmonyPatch(typeof(MadrugaShared.Binding), "isValidKey", MethodType.Normal)]
    static class Binding_isValidKey_Patch
    {

        [HarmonyPrefix]
        static bool Prefix(ref bool __result, KeyCode code)
        {
            __result = isValidKey(code);
            return false;
        }

        private static bool isValidKey(KeyCode code)
        {
            switch (code)
            {
                case KeyCode.None:
                case KeyCode.Clear:
                case KeyCode.Pause:
                case KeyCode.Escape:
                case KeyCode.F9:
                case KeyCode.F10:
                case KeyCode.F11:
                case KeyCode.F12:
                case KeyCode.F13:
                case KeyCode.F14:
                case KeyCode.F15:
                case KeyCode.Numlock:
                case KeyCode.CapsLock:
                case KeyCode.ScrollLock:
                case KeyCode.RightShift:
                case KeyCode.LeftShift:
                case KeyCode.RightControl:
                case KeyCode.LeftControl:
                case KeyCode.RightAlt:
                case KeyCode.LeftAlt:
                case KeyCode.RightCommand:
                case KeyCode.LeftCommand:
                case KeyCode.LeftWindows:
                case KeyCode.RightWindows:
                case KeyCode.AltGr:
                case KeyCode.Help:
                case KeyCode.Print:
                case KeyCode.SysReq:
                case KeyCode.Break:
                case KeyCode.Menu:
                    return false;
                default:
                    return true;
            }
        }

    }
}

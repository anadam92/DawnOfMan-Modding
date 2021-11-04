using System;
using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;
using System.Reflection;

namespace TestMod
{

    static class Main
    {

        public static bool enabled;

        // Send a response to the mod manager about the launch status, success or not.
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            // modEntry.Info - Contains all fields from the 'Info.json' file.
            // modEntry.Path - The path to the mod folder e.g. '\Steam\steamapps\common\YourGame\Mods\TestMod\'.
            // modEntry.Active - Active or inactive.
            // modEntry.Logger - Writes logs to the 'Log.txt' file.
            // modEntry.OnToggle - The presence of this function will let the mod manager know that the mod can be safely disabled during the game.
            modEntry.OnToggle = OnToggle;
            // modEntry.OnGUI - Called to draw UI.
            // modEntry.OnSaveGUI - Called while saving.
            // modEntry.OnUpdate - Called by MonoBehaviour.Update.
            modEntry.OnUpdate = OnUpdate;
            // modEntry.OnLateUpdate - Called by MonoBehaviour.LateUpdate.
            // modEntry.OnFixedUpdate - Called by MonoBehaviour.FixedUpdate.
            
            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            return true; // If false the mod will show an error.
        }

        // Called when the mod is turned to on/off.
        // With this function you control an operation of the mod and inform users whether it is enabled or not.
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value /* active or inactive */)
        {
            enabled = value;
            modEntry.Logger.Log(Application.loadedLevelName);
            return true; // If true, the mod will switch the state. If not, the state will not change.
        }

        static void OnUpdate(UnityModManager.ModEntry modEntry, float dt)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {

            }
        }

    }

}
using System;
using System.IO;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace OutputGeneratedTerrain {

    [HarmonyPatch(typeof(TerrainGenerator), "generate", MethodType.Normal)]
    static class TerrainGenerator_generate_Patch {

        [HarmonyPostfix]
        public static void Postfix(
            TerrainGenerator __instance,
            GeneratedTerrain __result,
            HeightmapGenerator heightmapGenerator, TerrainParameters parameters, EnvironmentDefinition environment, ScenarioLocation location, RandomGenerator random
        ) {
            BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            string strPathFolderOutput = Path.Combine(Util.getFilesFolder(), "OutputGeneratedTerrain_Data");
            strPathFolderOutput = Path.Combine(strPathFolderOutput, string.Format("{0}.{1}.{2}", location.Id, location.Environment, location.Seed));
            FileInfo fiOutput;
            fiOutput = new FileInfo(Path.Combine(strPathFolderOutput, ".heightmap.ser"));
            using (Stream s = fiOutput.OpenWrite()) {
                binaryFormatter.Serialize(s, __result.getHeightmap());
            }
            fiOutput = new FileInfo(Path.Combine(strPathFolderOutput, ".alphamap.ser"));
            using (Stream s = fiOutput.OpenWrite()) {
                binaryFormatter.Serialize(s, __result.getAlphamap());
            }
            fiOutput = new FileInfo(Path.Combine(strPathFolderOutput, ".details.ser"));
            using (Stream s = fiOutput.OpenWrite()) {
                binaryFormatter.Serialize(s, __result.getDetails());
            }
        }

    }

}

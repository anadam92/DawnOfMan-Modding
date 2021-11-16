using System;
using System.IO;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

namespace OutputGeneratedTerrain {

    [HarmonyPatch(typeof(TerrainGenerator), "generate", MethodType.Normal)]
    static class TerrainGenerator_generate_Patch {

        private static BinaryFormatter _binaryFormatter;
        private static DirectoryInfo _folderOutput;

        public static BinaryFormatter binaryFormatter {
            get {
                if (TerrainGenerator_generate_Patch._binaryFormatter == null) {
                    TerrainGenerator_generate_Patch._binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                }
                return TerrainGenerator_generate_Patch._binaryFormatter;
            }
        }

        public static DirectoryInfo folderOutput {
            get {
                if (TerrainGenerator_generate_Patch._folderOutput == null) {
                    TerrainGenerator_generate_Patch._folderOutput = new DirectoryInfo(Path.Combine(Util.getFilesFolder(), "OutputGeneratedTerrain_Data"));
                    if (!_folderOutput.Exists) {
                        Directory.CreateDirectory(_folderOutput.FullName);
                    }
                }
                return TerrainGenerator_generate_Patch._folderOutput;
            }
        }


        [HarmonyPostfix]
        public static void Postfix(
            TerrainGenerator __instance,
            GeneratedTerrain __result,
            HeightmapGenerator heightmapGenerator, TerrainParameters parameters, EnvironmentDefinition environment, ScenarioLocation location, RandomGenerator random
        ) {

            string fileNameBase = string.Format("{0}.{1}.{2}", location.Id, location.Environment, location.Seed);

            FileInfo[] files = folderOutput.GetFiles(fileNameBase + "*", SearchOption.TopDirectoryOnly);
            if (files.Length > 0) {
                return;
            }

            saveArrayCompressed(fileNameBase, ".heightmap.ser.gz", __result.getHeightmap());
            saveArrayCompressed(fileNameBase, ".alphamap.ser.gz", __result.getAlphamap());
            saveArrayCompressed(fileNameBase, ".details.ser.gz", __result.getDetails());
        }

        private static void saveArrayCompressed(string fileNameBase, string arrayName, Array array) {
            string pathOutput = Path.Combine(folderOutput.FullName, fileNameBase + arrayName);
            using (Stream s = new FileStream(pathOutput, FileMode.Create, FileAccess.Write)) {
                using (GZipStream gz = new System.IO.Compression.GZipStream(s, CompressionMode.Compress)) {
                    binaryFormatter.Serialize(gz, array);
                }
            }
        }

    }

}

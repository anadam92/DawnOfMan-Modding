using System;
using System.IO;
using HarmonyLib;
using UnityEngine;
using MadrugaShared;
using DawnOfMan;
using System.Reflection;
using System.Collections.Generic;

namespace OutputXmlResources {

    [HarmonyPatch(typeof(GameStateInit), MethodType.Constructor)]
    static class GameStateInit_ctor_Patch {

        private static DirectoryInfo _folderOutput;

        public static DirectoryInfo folderOutput {
            get {
                if (GameStateInit_ctor_Patch._folderOutput == null) {
                    GameStateInit_ctor_Patch._folderOutput = new DirectoryInfo(Path.Combine(Util.getFilesFolder(), "OutputXmlResources_Data"));
                    if (!_folderOutput.Exists) {
                        Directory.CreateDirectory(_folderOutput.FullName);
                    }
                }
                return GameStateInit_ctor_Patch._folderOutput;
            }
        }

        [HarmonyPostfix]
        public static void Postfix() {
            string pathBuiltinFolder = Util.getFolderPath("EntityTypes/entity_type_list");
            List<string> pathList = Deserializer.getPathList("EntityTypes/entity_type_list");
            System.IO.File.WriteAllText(System.IO.Path.Combine(folderOutput.FullName, "entity_type_list.txt"), string.Join("\r\n", pathList.ToArray()));
            foreach (string path in pathList) {
                string camelCase = path.Substring(path.LastIndexOf('/') + 1);
                string snakeCase = Util.camelCaseToLowerCase(camelCase);
                string path1 = string.Format("{0}/{1}/{2}", pathBuiltinFolder, path, Util.camelCaseToLowerCase(camelCase));
                string pathOutputFile = Path.Combine(folderOutput.FullName, snakeCase);
                pathOutputFile = Path.ChangeExtension(pathOutputFile, "xml");
                Deserializer.getDocumentElement(path1).OwnerDocument.Save(pathOutputFile);
            }
        }

    }

}

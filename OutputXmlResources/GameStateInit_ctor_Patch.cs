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

        [HarmonyPostfix]
        public static void Postfix() {
            string pathOutputFolder = Util.getFilesFolder() + "/OutputXmlResources_Data";
            string pathBuiltinFolder = Util.getFolderPath("EntityTypes/entity_type_list");
            List<string> pathList = Deserializer.getPathList("EntityTypes/entity_type_list");
            System.IO.File.WriteAllText(System.IO.Path.Combine(pathOutputFolder, "entity_type_list.txt"), string.Join("\r\n", pathList.ToArray()));
            foreach (string path in pathList) {
                string camelCase = path.Substring(path.LastIndexOf('/') + 1);
                string snakeCase = Util.camelCaseToLowerCase(camelCase);
                string path1 = string.Format("{0}/{1}/{2}", pathBuiltinFolder, path, Util.camelCaseToLowerCase(camelCase));
                string pathOutputFile = Path.ChangeExtension(Path.Combine(pathOutputFolder, snakeCase), "xml");
                Deserializer.getDocumentElement(path1).OwnerDocument.Save(pathOutputFile);
            }
        }

    }

}

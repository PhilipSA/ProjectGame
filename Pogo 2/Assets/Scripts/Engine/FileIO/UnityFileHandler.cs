using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Engine.FileIO
{
    public class UnityFileHandler
    {
        private string folderName;

        public UnityFileHandler(string directory)
        {
            folderName = Application.dataPath + directory;
        }

        public List<string> GetAllLevelScenesFromDirectory()
        {
            var sceneList = new List<string>();
            var dirInfo = new DirectoryInfo(folderName);
            var allFileInfos = dirInfo.GetFiles("*.unity", SearchOption.AllDirectories);
            foreach (var fileInfo in allFileInfos)
            {
                sceneList.Add(Path.GetFileNameWithoutExtension(fileInfo.Name));
            }
            return sceneList;
        }
    }
}

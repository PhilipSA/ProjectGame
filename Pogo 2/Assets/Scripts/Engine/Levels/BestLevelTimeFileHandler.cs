using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Engine.FileIO;

namespace Assets.Scripts.Engine.Levels
{
    public class BestLevelTimeFileHandler : BinaryFileHandler
    {
        public BestLevelTimeFileHandler(string fileName) : base(fileName)
        {
            
        }

        public float LoadBestTimeForLevel(string levelName)
        {
            var bestTimeList = LoadBestTimeFile(OpenFile());
            var levelData = bestTimeList.FirstOrDefault(x => x.LevelName == levelName);
            return levelData == null ? 0 : levelData.BestTime;
        }

        public void SaveBestTimeForLevel(string levelName, float time)
        {
            var binaryFormatter = new BinaryFormatter();
            var bestTimeList = new List<BestTimeData>();
            using (var fileStream = OpenFile())
            {
                var levelData = new BestTimeData(levelName, time);
                bestTimeList = LoadBestTimeFile(fileStream);

                if (bestTimeList.FirstOrDefault(x => x.LevelName.Equals(levelName)) != null)
                {
                    bestTimeList[bestTimeList.FindIndex(x => x.LevelName.Equals(levelName))] = levelData;                   
                }
                else
                {
                    bestTimeList.Add(levelData);
                }
            }

            using (var fileStream = OverwriteFile())
            {
                binaryFormatter.Serialize(fileStream, bestTimeList);
            }
        }

        public List<BestTimeData> LoadBestTimeFile(FileStream fileStream)
        {
            var binaryFormatter = new BinaryFormatter();
            try
            {
                return (List<BestTimeData>)binaryFormatter.Deserialize(fileStream);
            }
            catch (Exception)
            {
                return new List<BestTimeData>();
            }

        }
    }

    [Serializable]
    public class BestTimeData
    {
        public BestTimeData(string levelName, float bestTime)
        {
            LevelName = levelName;
            BestTime = bestTime;
        }

        public string LevelName { get; set; }
        public float BestTime { get; set; }
    }
}

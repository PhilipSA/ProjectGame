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

        public float LoadBestTimeForLevel(int levelIndex)
        {
            using (var fileStream = OpenFile())
            {
                var bestTimeList = LoadBestTimeFile(fileStream);
                var levelData = bestTimeList.FirstOrDefault(x => x.LevelIndex == levelIndex);
                return levelData == null ? 0 : levelData.BestTime;
            }
        }

        public void SaveBestTimeForLevel(int levelIndex, float time)
        {
            var binaryFormatter = new BinaryFormatter();
            var bestTimeList = new List<BestTimeData>();
            using (var fileStream = OpenFile())
            {
                var levelData = new BestTimeData(levelIndex, time);
                bestTimeList = LoadBestTimeFile(fileStream);

                if (bestTimeList.FirstOrDefault(x => x.LevelIndex.Equals(levelIndex)) != null)
                {
                    bestTimeList[bestTimeList.FindIndex(x => x.LevelIndex.Equals(levelIndex))] = levelData;                   
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
        public BestTimeData(int levelIndex, float bestTime)
        {
            LevelIndex = levelIndex;
            BestTime = bestTime;
        }

        public int LevelIndex { get; set; }
        public float BestTime { get; set; }
    }
}

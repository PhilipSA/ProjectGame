using System.IO;
using UnityEngine;

namespace Assets.Scripts.Engine.FileIO
{
    public abstract class BinaryFileHandler
    {
        public string FileName { get; set; }
        protected string FilePath;

        protected BinaryFileHandler(string fileName)
        {
            FileName = fileName;
            FilePath = Application.persistentDataPath + "/" + FileName;
        }

        public FileStream OpenFile()
        {
            return File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public FileStream OverwriteFile()
        {
            return File.Open(FilePath, FileMode.Create, FileAccess.ReadWrite);
        }
    }
}

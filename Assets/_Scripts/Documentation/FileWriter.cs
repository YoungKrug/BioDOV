using System.IO;
using UnityEngine;

namespace _Scripts.Documentation
{
    public class FileWriter
    {
        public bool WriteToFile(string path, string data)
        {
            File.WriteAllText(path, data);
            string file = File.ReadAllText(path);
            Debug.Log(file);
            return !string.IsNullOrEmpty(file);
        }
    }
}
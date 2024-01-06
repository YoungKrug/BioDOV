using System;
using System.IO;
using UnityEngine;

namespace _Scripts.Documentation
{
    public class FileWriter
    {
        public bool WriteToFile(string path, string data)
        {
            try
            {
                File.WriteAllText(path, data);
                string file = File.ReadAllText(path);
                Debug.Log(file);
                return !string.IsNullOrEmpty(file);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            return false;
        }
    }
}
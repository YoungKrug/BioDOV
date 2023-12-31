using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.CSVData
{
    public class DataInserter : MonoBehaviour
    {
        // Start is called before the first frame update
        
        public Csv _csv;
        public BaseEventScriptableObject levelEventScriptableObject;
        public void Initialize()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "ExampleSimFile.csv");
            OnInsertEvent(path);
        }
        //Inserts 
        public bool OnInsertEvent(string path)
        {
            FileInserter inserter = new FileInserter(path);
            _csv = inserter.ReadData();
            if(levelEventScriptableObject)
                levelEventScriptableObject.OnEventRaised(_csv);
            return _csv.Data.Count > 0;
        }



    }
}

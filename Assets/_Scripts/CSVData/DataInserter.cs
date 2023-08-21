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
        private void Start()
        {
            OnInsertEvent(@"F:\MasterThesis_Project\BioDOV\Assets\SimulationFiles\DataExtra.csv");
        }

        public bool OnInsertEvent(string path)
        {
            FileInserter inserter = new FileInserter(path);
            _csv = inserter.ReadData();
            return _csv.Data.Count > 0;
        }



    }
}

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
        
        [SerializeField] private Csv _csv;
        [SerializeField] private SimulationManager _manager;
        [SerializeField] private BaseEventScriptableObject _eventScriptableObject;

        private void Start()
        {
            OnInsertEvent(@"F:\MasterThesis_Project\BioDOV\Assets\SimulationFiles\DataExtra.csv");
        }

        public bool OnInsertEvent(string path)
        {
            FileInserter inserter = new FileInserter(path);
            Csv csv = inserter.ReadData();
            CreateSimulation(csv);
            return csv.Data.Count > 0;
        }

        public bool CreateSimulation(Csv csv)
        {
            DefaultSimulation defaultSimulation = new DefaultSimulation(csv, _eventScriptableObject, 
                _manager.prefab.gameObject); // Zombie Code
            _manager.CsvData = csv; //Remove this and force it to work with UI *TODO*
            _manager.CurrentSimulation = defaultSimulation;
            _manager.OnEventSimulate();
            return _manager.CurrentSimulation == defaultSimulation;
        }

        

    }
}

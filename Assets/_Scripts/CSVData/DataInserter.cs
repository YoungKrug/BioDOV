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
        [SerializeField] private TextAsset csvAsset;
        [SerializeField] private Csv _csv;
        [SerializeField] private SimulationManager _manager;
        [SerializeField] private BaseEventScriptableObject _eventScriptableObject;

        private void Start()
        {
            OnInsertEvent();
        }

        private void OnInsertEvent()
        {
            Csv csv = new Csv();
            List<CsvNode> nodesList = new List<CsvNode>();
            string[] rowValues = csvAsset.text.Split("\n");
            List<string> columnValues = rowValues[0].Split(",").ToList();
            foreach (var csvValue in columnValues)
            {
                nodesList.Add(new CsvNode
                {
                    Name = csvValue
                });
            }
            for (int i = 1 ; i < rowValues.Length; i++)
            {
                columnValues = rowValues[i].Split(",").ToList();
                for (int j = 0; j < columnValues.Count; j++)
                {
                    double val = 0;
                    if (!double.TryParse(columnValues[j], out val))
                    {
                        val = 0;
                    }
                    nodesList[j].States.Add(val);
                }
            }
            csv.Data = nodesList;
            csv.Data.RemoveRange(0,3);
            foreach (var node in csv.Data)
            {
                node.CurrentState = node.States[0]; // Set them all to there initial State
            }
            DefaultSimulation defaultSimulation = new DefaultSimulation(csv, _eventScriptableObject, 
                _manager.prefab.gameObject); // Zombie Code
            _manager.CsvData = csv; //Remove this and force it to work with UI *TODO*
            _manager.CurrentSimulation = defaultSimulation;
            _manager.OnEventSimulate();
        }

        

    }
}

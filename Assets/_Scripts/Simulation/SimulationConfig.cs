using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Documentation;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation.SimulationSettings;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Simulation
{
    [System.Serializable]
    public struct SimulationConfig
    {
        public ISimulator CurrentSimulation;
        public Csv CsvData;
        public Button Button;
        public SimulationObject Prefab;
        public SimulationData Data;
        public BaseEventScriptableObject BaseEventScriptableObject;
        public BaseEventScriptableObject nextLevelScriptableObject;
        public ThresholdMeter Meter;
        public DocumentationWriter DocWriter;

    }
}
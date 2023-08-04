﻿using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation.SimulationSettings;
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
    }
}
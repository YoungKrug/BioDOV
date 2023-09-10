using System;
using System.Collections.Generic;
using _Scripts.Commands;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.Simulation
{
    public class SimulationManager: MonoBehaviour, IEventReactor // Move this to SimulationController
    {
        private readonly SimulationController _simulationController = new SimulationController();
        public SimulationConfig Config = new SimulationConfig();
        private bool _init;

        private void Awake()
        {
            Config.BaseEventScriptableObject.Subscribe(this);
            Config.Button.onClick.AddListener(Predict);
        }
        private void OnEventSimulate()
        {
            if (!_init)
            {
                Initialize();
                _init = true;
            }
            Config.CurrentSimulation.Simulate(Config);
        }

        private void Initialize()
        {
            Config = _simulationController.Initialize(Config);
        }

        public void Predict()
        {
            _simulationController.Predict(Config);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Config.CurrentSimulation.UndoCommand();
            }
        }

        public void Execute(object eventObject)
        {
            if (eventObject.GetType() == typeof(Csv))
            {
                Config.CsvData = (Csv) eventObject;
                return;
            }
            Config.CurrentSimulation = (ISimulator)eventObject;
            OnEventSimulate();
        }

    }
}
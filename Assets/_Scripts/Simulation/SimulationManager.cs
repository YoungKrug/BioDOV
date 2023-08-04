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
        public BaseEventScriptableObject baseEventScriptableObject;
        public BaseEventScriptableObject ScriptableObject => baseEventScriptableObject;
        private readonly SimulationController _simulationController = new SimulationController();
        public SimulationConfig Config = new SimulationConfig();
        private bool _init;
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
            baseEventScriptableObject.Subscribe(this);
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
            Config.CurrentSimulation = (ISimulator)eventObject;
            OnEventSimulate();
        }
    }
}
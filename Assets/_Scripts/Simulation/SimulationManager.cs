using System;
using System.Collections.Generic;
using _Scripts.Commands;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.Simulation
{
    public class SimulationManager: MonoBehaviour, IEventReactor, IInputReceivers // Move this to SimulationController
    {
        private readonly SimulationController _simulationController = new SimulationController();
        public BaseEventScriptableObject inputScriptableObject;
        public SimulationConfig Config = new SimulationConfig();
        public Image progressBar;
        public Gradient relationshipGradiant;
        private bool _init;
        public KeyCode[] Keys { get; } = { KeyCode.Escape, KeyCode.R };
        private void Start()
        {
            Config.BaseEventScriptableObject.Subscribe(this);
            inputScriptableObject.OnEventRaised(this);
            Config.Button.onClick.AddListener(Predict);
            Config.Meter = new ThresholdMeter(progressBar);
        }
        private void OnEventSimulate()
        {
            if (!_init)
            {
                Initialize();
                _init = true;
            }
            Config.lineGradient = relationshipGradiant;
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
        private void UndoCommandInSimulator()
        {
            Config.CurrentSimulation.UndoCommand();
        }

        private void ExitGame()
        {
            Application.Quit();
        }
        public void NextLevel()
        {
            Config.CurrentSimulation.Reset();
        }

        public void Execute(object eventObject)
        {
            if (eventObject.GetType() == typeof(Csv))
            {
                Csv csv = (Csv) eventObject;
                if (Config.CsvData == null)
                {
                    Config.CsvData = csv;
                    return;
                }
                Config.CsvData = csv;
                _init = false;
                OnEventSimulate();
                return;
            }
            if (eventObject is SimulationConfig)
            {
                SimulationConfig config = (SimulationConfig) eventObject;
                config.Data.Reset();
            }
            Config.CurrentSimulation = (ISimulator)eventObject;
            OnEventSimulate();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                UndoCommandInSimulator();
            }
        }

        public void WriteDocumentation()
        {
            Config.CurrentSimulation.FinishSimulation();
        }
        public void ExecuteKey(KeyCode code)
        {
            if (code == KeyCode.Escape)
            {
                ExitGame();
            }
        }
    }
}
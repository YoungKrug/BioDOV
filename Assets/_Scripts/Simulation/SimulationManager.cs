using System;
using System.Collections.Generic;
using _Scripts.Commands;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.Planning;
using _Scripts.Planning.Interfaces;
using _Scripts.ScriptableObjects;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.Simulation
{
    public class SimulationManager: MonoBehaviour, IEventReactor, IPlannable // Move this to SimulationController
    {
        private readonly SimulationController _simulationController = new SimulationController();
        public SimulationConfig Config = new SimulationConfig();
        public Image progressBar;
        private bool _init;
        public int Order => 2;

        private void Awake()
        {
            Config.BaseEventScriptableObject.Subscribe(this);
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

        public void WriteDocumentation()
        {
            Config.CurrentSimulation.FinishSimulation();
        }

        
        public bool CheckForPrerequisite(object obj)
        {
            return obj.GetType() == typeof(Csv);
        }

        public PlannerConfig PlannedExecution(PlannerConfig plannerConfig)
        {
            object preObject = plannerConfig.InvolvedObject;
            if (!CheckForPrerequisite(preObject))
            {
                plannerConfig.PlannerState = Planner.PlannerState.PlanFailed;
                return null;
            }
            Csv csv = (Csv) preObject;
            if (Config.CsvData == null)
            {
                Config.CsvData = csv;
                return null;
            }
            Config.CsvData = csv;
            _init = false;
            OnEventSimulate();
            plannerConfig.InvolvedObject = csv;
            return plannerConfig;
            
        }
    }
}
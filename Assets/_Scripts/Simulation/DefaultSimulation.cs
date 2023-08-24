using System.Collections.Generic;
using System.Linq;
using _Scripts.Commands;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation.SimulationSettings;
using _Scripts.Statistics;
using UnityEngine;

namespace _Scripts.Simulation
{
    public class DefaultSimulation : ISimulator // Invoker Class
    {
        private readonly BaseEventScriptableObject _baseEventScriptableObject;
        private readonly SimulationInvoker _simulationInvoker = new SimulationInvoker();
        private SimulationConfig _config;
        private bool _isInitialize = false;
        public DefaultSimulation(SimulationConfig config)
        {
            _config = config;
            _baseEventScriptableObject = config.BaseEventScriptableObject;
        }
   
        public bool Simulate(SimulationConfig config)
        {
            if (!_isInitialize)
            {
                Initialize(config.Data.AllCurrentObjects);
                _isInitialize = false;
                return true;
            }

            return false;
        }

        private bool Initialize(List<SimulationObject> simulationGameObjects)
        {
            _config.Data.AllCurrentObjects = simulationGameObjects;
            _config.Data.Prefab = _config.Prefab.gameObject;
            int count = _config.Data.AllCurrentObjects.Count;
            double[] simulatedObjectStates = new double[count];
            for (int i = 0; i < count; i++)
            {
                simulatedObjectStates[i] = _config.Data.AllCurrentObjects[i].Node.CurrentState;
            }

            foreach (var simulatedObject in simulationGameObjects)
            {
                simulatedObject.Node.PredictionModel =
                    new PartialLeastSquaresPredictionModel(simulatedObject.Node, _config.CsvData);
            }
            _config.Data.CurrentStates = simulatedObjectStates;
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new ChangeColorBasedOnStatesCommand());
            ExecuteCommand(commands, null);
            _simulationInvoker.RemoveRecentCommand(); //The initial command does is removed as the user
            //Should not be able to undo to the baseline state
            return true;
        }

       
        public bool ExecuteCommand(List<ICommand> commands, SimulationObject simulationObject)
        {
            _config.Data.CurrentInteractedObject = simulationObject;
            _simulationInvoker.ExecuteCommand(commands, ref _config.Data);
            return true;

        }
        public bool UndoCommand()
        {
            return _simulationInvoker.UndoCommands(ref _config.Data);
        }
        public void SetAsCurrentSimulator()
        {
            _baseEventScriptableObject.OnEventRaised(this);
        }
    }
}
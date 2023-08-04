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
        [SerializeField] private BaseEventScriptableObject _baseEventScriptableObject;
        private SimulationData _simulationData = new SimulationData();
        private SimulationInvoker _simulationInvoker = new SimulationInvoker();
        private Csv _csv;
        private bool isInitalize = false;
        public DefaultSimulation(Csv csv, BaseEventScriptableObject baseEventScriptableObject, GameObject simPrefab)
        {
            _csv = csv;
            _baseEventScriptableObject = baseEventScriptableObject;
            _simulationData.Prefab = simPrefab;
        }
   
        public bool Simulate(Csv csv, List<SimulationObject> simulationGameObjects)
        {
            if (!isInitalize)
            {
                _csv = csv;
                Initialize(simulationGameObjects);
                isInitalize = false;
                return true;
            }
            return true;
        }

        private bool Initialize(List<SimulationObject> simulationGameObjects)
        {
            _simulationData.AllCurrentObjects = simulationGameObjects;
            int count = _simulationData.AllCurrentObjects.Count;
            double[] simulatedObjectStates = new double[count];
            for (int i = 0; i < count; i++)
            {
                simulatedObjectStates[i] = _simulationData.AllCurrentObjects[i].Node.CurrentState;
            }

            foreach (var simulatedObject in simulationGameObjects)
            {
                simulatedObject.Node.PredictionModel =
                    new PartialLeastSquaresPredictionModel(simulatedObject.Node, _csv);
            }
            _simulationData.CurrentStates = simulatedObjectStates;
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new ChangeColorBasedOnStatesCommand());
            bool hasExecuted = ExecuteCommand(commands, null);
            _simulationInvoker.RemoveRecentCommand(); //The initial command does is removed as the user
            //Should not be able to undo to the baseline state
            return hasExecuted;
        }

       
        public bool ExecuteCommand(List<ICommand> commands, SimulationObject simulationObject)
        {
            _simulationData.CurrentInteractedObject = simulationObject;
            SimulationData data = _simulationInvoker.ExecuteCommand(commands, ref _simulationData);
            return _simulationData.Equals(data);
            
        }
        public bool UndoCommand()
        {
            SimulationData data = _simulationInvoker.UndoCommands(ref _simulationData);
            return _simulationData.Equals(data);
        }
        public void SetAsCurrentSimulator()
        {
            _baseEventScriptableObject.OnEventRaised(this);
        }
    }
}
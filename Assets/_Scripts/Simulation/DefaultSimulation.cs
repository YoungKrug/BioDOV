using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using _Scripts.Commands;
using _Scripts.CSVData;
using _Scripts.Documentation;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation.SimulationSettings;
using _Scripts.Statistics;
using UnityEngine;

namespace _Scripts.Simulation
{
    public class DefaultSimulation : ISimulator // Invoker Class
    {
        private readonly string _testPath = "C:/Users/gregj/Documents/test.txt";
        private readonly BaseEventScriptableObject _baseEventScriptableObject;
        private readonly SimulationInvoker _simulationInvoker = new SimulationInvoker();
        public SimulationConfig _config;
        private bool _isInitialize = false;
        public DefaultSimulation(SimulationConfig config)
        {
            _config = config;
            _baseEventScriptableObject = config.BaseEventScriptableObject;
        }


        public SimulationConfig Config => _config;

        public bool Simulate(SimulationConfig config)
        {
            if (!_isInitialize)
            {
                _config.CsvData = config.CsvData;
                Initialize(config.Data.AllCurrentObjects);
                _isInitialize = false;
                return true;
            }

            return false;
        }

        private bool Initialize(List<SimulationObject> simulationGameObjects)
        {
            _config.DocWriter = new DocumentationWriter();
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
            _simulationInvoker.RemoveRecentCommand(); 
            return true;
        }

       
        public bool ExecuteCommand(List<ICommand> commands, SimulationObject simulationObject)
        {
            _config.Data.CurrentInteractedObject = simulationObject;
            _simulationInvoker.ExecuteCommand(commands, ref _config.Data);
            ToDocumentation(commands);
            return true;

        }
        private void ToDocumentation(List<ICommand> commands)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var command in commands)
            {
                stringBuilder.Append(command.ToString());
                stringBuilder.Append("\n");
            }
            Debug.Log(stringBuilder.ToString());
            _config.DocWriter?.AddData(stringBuilder.ToString());
            
        }
        public bool UndoCommand()
        {
            bool returnedVal = _simulationInvoker.UndoCommands(ref _config.Data);
            _config.DocWriter?.UndoLastData();
            return returnedVal;
        }
        public void SetAsCurrentSimulator()
        {
            _baseEventScriptableObject.OnEventRaised(this);
        }

        public bool Reset()
        {
            //TODO Fix commands that do not undo all the way
            _simulationInvoker.UndoCommands(ref _config.Data);
            _isInitialize = false;
            _config.Data.Reset();
            _config.nextLevelScriptableObject.OnEventRaised(this);
            return false;
        }

        public bool FinishSimulation()
        {
            FileWriter writer = new FileWriter();
            return writer.WriteToFile(_testPath, _config.DocWriter.ToString());
        }
    }
}
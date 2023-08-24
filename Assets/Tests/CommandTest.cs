using System.Collections.Generic;
using System.IO;
using _Scripts.Commands;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using _Scripts.Statistics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Tests
{
    [TestFixture]
    public class CommandTest
    {
        private SimulationController _controller;
        private SimulationObject _simulationObject;
        private SimulationConfig _config;
        [SetUp]
        public void Setup()
        {
            FileInserter inserter =
                new FileInserter(Path.Combine(Application.dataPath, "SimulationFiles/DataExtra.csv"));
            Csv csv = inserter.ReadData();
            _controller = new SimulationController();
            _simulationObject = Resources.Load<SimulationObject>("Testing/Node_Test");
            _simulationObject.Node = new CsvNode
            {
                Name = "",
                States = csv.Data[0].States,
                CurrentState = 0,
            };
            _simulationObject.Node.PredictionModel = new PartialLeastSquaresPredictionModel(csv.Data[0],
                csv);
            _config = new SimulationConfig
            {
                Prefab = _simulationObject,
                CsvData = inserter.ReadData(),
                Data = new SimulationData
                {
                    AllCurrentObjects = new List<SimulationObject>{_simulationObject},
                    CurrentStates = new double[]{0,1},
                    Prefab = _simulationObject.gameObject
                }
            };
            _config.CurrentSimulation = new DefaultSimulation(_config);
        }
        
        [Test]
        [TestCase("PredictGeneStatesCommand")]
        [TestCase("ExtrudeObjectCommand")]
        [TestCase("ChangeColorBasedOnStatesCommand")]
        [TestCase("ClickDetectionCommand")]
        public void TestCommands(string commandString)
        {
            ICommand command = new ClickDetectionCommand();
            switch (commandString)
            {
                case "PredictGeneStatesCommand" :
                    command = new PredictGeneStatesCommand();
                    break;
                case "ExtrudeObjectCommand" :
                    command = new ExtrudeObjectCommand();
                    break;
                case "ChangeColorBasedOnStatesCommand" :
                    command = new ChangeColorBasedOnStatesCommand();
                    break;
                case "ClickDetectionCommand" :
                    command = new ClickDetectionCommand();
                    break;
            }
            bool isExecuteCommand = _config.CurrentSimulation.ExecuteCommand(new List<ICommand> { command }, 
                _simulationObject);
            Assert.IsTrue(isExecuteCommand);
        }
        [Test]
        [TestCase("PredictGeneStatesCommand")]
        [TestCase("ExtrudeObjectCommand")]
        [TestCase("ChangeColorBasedOnStatesCommand")]
        [TestCase("ClickDetectionCommand")]
        public void UndoTestCommands(string commandString)
        {
            ICommand command = new ClickDetectionCommand();
            switch (commandString)
            {
                case "PredictGeneStatesCommand" :
                    command = new PredictGeneStatesCommand();
                    break;
                case "ExtrudeObjectCommand" :
                    command = new ExtrudeObjectCommand();
                    break;
                case "ChangeColorBasedOnStatesCommand" :
                    command = new ChangeColorBasedOnStatesCommand();
                    break;
                case "ClickDetectionCommand" :
                    command = new ClickDetectionCommand();
                    break;
            }
            _config.CurrentSimulation.ExecuteCommand(new List<ICommand> { command }, 
                _simulationObject);
            bool hasUndoCommand = _config.CurrentSimulation.UndoCommand();
            Assert.IsTrue(hasUndoCommand);
        }
    }
}
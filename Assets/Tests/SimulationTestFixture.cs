using System.Collections.Generic;
using System.IO;
using _Scripts.Commands;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class SimulationTestFixture
    {
        private SimulationConfig _config;
        private SimulationObject _simulationObject;
        [SetUp]
        public void Setup()
        {
            FileInserter inserter =
                new FileInserter(Path.Combine(Application.dataPath, "SimulationFiles/DataExtra.csv"));
            Csv csv = inserter.ReadData();
            _simulationObject = Resources.Load<SimulationObject>("Testing/Node_Test");
            _simulationObject.Node = csv.Data[5];
            _config = new SimulationConfig
            {
                Prefab = _simulationObject,
                CsvData = csv,
                Data = new SimulationData
                {
                    AllCurrentObjects = new List<SimulationObject>{_simulationObject},
                    CurrentStates = new double[]{0,1},
                    Prefab = _simulationObject.gameObject
                }
            };
        }

        [Test]
        [TestCase("Default")]
        [TestCase("Custom")]
        public void Simulation(string simulation)
        {
            ISimulator simulator = null;
            switch (simulation)
            {
                case "Default":
                    simulator = new DefaultSimulation(_config);
                    break;
                case "Custom":
                    simulator = new CustomSimulation();
                    break;
            }
            Assert.IsTrue(simulator.Simulate(_config));
        }
        
        [Test]
        [TestCase("Default")]
        [TestCase("Custom")]
        public void Simulation_ExecuteCommand(string simulation)
        {
            ISimulator simulator = null;
            switch (simulation)
            {
                case "Default":
                    simulator = new DefaultSimulation(_config);
                    break;
                case "Custom":
                    simulator = new CustomSimulation();
                    break;
            }

            List<ICommand> commands = new List<ICommand> { new ClickDetectionCommand() };
            Assert.IsTrue(simulator.ExecuteCommand(commands, _simulationObject));
        }
        
        [Test]
        [TestCase("Default")]
        [TestCase("Custom")]
        public void Simulation_UndoCommand(string simulation)
        {
            ISimulator simulator = null;
            bool hasUndo;
            bool cantUndo;
            List<ICommand> commands = new List<ICommand> { new ClickDetectionCommand() };
            switch (simulation)
            {
                case "Default":
                    simulator = new DefaultSimulation(_config);
                    simulator.ExecuteCommand(commands, _simulationObject);
                    hasUndo = simulator.UndoCommand();
                    cantUndo = simulator.UndoCommand();
                    Assert.IsTrue(hasUndo);
                    Assert.IsFalse(cantUndo);
                    break;
                case "Custom":
                    simulator = new CustomSimulation();
                    hasUndo = simulator.UndoCommand();
                    Assert.IsTrue(hasUndo);
                    break;
            }
          
        }
    }
}
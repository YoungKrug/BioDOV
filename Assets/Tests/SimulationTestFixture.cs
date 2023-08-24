using System.Collections.Generic;
using _Scripts.CSVData;
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
        private DefaultSimulation _simulation;
        private SimulationConfig _config;
        [SetUp]
        public void SetUp()
        {
            _config = new SimulationConfig();
            SimulationObject simulationObject = Resources.Load<SimulationObject>("Testing/Node_Test");
            simulationObject.Node = new CsvNode();
            _config.Prefab = simulationObject;
            _config.Data.AllCurrentObjects = new List<SimulationObject>();
            _config.Data.AllCurrentObjects.Add(simulationObject);
            _simulation = new DefaultSimulation(_config);
        }

        [Test]
        public void Simulation()
        {
            //Assert.IsTrue(_simulation.Simulate(_config));
        }
    }
}
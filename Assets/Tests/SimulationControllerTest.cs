using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class SimulationControllerTest
    {
        private SimulationController _controller;
        private SimulationObject _simulationObject;
        private SimulationConfig _config;
        [SetUp]
        public void Setup()
        {
            _controller = new SimulationController();
            _simulationObject = Resources.Load<SimulationObject>("Testing/Node_Test");
            _config = new SimulationConfig
            {
                Prefab = _simulationObject,
                CsvData = new Csv(),
                Data = new SimulationData
                {
                    AllCurrentObjects = new List<SimulationObject>(),
                    CurrentStates = new double[]{}
                }
            };
            _config.CurrentSimulation = new DefaultSimulation(_config);
        }
        [Test]
        public void Initialization()
        {
            _config = _controller.Initialize(_config);
            Assert.IsNotNull(_config.Data.AllCurrentObjects);
        }

        [Test]
        public void Test_Prediction()
        {
            bool hasPredicted = _controller.Predict(_config);
            Assert.IsTrue(hasPredicted);
        }
    }
}
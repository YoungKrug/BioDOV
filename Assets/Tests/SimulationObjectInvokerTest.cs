using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class SimulationObjectResponderTest
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
                    CurrentStates = new double[]{0,1}
                }
            };
            _simulationObject.Node = new CsvNode();
            _config.CurrentSimulation = new DefaultSimulation(_config);
        }
        [Test]
        public void TestObjectResponderOnClick()
        {
            SimulationObjectResponder responder = new SimulationObjectResponder(new DefaultSimulation(_config),
                _simulationObject);
            bool hasClicked = responder.OnClick();
            Assert.IsTrue(hasClicked);
        }
    }
}
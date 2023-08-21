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
            List<SimulationObject> simulationObjects = new List<SimulationObject>();
           // SimulationObjectResponder responder = new SimulationObjectResponder(new DefaultSimulation(_config))
            //simulationObjects.Add();
        }

        [Test]
        public void Simulation()
        {
           // _simulation.
        }
    }
}
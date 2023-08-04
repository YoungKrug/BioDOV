using _Scripts.CSVData;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class SimulationTestFixture
    {
        private DefaultSimulation _simulation;

        [SetUp]
        public void SetUp()
        {
            _simulation = new DefaultSimulation(new Csv(), 
                null, new GameObject());
        }

        [Test]
        public void Simulation()
        {
           // _simulation.
        }
    }
}
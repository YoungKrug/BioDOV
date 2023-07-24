using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation.SimulationSettings;
using UnityEngine;

namespace _Scripts.Simulation
{
    public class CustomSimulation : ISimulator
    {
        [SerializeField] private BaseEventScriptableObject _baseEventScriptableObject;
        private SimulationData _simulationData;
        public void Simulate(Csv csv, List<SimulationObject> simulationObjects)
        {
            throw new System.NotImplementedException();
        }

        public void SetAsCurrentSimulator()
        {
            _baseEventScriptableObject.OnEventRaised(this);
        }

        public void InteractedWithObject(SimulationObject simulationObject)
        {
            throw new System.NotImplementedException();
        }

        public double[] StatesArray { get; }
    }
}
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation.SimulationSettings;
using UnityEngine;

namespace _Scripts.Simulation
{
    public class DefaultSimulation : ISimulator
    {
        [SerializeField] private BaseEventScriptableObject _baseEventScriptableObject;
        private SimulationData _simulationData;
        public void Simulate()
        {
            throw new System.NotImplementedException();
        }

        public void SetAsCurrentSimulator()
        {
            _baseEventScriptableObject.OnEventRaised(this);
        }
    }
}
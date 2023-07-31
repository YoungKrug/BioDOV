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

        public bool Simulate(Csv csv, List<SimulationObject> simulationGameObjects)
        {
            throw new System.NotImplementedException();
        }

        public bool ExecuteCommand(List<ICommand> commands, SimulationObject simulationObject)
        {
            throw new System.NotImplementedException();
        }

        public bool UndoCommand()
        {
            throw new System.NotImplementedException();
        }
    }
}
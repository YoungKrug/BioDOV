using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using UnityEngine;

namespace _Scripts.Interface
{
    public interface ISimulator
    {
        public bool Simulate(Csv csv, List<SimulationObject> simulationGameObjects);
        public bool ExecuteCommand(List<ICommand> commands, SimulationObject simulationObject);
        public bool UndoCommand();
    }
}
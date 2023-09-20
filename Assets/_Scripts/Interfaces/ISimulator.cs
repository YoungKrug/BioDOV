using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using UnityEngine;

namespace _Scripts.Interface
{
    public interface ISimulator
    {
        public SimulationConfig Config { get; }
        public bool Simulate(SimulationConfig config);
        public bool ExecuteCommand(List<ICommand> commands, SimulationObject simulationObject);
        public bool UndoCommand();
        public bool Reset();
        public bool FinishSimulation();
    }
}
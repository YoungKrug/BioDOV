using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using UnityEngine;

namespace _Scripts.Interface
{
    public interface ISimulator
    {
        public void Simulate(Csv csv, List<SimulationObject> simulationGameObjects);
        public void SetAsCurrentSimulator();
        public void InteractedWithObject(SimulationObject simulationObject);
    }
}
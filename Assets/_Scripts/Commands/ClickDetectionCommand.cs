using System.Collections.Generic;
using _Scripts.Interface;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using Accord.Diagnostics;
using Debug = UnityEngine.Debug;

namespace _Scripts.Commands
{
    public class ClickDetectionCommand : ICommand // Concrete Commands
    {

        public SimulationData Data { get; private set; }
        private KeyValuePair<int, double> prevState = new KeyValuePair<int, double>();
        public bool Execute()
        {
            SimulationData currentData = Data;
            double[] states = Data.CurrentStates;
            List<SimulationObject> allObjects = Data.AllCurrentObjects;
            SimulationObject currentObject = Data.CurrentInteractedObject;
            int index = allObjects.IndexOf(currentObject);
            if (index == -1)
                return false;
            double oldVal = states[index];
            prevState = new KeyValuePair<int, double>(index, oldVal);
            double newVal = (oldVal + 1) % 3;
            states[index] = newVal;
            currentObject.Node.CurrentState = newVal;
            currentData.CurrentStates = states;
            Data = currentData;
            return true;
        }
        
        public bool Undo()
        {
            SimulationData oldData = Data;
            oldData.CurrentStates[prevState.Key] = prevState.Value;
            Data.AllCurrentObjects[prevState.Key].Node.CurrentState = prevState.Value;
            Data = oldData;
            return true;
        }

        public void Set(SimulationData data)
        {
            Data = data;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
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
        private KeyValuePair<int, double> _prevState = new KeyValuePair<int, double>();
        private string _docString;
        public bool Execute()
        {
            _docString = "";
            SimulationData currentData = Data;
            double[] states = Data.CurrentStates;
            List<SimulationObject> allObjects = Data.AllCurrentObjects;
            SimulationObject currentObject = Data.CurrentInteractedObject;
            int index = allObjects.IndexOf(currentObject);
            if (index == -1)
                return false;
            double oldVal = states[index];
            _prevState = new KeyValuePair<int, double>(index, oldVal);
            double newVal = (oldVal + 1) % 3;
            states[index] = newVal;
            currentObject.Node.CurrentState = newVal;
            currentData.CurrentStates = states;
            Data = currentData;
            _docString = $"{currentObject.Node.Name} was interacted with," +
                              $" and stated changed from {oldVal}->{newVal}";
            return true;
        }
        public bool Undo()
        {
            SimulationData oldData = Data;
            oldData.CurrentStates[_prevState.Key] = _prevState.Value;
            Data.AllCurrentObjects[_prevState.Key].Node.CurrentState = _prevState.Value;
            Data = oldData;
            return true;
        }

        public void Set(SimulationData data)
        {
            Data = data;
        }
        public override string ToString()
        {
            return _docString;
        }
    }
}
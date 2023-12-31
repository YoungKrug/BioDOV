﻿
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Scripts.Interface;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Commands
{
    public class PredictGeneStatesCommand : ICommand // Concrete Commands
    {
        private Dictionary<int, double> _previousStates = new Dictionary<int, double>();
        public SimulationData Data { get; private set; }
        public readonly StringBuilder _docString = new StringBuilder();
        public bool Execute()
        {
            _docString.Clear();
            bool canPredict = Data.AllCurrentObjects.Any(x => x.hasInteracted);
            if (!canPredict)
            {
                Debug.Log("No changes, no need to predict");
                return false;
            }

            Debug.Log("Predicting");
            foreach (var simulatedObject in Data.AllCurrentObjects) //TODO Fix gene state value error
            {
                if (simulatedObject.hasInteracted)
                {
                    simulatedObject.hasInteracted = false;
                    continue;
                }
               
                double newState = simulatedObject.Node.PredictionModel.Predict(Data.CurrentStates);
                int index = Data.AllCurrentObjects.IndexOf(simulatedObject);
                double prevState = Data.AllCurrentObjects[index].Node.CurrentState;
                simulatedObject.Node.CurrentState = newState;
                Data.CurrentStates[index] = newState;
                _previousStates.Add(index, prevState);
                _docString.Append($"{simulatedObject.Node.Name} Prediction, OldState: " +
                                  $"{prevState}, NewState: {newState}");
            }
            return true;
        }

        public bool Undo()
        {
            for (int i = 0; i < _previousStates.Count; i++)
            {
                int index = _previousStates.ElementAt(i).Key;
                double val = _previousStates.ElementAt(i).Value;
                SimulationObject obj = Data.AllCurrentObjects[index];
                Data.CurrentStates[index] = val;
                obj.Node.CurrentState = val;
                Data.AllCurrentObjects[index] = obj;
            }
            Debug.Log("Undoing Predictions");
            return true;
        }

        public void Set(SimulationData data)
        {
            Data = data;
        }
        public override string ToString()
        {
            return _docString.ToString();
        }
    }
}
using System.Collections.Generic;
using _Scripts.Interface;
using _Scripts.Simulation.SimulationSettings;
using UnityEngine;

namespace _Scripts.Commands
{
    public class ExtrudeObjectCommand : ICommand
    {
        public SimulationData Data { get; private set; }
        private Dictionary<int, Vector3> _previousStates = new Dictionary<int, Vector3>();
        public void Execute()
        {
            foreach (var simulationObject in Data.AllCurrentObjects)
            {
                float scaler = (float)simulationObject.Node.PredictionModel.UnRoundedPredictionValue;
                int index = Data.AllCurrentObjects.IndexOf(simulationObject);
                Vector3 currentScaler = simulationObject.gameObject.transform.localScale;
                Vector3 scaleVector = Data.Prefab.transform.localScale;
                _previousStates.Add(index, currentScaler);
                scaleVector += new Vector3(0, scaler);
                simulationObject.gameObject.transform.localScale = scaleVector;
            }
        }

        public void Undo()
        {
            foreach (var previousState in _previousStates)
            {
                int index = previousState.Key;
                Vector3 previousVector = previousState.Value;
                GameObject simulationObject = Data.AllCurrentObjects[index].gameObject;
                simulationObject.transform.localScale = previousVector;
            }
        }

        public void Set(SimulationData data)
        {
            Data = data;
        }
    }
}
using System.Collections.Generic;
using System.Text;
using _Scripts.Interface;
using _Scripts.Simulation.SimulationSettings;
using UnityEngine;

namespace _Scripts.Commands
{
    public class ExtrudeObjectCommand : ICommand
    {
        public SimulationData Data { get; private set; }
        private readonly Dictionary<int, Vector3> _previousStates = new Dictionary<int, Vector3>();
        private readonly StringBuilder _docString = new StringBuilder();
        public bool Execute()
        {
            _docString.Clear();
            foreach (var simulationObject in Data.AllCurrentObjects)
            {
                float scaler = (float)simulationObject.Node.PredictionModel.UnRoundedPredictionValue;
                int index = Data.AllCurrentObjects.IndexOf(simulationObject);
                Vector3 currentScaler = simulationObject.gameObject.transform.localScale;
                Vector3 scaleVector = Data.Prefab.transform.localScale;
                int negativeScaler = simulationObject.Node.CurrentState > 1 ? 1 : -1;
                Vector3 newPosition = new Vector3(simulationObject.gameObject.transform.position.x, 
                    scaler * negativeScaler);
                _previousStates.Add(index, currentScaler);
                scaleVector += new Vector3(0, scaler);
                var gameObject = simulationObject.gameObject;
                gameObject.transform.localScale = scaleVector;
                gameObject.transform.position = newPosition;
                _docString.Append($"{simulationObject.Node.Name} was extruded from" +
                                  $" {currentScaler} -> {scaleVector}\n");
            }
            return true;
        }

        public bool Undo() //TODO Fix extrude undo functionality
        {
            foreach (var previousState in _previousStates)
            {
                int index = previousState.Key;
                Vector3 previousVector = previousState.Value;
                GameObject simulationObject = Data.AllCurrentObjects[index].gameObject;
                simulationObject.transform.localScale = previousVector;
            }
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
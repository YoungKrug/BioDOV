using System.Collections.Generic;
using System.Linq;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation.SimulationSettings;
using _Scripts.Statistics;
using UnityEngine;

namespace _Scripts.Simulation
{
    public class DefaultSimulation : ISimulator
    {
        public DefaultSimulation(Csv csv, BaseEventScriptableObject baseEventScriptableObject)
        {
            _csv = csv;
            _baseEventScriptableObject = baseEventScriptableObject;
        }
        [SerializeField] private BaseEventScriptableObject _baseEventScriptableObject;
        public double[] StatesArray => _statesArray;
        private double[] _statesArray;
        private Color _colorGreen = Color.green;
        private Color _colorYellow = Color.yellow;
        private SimulationData _simulationData;
        public List<SimulationObject> SimulatedGameObjects = new List<SimulationObject>();
        private Csv _csv;
        public void Simulate(Csv csv, List<SimulationObject> simulationGameObjects)
        {
            SimulatedGameObjects = simulationGameObjects;
            int count = SimulatedGameObjects.Count;
            double[] simulatedObjectStates = new double[count];
            for (int i = 0; i < count; i++)
            {
                simulatedObjectStates[i] = SimulatedGameObjects[i].Node.CurrentState;
            }

            foreach (var simulatedObject in simulationGameObjects)
            {
                simulatedObject.Node.PredictionModel =
                    new PartialLeastSquaresPredictionModel(simulatedObject.Node, csv);
            }
            _statesArray = simulatedObjectStates;
            _csv = csv;
            ModifyColorBasdOnState();
        }

        private void ModifyColorBasdOnState()
        {
            foreach (SimulationObject obj in SimulatedGameObjects)
            {
                Color color = obj.Material.color;
                double cState = obj.Node.CurrentState;
                color =  cState > 1 ? _colorGreen : _colorYellow;
                if (cState == 0)
                    color = Color.white;
                obj.Material.color = color;
            }
        }
        public void InteractedWithObject(SimulationObject simulationObject)
        {
            int index = SimulatedGameObjects.IndexOf(simulationObject);
            double val = _statesArray[index];
            double newVal = (val + 1) % 3;
            _statesArray[index] = newVal;
            simulationObject.Node.CurrentState = newVal;
            foreach (var simulatedObject in SimulatedGameObjects)
            {
                if(simulatedObject.Node.Name == simulationObject.Node.Name)
                    continue;
                double newState = simulatedObject.Node.PredictionModel.Predict(_statesArray);
                simulatedObject.Node.CurrentState = newState;
            }
            ModifyColorBasdOnState();
        }

       

        public void SetAsCurrentSimulator()
        {
            _baseEventScriptableObject.OnEventRaised(this);
        }
    }
}
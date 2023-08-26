using System.Collections.Generic;
using _Scripts.Interface;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using UnityEngine;

namespace _Scripts.Commands
{
    public class ChangeColorBasedOnStatesCommand : ICommand // Concrete Commands
    {
        public SimulationData Data { get; private set; }
        private Dictionary<SimulationObject, Color> _previousData = new Dictionary<SimulationObject, Color>();
        private readonly Color _highExpressionColor = Color.green;
        private readonly Color _lowExpressionColor = Color.red;
        private readonly Color _noExpression = Color.white;
        
        public bool Execute()
        {
            foreach (SimulationObject obj in Data.AllCurrentObjects)
            {
                _previousData.Add(obj, obj.Material.color);
                double cState = obj.Node.CurrentState;
                Color color =  cState > 1 ? _highExpressionColor : _lowExpressionColor;
                if (cState == 0)
                    color = _noExpression;
                obj.Material.color = color;
            }

            return true;
        }

        public bool Undo()
        {
            Debug.Log("Redoing Color Changing Command");
            foreach (var val in _previousData)
            {
                SimulationObject simulationObject = val.Key;
                Color color = val.Value;
                simulationObject.Material.color = color;
            }
            return true;
        }

        public void Set(SimulationData data)
        {
            Data = data;
        }
    }
}
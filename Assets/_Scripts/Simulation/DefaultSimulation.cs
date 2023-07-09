using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation.SimulationSettings;
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
        private Color _colorGreen = Color.green;
        private Color _colorRed = Color.red;
        private SimulationData _simulationData;
        public List<SimulationObject> SimulatedGameObjects = new List<SimulationObject>();
        private Csv _csv;
        public void Simulate(Csv csv, List<SimulationObject> simulationGameObjects)
        {
            SimulatedGameObjects = simulationGameObjects;
            _csv = csv;
            // foreach (var simulationObject in SimulatedGameObjects)
            // {
            //     
            // }
        }

        public void InteractedWithObject(SimulationObject simulationObject)
        {
            int indexOf = _csv.Data.IndexOf(simulationObject.Node);
            List<CasualtyInformation> list = _csv.Data[indexOf].CasualtyInformationList;
            foreach (var causalityInfo in list)
            {
                float cPercent = (float)causalityInfo.CasualtyPercent;
                foreach (var node in SimulatedGameObjects)
                {
                    if (node.Node.Name == causalityInfo.Name)
                    {
                        Color color = node.Material.color;
                        color = cPercent > 0 ? _colorGreen : _colorRed;
                        if(cPercent == 0)
                            color = Color.white;
                        color.a *= Mathf.Abs(cPercent);
                        node.Material.color = color;
                        Debug.Log($"{node.Node.Name}: {cPercent}");
                    }
                }
            }
            //Color has to be related to the percentage they are related
            //So Alpha = Abs(%Relation) for negative red, positive green  
        }

        public void SetAsCurrentSimulator()
        {
            _baseEventScriptableObject.OnEventRaised(this);
        }
    }
}
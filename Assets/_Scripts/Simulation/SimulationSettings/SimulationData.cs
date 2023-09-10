using System.Collections.Generic;
using _Scripts.Interface;
using UnityEngine;

namespace _Scripts.Simulation.SimulationSettings
{
    public struct SimulationData
    {
        public SimulationCustomizationRules SimulationRules;
        public List<SimulationObject> AllCurrentObjects;
        public double[] CurrentStates;
        public SimulationObject CurrentInteractedObject;
        public GameObject Prefab;
        public void Reset()
        {
            for (int i = 0; i < AllCurrentObjects.Count; i++)
            {
                Object.Destroy(AllCurrentObjects[i].gameObject);
            }
            Prefab = null;
            CurrentInteractedObject = null;
        }
    }
}
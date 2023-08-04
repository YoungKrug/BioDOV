using _Scripts.CSVData;
using _Scripts.Interface;
using UnityEngine;

namespace _Scripts.Simulation
{
    public class SimulationController
    {
        
        public bool Initialize(SimulationObject prefab, Csv csvData, ISimulator currentSimulation)
        {
            float dist = 2f;
            foreach (var csv in csvData.Data)
            {
                GameObject simulationObject = GameObject.Instantiate(prefab.gameObject);
                Transform transform = simulationObject.transform;
                transform.position = new Vector3(dist * .5f, 0);
                simulationObject.GetComponent<SimulationObject>().Node = csv;
                simulationObject.GetComponent<SimulationObject>().Simulator = currentSimulation;
                simulationObject.GetComponent<SimulationObject>().Material = simulationObject.GetComponent<Renderer>().material;
                dist += 2f;
                _simulationObjects.Add(simulationObject.GetComponent<SimulationObject>());
            }
            button.onClick.AddListener(() => {Predict();});
            return true;
        }
    }
}
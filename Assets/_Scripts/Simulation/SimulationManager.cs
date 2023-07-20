using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.Simulation
{
    public class SimulationManager: MonoBehaviour, IEventReactor
    {
        public ISimulator CurrentSimulation;
        public BaseEventScriptableObject BaseEventScriptableObject;
        public Csv CsvData = new Csv();
        public BaseEventScriptableObject ScriptableObject => BaseEventScriptableObject;
        [SerializeField] private SimulationObject _prefab;
        private List<SimulationObject> _simulationObjects = new List<SimulationObject>();
        private bool _init = false;
        
        public void OnEventSimulate()
        {
            if (!_init)
            {
                Initialize();
                _init = true;
            }
            CurrentSimulation.Simulate(CsvData, _simulationObjects);
            //CurrentSimulation.Simulate();
        }

        private void Initialize()
        {
            float dist = 2f;
            foreach (var csv in CsvData.Data)
            {
                GameObject simulationObject = GameObject.Instantiate(_prefab.gameObject);
                Transform transform = simulationObject.transform;
                transform.position = new Vector3(csv.Coords.x * 4, csv.Coords.y * 4);
                simulationObject.GetComponent<SimulationObject>().Node = csv;
                simulationObject.GetComponent<SimulationObject>().Simulator = CurrentSimulation;
                dist += 2f;
                _simulationObjects.Add(simulationObject.GetComponent<SimulationObject>());
            }
        }
        public void Execute(object eventObject)
        {
            CurrentSimulation = (ISimulator)eventObject;
            OnEventSimulate();
        }
    }
}
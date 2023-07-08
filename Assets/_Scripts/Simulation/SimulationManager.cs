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
        private List<GameObject> _simulationObjects;
        private bool _init = false;
        
        public void OnEventSimulate()
        {
            if (!_init)
            {
                Initialize();
                _init = true;
            }
            //CurrentSimulation.Simulate();
        }

        private void Initialize()
        {
            float dist = 2f;
            foreach (var csv in CsvData.Data)
            {
                GameObject simulationObject = GameObject.Instantiate(_prefab.gameObject);
                Transform transform = simulationObject.transform;
                transform.position = new Vector3(transform.position.x + dist, transform.position.y);
                simulationObject.GetComponent<SimulationObject>().Node = csv;
                dist += 2f;
            }
        }
        public void Execute(object eventObject)
        {
            CurrentSimulation = (ISimulator)eventObject;
            OnEventSimulate();
        }
    }
}
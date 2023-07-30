using System.Collections.Generic;
using _Scripts.Commands;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Simulation
{
    public class SimulationManager: MonoBehaviour, IEventReactor
    {
        public ISimulator CurrentSimulation;
        public BaseEventScriptableObject BaseEventScriptableObject;
        public Csv CsvData = new Csv();
        public BaseEventScriptableObject ScriptableObject => BaseEventScriptableObject;
        public SimulationObject prefab;
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

        private bool Initialize()
        {
            float dist = 2f;
            foreach (var csv in CsvData.Data)
            {
                GameObject simulationObject = GameObject.Instantiate(prefab.gameObject);
                Transform transform = simulationObject.transform;
                transform.position = new Vector3(dist * .5f, 0);
                simulationObject.GetComponent<SimulationObject>().Node = csv;
                simulationObject.GetComponent<SimulationObject>().Simulator = CurrentSimulation;
                simulationObject.GetComponent<SimulationObject>().Material = simulationObject.GetComponent<Renderer>().material;
                dist += 2f;
                _simulationObjects.Add(simulationObject.GetComponent<SimulationObject>());
            }

            return true;
        }

        public bool Predict()
        {
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new PredictGeneStatesCommand());
            commands.Add(new ChangeColorBasedOnStatesCommand());
            commands.Add(new ExtrudeObjectCommand());
            CurrentSimulation.ExecuteCommand(commands, null);
            return true;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                CurrentSimulation.UndoCommand();
            }
        }

        public void Execute(object eventObject)
        {
            CurrentSimulation = (ISimulator)eventObject;
            OnEventSimulate();
        }
    }
}
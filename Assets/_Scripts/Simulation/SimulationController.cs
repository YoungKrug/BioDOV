using System.Collections.Generic;
using _Scripts.Commands;
using _Scripts.Interface;
using _Scripts.Simulation.SimulationSettings;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Simulation
{
    public class SimulationController
    {
        public SimulationConfig Initialize(SimulationConfig config)
        {
            float dist = 2f;
            config.Data = new SimulationData();
            ISimulator simulator = config.CurrentSimulation;
            List<SimulationObject> currentObjects = new List<SimulationObject>();
            GameObject prefab = config.Prefab.gameObject;
            foreach (var csv in config.CsvData.Data)
            {
                GameObject simulationObject = Object.Instantiate(prefab);
                Transform transform = simulationObject.transform;
                transform.position = new Vector3(dist * .5f, 0);
                simulationObject.GetComponent<SimulationObject>().Node = csv;
                simulationObject.GetComponent<SimulationObject>().Simulator = simulator;
                simulationObject.GetComponent<SimulationObject>().Material = simulationObject.GetComponent<Renderer>().material;
                dist += 2f;
                currentObjects.Add(simulationObject.GetComponent<SimulationObject>());
            }
            config.Data.AllCurrentObjects = currentObjects;
            return config;
        }
        public bool Predict(SimulationConfig config)
        {
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new PredictGeneStatesCommand());
            commands.Add(new ChangeColorBasedOnStatesCommand());
            commands.Add(new ExtrudeObjectCommand());
            config.CurrentSimulation.ExecuteCommand(commands, null);
            return true;
        }
    }
}
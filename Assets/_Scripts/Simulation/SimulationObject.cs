using System;
using System.Collections.Generic;
using _Scripts.Commands;
using _Scripts.CSVData;
using _Scripts.Interface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Simulation
{
    public class SimulationObject: MonoBehaviour, IPointerClickHandler //
    {
        public CsvNode Node;
        public Material Material;
        public ISimulator Simulator;
        public bool hasInteracted;

        public void Start()
        {
            Material = GetComponent<Renderer>().material;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"You Clicked the object {Node.Name}");
            hasInteracted = true;
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new ClickDetectionCommand());
            commands.Add(new ChangeColorBasedOnStatesCommand());
            Simulator.ExecuteCommand(commands, this);
            //This is the node that will be used to modify the simulation, so
        }
    }
}
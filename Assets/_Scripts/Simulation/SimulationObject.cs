using System;
using _Scripts.CSVData;
using _Scripts.Interface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Simulation
{
    public class SimulationObject: MonoBehaviour, IPointerClickHandler
    {
        public CsvNode Node;
        public Material Material;
        public ISimulator Simulator;

        public void Start()
        {
            Material = GetComponent<Renderer>().material;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"You Clicked the object {Node.Name}");
            Simulator.InteractedWithObject(this);
        }
    }
}
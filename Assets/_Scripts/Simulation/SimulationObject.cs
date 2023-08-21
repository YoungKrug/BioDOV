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
        public bool hasInteracted;
        private SimulationObjectResponder _responder;
        public void Init(ISimulator simulator)
        {
            _responder = new SimulationObjectResponder(simulator, this);
            Material = GetComponent<Renderer>().material;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            hasInteracted = true;
            _responder.OnClick();
        }
    }
}
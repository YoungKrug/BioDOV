using System;
using System.Collections.Generic;
using _Scripts.Commands;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = Accord.Diagnostics.Debug;

namespace _Scripts.Simulation
{
    public class SimulationObject: MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler//
    {
        public CsvNode Node;
        public Material Material;
        public bool hasInteracted;
        public GeneDisplayInformation displayInformation;
        private SimulationObjectResponder _responder;
        private GeneDataDisplay _geneDisplay;
        public void Init(ISimulator simulator)
        {
            _responder = new SimulationObjectResponder(simulator, this);
            Material = GetComponent<Renderer>().material;
            _geneDisplay = new GeneDataDisplay(displayInformation);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            hasInteracted = true;
            _responder.OnClick();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            _geneDisplay.OnPointerEnter(eventData, transform.position, Node);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _geneDisplay.OnPointerExit(eventData);
        }
    }
}
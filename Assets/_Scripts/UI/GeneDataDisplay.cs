using _Scripts.CSVData;
using Accord.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
namespace _Scripts.UI
{
    public class GeneDataDisplay
    { 
        private readonly GeneDisplayInformation _display;
        private readonly Canvas _canvas;
        private GeneDisplayInformation _createdDisplay;
        public GeneDataDisplay(GeneDisplayInformation display)
        {
            _display = display;
            _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }
        public void OnPointerEnter(PointerEventData eventData, Vector3 objectPosition, CsvNode node)
        {
           
            if (!_createdDisplay)
            {
                _createdDisplay = Object.Instantiate(_display, _canvas.transform, true);
            }
            RectTransform transform = _createdDisplay.GetComponent<RectTransform>();
            transform.gameObject.transform.position = eventData.position;
            _createdDisplay.gameObject.SetActive(true);
            _createdDisplay.text.text = $"Gene: {node.Name}\n Current State: {node.CurrentState}";
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _createdDisplay.gameObject.SetActive(false);
        }
    }
}
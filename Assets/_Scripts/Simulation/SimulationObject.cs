using _Scripts.CSVData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Simulation
{
    public class SimulationObject: MonoBehaviour, IPointerClickHandler
    {
        public CsvNodes Node;
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"You Clicked the object {Node.Name}");
        }
    }
}
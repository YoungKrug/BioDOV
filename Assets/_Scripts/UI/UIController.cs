using _Scripts.CSVData;
using _Scripts.Simulation;
using UnityEngine;

namespace _Scripts.UI
{
    public class UIController : MonoBehaviour
    {
        public SimulationManager Manager;
        public DataInserter Inserter;
        public void CreateSimulation()
        {
            Manager.Config.CsvData = Inserter._csv;
            DefaultSimulation defaultSimulation = new DefaultSimulation(Manager.Config); // Zombie Code
            defaultSimulation.SetAsCurrentSimulator();
        }
    }
}
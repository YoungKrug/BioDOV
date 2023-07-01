using _Scripts.Interface;
using _Scripts.ScriptableObjects;

namespace _Scripts.Simulation
{
    public class SimulationManager: IEventReactor
    {
        public ISimulator CurrentSimulation;
        public BaseEventScriptableObject BaseEventScriptableObject;
        public BaseEventScriptableObject ScriptableObject => BaseEventScriptableObject;
        
        private void OnEventSimulate()
        {
            CurrentSimulation.Simulate();
        }
        public void Execute(object eventObject)
        {
            CurrentSimulation = (ISimulator)eventObject;
            OnEventSimulate();
        }
    }
}
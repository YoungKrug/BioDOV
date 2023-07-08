using _Scripts.Simulation.SimulationSettings;

namespace _Scripts.Interface
{
    public interface ISimulator
    {
        public void Simulate();
        public void SetAsCurrentSimulator();
    }
}
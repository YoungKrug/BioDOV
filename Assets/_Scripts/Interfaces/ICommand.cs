using _Scripts.Simulation.SimulationSettings;

namespace _Scripts.Interface
{
    public interface ICommand
    {
        public SimulationData Data { get; }
        public void Execute();
        public void Undo();
        public void Set(SimulationData data);
    }
}
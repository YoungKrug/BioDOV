using _Scripts.Simulation.SimulationSettings;

namespace _Scripts.Interface
{
    public interface ICommand
    {
        public SimulationData Data { get; }
        public bool Execute();
        public bool Undo();
        public void Set(SimulationData data);
    }
}
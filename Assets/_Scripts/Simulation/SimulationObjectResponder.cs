using System.Collections.Generic;
using _Scripts.Commands;
using _Scripts.Interface;

namespace _Scripts.Simulation
{
    public class SimulationObjectResponder
    {
        private readonly ISimulator _simulator;
        private readonly SimulationObject _simulationObject;

        public SimulationObjectResponder(ISimulator simulator, SimulationObject simulationObject)
        {
            _simulator = simulator;
            _simulationObject = simulationObject;
        }
        public bool OnClick()
        {
            List<ICommand> commands = new List<ICommand>();
            commands.Add(new ClickDetectionCommand());
            commands.Add(new ChangeColorBasedOnStatesCommand());
            bool? hasReturned = _simulator?.ExecuteCommand(commands, _simulationObject);
            return hasReturned.HasValue;
            //This is the node that will be used to modify the simulation, so
        }
    }
}
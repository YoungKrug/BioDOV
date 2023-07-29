using System.Collections.Generic;
using _Scripts.Interface;
using _Scripts.Simulation.SimulationSettings;

namespace _Scripts.Commands
{
    public class SimulationInvoker
    {
        private Stack<List<ICommand>> _commands = new Stack<List<ICommand>>();
        public void ExecuteCommand(List<ICommand> commands, ref SimulationData data)
        {
            foreach (var command in commands)
            {
                command.Set(data);
                command.Execute();
                data = command.Data;

            }
            _commands.Push(commands);
        }

        public void RemoveRecentCommand()
        {
            _commands?.Pop();
        }
        public void UndoCommands(ref SimulationData data)
        {
            if (_commands.Count <= 0)
                return;
            List<ICommand> commands = _commands.Pop();
            foreach (var command in commands)
            {
                command.Set(data);
                command.Undo();
                data = command.Data;
            }
        }
        public void UndoAllCommands(ref SimulationData data)
        {
            int count = _commands.Count;
            for (int i = 0; i < count; i++)
            {
                List<ICommand> commands = _commands.Pop();
                foreach (var command in commands)
                {
                    command.Set(data);
                    command.Undo();
                    data = command.Data;
                }
            }
        }
    }
}
using System.Collections.Generic;
using _Scripts.Interface;
using _Scripts.Simulation.SimulationSettings;

namespace _Scripts.Commands
{
    public class SimulationInvoker
    {
        private Stack<ICommand> _commands = new Stack<ICommand>();
        public void ExecuteCommand(List<ICommand> commands, ref SimulationData data)
        {
            foreach (var command in commands)
            {
                command.Set(data);
                command.Execute();
                _commands.Push(command);
               
            }
        }

        public void UndoCommands(ref SimulationData data)
        {
            ICommand command = _commands?.Pop();
            command?.Set(data);
            command?.Undo();

        }

        public void UndoAllCommands()
        {
            int count = _commands.Count;
            for (int i = 0; i < count; i++)
            {
                _commands.Pop().Undo();
            }
        }
    }
}
using System.Collections.Generic;
using _Scripts.Interface;
using _Scripts.Simulation.SimulationSettings;
using JetBrains.Annotations;

namespace _Scripts.Commands
{
    public class SimulationInvoker
    {
        private Stack<List<ICommand>> _commands = new Stack<List<ICommand>>();
        public SimulationData ExecuteCommand(List<ICommand> commands, ref SimulationData data)
        {
            foreach (var command in commands)
            {
                command.Set(data);
                command.Execute();
                data = command.Data;
            }
            _commands.Push(commands);
            return data;
            
        }

        public bool RemoveRecentCommand()
        {
            if (_commands.Count <= 0)
                return false;
            _commands.Pop();
            return true;
        }
        public bool UndoCommands(ref SimulationData data)
        {
            if (_commands.Count <= 0)
                return false; 
            List<ICommand> commands = _commands.Pop();
            foreach (var command in commands)
            {
                command.Set(data);
                command.Undo();
                data = command.Data;
            }
            return true; 
        }
        public SimulationData UndoAllCommands(ref SimulationData data)
        {
            if(_commands.Count <= 0)
                return data;
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

            return data;
        }
    }
}
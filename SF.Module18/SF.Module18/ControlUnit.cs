using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.Module18
{
    /// <summary>
    /// Класс Sender
    /// </summary>
    internal class ControlUnit
    {
        List<Command> commandHistory = new List<Command>();
        Command _command;

        public void SetCommand(Command command)
        {
            _command = command;
            commandHistory.Add(command);
        }

        // Выполнить
        public async Task Run()
        {
            await _command.Execute();
        }

        // Отменить
        public void Cancel()
        {
            _command.Undo();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TelBot.Model
{
    public class Command : ICommand
    {
        public Action<object> _execute;
        public Predicate<object> _canExecute;

        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            else _execute = execute;

            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute(parameter) : true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}

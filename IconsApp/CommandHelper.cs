using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace IconsApp
{
    public class CommandHelper : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExectue;

        public event EventHandler CanExecuteChanged;
        public CommandHelper(Action<object> execute, Func<object,bool> canExectue)
        {
            _execute = execute;
            _canExectue = canExectue;
        }

        public CommandHelper(Action<object> execute)
        {
            _canExectue = _ => true;
            _execute = execute;
        }
        public CommandHelper(Action execute)
        {
            _canExectue = _ => true;
            _execute = _ => execute();
        }

        public bool CanExecute(object parameter)
            => _canExectue.Invoke(parameter);

        public void Execute(object parameter)
            => _execute.Invoke(parameter);            
    }
}

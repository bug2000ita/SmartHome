using System;
using System.Windows.Input;

namespace SmartHome.Infra
{
    public class DelegateCommand:ICommand
    {

        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;
        
        public DelegateCommand(Action<object> execute)
        {
            this.execute = execute;
            this.canExecute = (x) => true;
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

    }
}
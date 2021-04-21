using System;
using System.Windows.Input;

namespace DemoHost
{
    public class RelayCommand : ICommand
    {
        protected Predicate<object> canExecute;
        protected Action<object> execute;
        private EventHandler canExecuteChanged;

        public RelayCommand(Action<object> execute)
        {            
            this.execute = execute;
        }

        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                execute?.Invoke(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                canExecuteChanged += value;
                
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                canExecuteChanged -= value;                
            } 
        }

        public void RaiseCanExecuteChanged()
        {
            canExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}

using System;
using System.Windows.Input;

namespace Services.Commands
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(
            Action<object> Execute, 
            Func<object, bool>? CanExecute = null)
        {
            if (Execute != null) _execute = Execute;
            _canExecute = CanExecute!;
        }

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public bool CanExecute(object? parameter) =>
            _canExecute?.Invoke(parameter!) ?? true;

        public void Execute(object? parameter) =>
            _execute(parameter!);
    }
}

using System;
using System.Windows;
using System.Windows.Input;

namespace lijohnttle.Media.Photo.App.ViewModels.Common
{
    public class DelegateCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;

            WeakEventManager<CommandManager, EventArgs>
                .AddHandler(null, nameof(CommandManager.RequerySuggested), OnCommandManagerRequerySuggested);
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            execute();
        }

        private void OnCommandManagerRequerySuggested(object sender, EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

using System;
using System.Windows;
using System.Windows.Input;

namespace lijohnttle.Media.Photo.App.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected CommandBase()
        {
            WeakEventManager<CommandManager, EventArgs>
                .AddHandler(null, nameof(CommandManager.RequerySuggested), OnCommandManagerRequerySuggested);
        }

        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        private void OnCommandManagerRequerySuggested(object sender, EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

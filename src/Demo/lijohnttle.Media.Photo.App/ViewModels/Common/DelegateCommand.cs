using lijohnttle.Media.Photo.App.Commands;
using System;
using System.Windows;
namespace lijohnttle.Media.Photo.App.ViewModels.Common
{
    public class DelegateCommand : CommandBase
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return canExecute?.Invoke() ?? true;
        }

        public override void Execute(object parameter)
        {
            execute();
        }
    }
}

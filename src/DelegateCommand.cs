using System;
using System.Windows.Input;

namespace Clicknado
{
    /// <summary>A simple implementation of ICommand which defers execution and executable queries to the caller.</summary>
    /// <remarks>
    /// Using this in conjunction with anonymous functions and the like can reduce a lot of WPF boilerplate ceremony.
    /// Simplified further from the Hardcodet NotifyIcon tutorial: https://www.codeproject.com/Articles/36468/WPF-NotifyIcon-2
    /// </remarks>
    public class DelegateCommand : ICommand
    {
        public Action Action { get; set; }
        public Func<bool> CanUse { get; set; }

        public void Execute(object parameter)
        {
            Action();
        }

        public bool CanExecute(object parameter)
        {
            return CanUse == null || CanUse();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

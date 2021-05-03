using System.Windows;
using System.Windows.Input;

namespace Clicknado
{
    /// <summary>View model for the Clicknado Notify Icon.</summary>
    public class NotifyIconViewModel
    {
        /// <summary>Shows the configuration window. Creates it if needed, else just tries to bring it to the foreground.</summary>
        public ICommand ShowConfigurationWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    Action = () =>
                    {
                        if (Application.Current.MainWindow == null)
                        {
                            Application.Current.MainWindow = new MainWindow();
                        }
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }

        /// <summary>Exits the application.</summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand
                {
                    Action = () =>
                    {
                        MainWorker.Shutdown();
                        Application.Current.Shutdown();
                    }
                };
            }
        }

        /// <summary>Enables the input router system. All configured input routers will start operating.</summary>
        public ICommand EnableInputRoutersCommand
        {
            get
            {
                return new DelegateCommand
                {
                    Action = () => MainWorker.Enable(),
                    CanUse = () => !MainWorker.IsEnabled
                };
            }
        }

        /// <summary>Disables the input router system. All configured input routers will stop operating.</summary>
        public ICommand DisableInputRoutersCommand
        {
            get
            {
                return new DelegateCommand
                {
                    Action = () => MainWorker.Disable(),
                    CanUse = () => MainWorker.IsEnabled
                };
            }
        }
    }
}

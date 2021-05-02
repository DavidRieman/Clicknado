using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace Clicknado
{
    /// <summary>The main Clicknado application class.</summary>
    /// <remarks>Initiates the Notification Icon as this application mainly lives in the notification tray.</remarks>
    public partial class ClicknadoApplication : Application
    {
        private TaskbarIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            notifyIcon = (TaskbarIcon)FindResource("ClicknadoNotifyIcon");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}

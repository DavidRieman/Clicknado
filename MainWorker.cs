using LowLevelInput.Hooks;
using System;
using System.Threading;

namespace Clicknado
{
    public static class MainWorker
    {
        private static Thread workerThread;
        private static LowLevelKeyboardHook keyboardHook;

        public static bool IsEnabled { get; private set; } = false;

        public static void Enable()
        {
            IsEnabled = true;
            if (workerThread == null)
            {
                workerThread = new Thread(DoWork);
                workerThread.Start();
            }
        }

        public static void Disable()
        {
            IsEnabled = false;
            workerThread = null;
        }

        public static void Shutdown()
        {
            IsEnabled = false;
            if (workerThread != null)
            {
                // Try to give a moment to wrap up naturally.
                workerThread.Join(100);
                workerThread = null;
            }
        }

        private static void DoWork()
        {
            keyboardHook = new LowLevelKeyboardHook();
            keyboardHook.OnKeyboardEvent += OnKeyboardEvent;
            keyboardHook.InstallHook();

            while (IsEnabled)
            {
                Thread.Sleep(11);
            }
        }

        private static void OnKeyboardEvent(VirtualKeyCode key, KeyState state)
        {
            Console.WriteLine($"{key} {state}");
        }
    }
}

using LowLevelInput.Hooks;
using LowLevelInput.WindowsHooks;
using System.Threading;

namespace Clicknado
{
    public static class MainWorker
    {
        private static Thread workerThread;
        private static LowLevelKeyboardHook keyboardHook;
        private static readonly bool[] keysDown = new bool[255];

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
            WindowsHookFilter.Filter += (VirtualKeyCode key, KeyState state) => {
                if (key == VirtualKeyCode.F11)
                {
                    // Filter the event (blocking it from being handled by other programs) but still mark the key as down/up accordingly.
                    OnKeyboardEvent(key, state);
                    return true;
                }
                return false;
            };
            
            keyboardHook = new LowLevelKeyboardHook();
            keyboardHook.OnKeyboardEvent += OnKeyboardEvent;
            keyboardHook.InstallHook();

            while (IsEnabled)
            {
                Triggers.Handle(keysDown);
                Thread.Sleep(1);
            }
        }

        private static void OnKeyboardEvent(VirtualKeyCode key, KeyState state)
        {
            // We need this to be fast, because some Windows are really picky about low level hook registrations taking too much time.
            // For a guaranteed fast per-key reaction, we want to avoid locks and thus fancy data structures with locking concerns.
            // As such, we're going to just maintain a small array of keys with boolean down state for reading in another thread, as we
            // can safely switch the boolean value without causing issues with the other thread.
            int index = (int)key;
            if (index >= 0 && index < 255)
            {
                keysDown[index] = state == KeyState.Down;
            }
        }
    }
}

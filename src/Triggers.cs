using LowLevelInput.Hooks;
using System;
using System.Linq;
using System.Collections.Generic;
using SimWinInput;

namespace Clicknado
{
    public class TriggerAction
    {
        /// <summary>Total time (in milliseconds) which should pass before executing this action again.</summary>
        public int ActionDelayMS { get; set; }

        /// <summary>The last time this action was triggered.</summary>
        public DateTime LastActionTime { get; set; }
    }

    public class Trigger
    {
        /// <summary>One ore more key codes which trigger something to happen while pressed down (at the same time).</summary>
        public List<VirtualKeyCode> KeyCodes { get; set; }

        /// <summary>The action to perform in response to the keys being held.</summary>
        public TriggerAction TriggerAction { get; set; }
    }

    public static class Triggers
    {
        private static readonly List<Trigger> triggers = new List<Trigger>();
        private static bool lastRunHandledMouseDown = false;
        private static bool leftMouseHeldDown = false;

        static Triggers()
        {
            // TEMPORARY TEST: Add a specific trigger for testing.
            triggers.Add(new Trigger()
            {
                KeyCodes = new List<VirtualKeyCode>() { VirtualKeyCode.F11 },
                TriggerAction = new TriggerAction()
                {
                    ActionDelayMS = 10,
                    LastActionTime = DateTime.Now
                }
            });
        }

        // TODO: Also a HandleToggle for keys to be registered as TOGGLES instead of just "HOLD-TO-SPAM"?  Enum for TriggerType perhaps?
        public static void Handle(bool[] keysDown)
        {
            // For simplicity, each time we run will either be a mouse-down pass, or a mouse-up pass. So the fastest we can try to spam
            // is roughly cycles of 1 ms held down, 1 ms held up.
            if (lastRunHandledMouseDown)
            {
                // Determine whether mouse is held down, in order to trigger the mouse back up.
                if (leftMouseHeldDown)
                {
                    InteropMouse.mouse_event((uint)InteropMouse.MouseEventFlags.LeftUp, 0, 0, 0, 0);
                    leftMouseHeldDown = false;
                }
                lastRunHandledMouseDown = false;
            }
            else
            {
                // Determine whether left mouse down should occur, based on at least one registered key combo being held.
                var now = DateTime.Now;
                var reactions = from t in triggers
                                let reaction = t.TriggerAction
                                where t.KeyCodes.All(key => keysDown[(int)key]) &&
                                      reaction.LastActionTime.AddMilliseconds(reaction.ActionDelayMS) <= now
                                select reaction;
                if (reactions.Any())
                {
                    foreach (var reaction in reactions)
                    {
                        reaction.LastActionTime = DateTime.Now;
                    }
                    InteropMouse.mouse_event((uint)InteropMouse.MouseEventFlags.LeftDown, 0, 0, 0, 0);
                    leftMouseHeldDown = true;
                }
                lastRunHandledMouseDown = true;
            }
        }
    }
}

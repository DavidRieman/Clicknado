using LowLevelInput.Hooks;
using System;
using System.Collections.Generic;

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

        static Triggers()
        {
            // TEMPORARY TEST: Add a specific trigger for testing.
            triggers.Add(new Trigger()
            {
                KeyCodes = new List<VirtualKeyCode>() { VirtualKeyCode.F11 },
                TriggerAction = new TriggerAction()
                {
                    ActionDelayMS = 100,
                    LastActionTime = DateTime.Now
                }
            });
        }

    }
}

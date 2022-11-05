using System;

namespace _Project.Scripts.Core.InputManager
{
    public class ButtonInput : SourceInput
    {
        public Action OnClickDown { get; set; }
        public Action OnClickUp { get; set; }
        public bool IsDown { get; set; }
        public bool IsUp { get; set; }

        public ButtonInput(string inputID)
        {
            InputID = inputID;
        }
    }
}
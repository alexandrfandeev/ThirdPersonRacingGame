using System.Collections.Generic;

namespace _Project.Scripts.Core.InputManager
{
    public class GlobalInputAdapter
    {
        private Dictionary<string, SourceInput> _inputSources = new Dictionary<string, SourceInput>();
        public static GlobalInputAdapter Current { get; private set; }

        public static void Initialize()
        {
            Current = new GlobalInputAdapter();
        }

        public ButtonInput GetButton(string inputID)
        {
            var inputSource = _inputSources.ContainsKey(inputID) ? _inputSources[inputID] : new ButtonInput(inputID);
            _inputSources[inputID] = inputSource;
            return (ButtonInput)inputSource;
        }

        public AxisInput GetAxis(string inputID)
        {
            var inputSource = _inputSources.ContainsKey(inputID) ? _inputSources[inputID] : new AxisInput(inputID);
            _inputSources[inputID] = inputSource;
            return (AxisInput)inputSource;
        }
    }
}
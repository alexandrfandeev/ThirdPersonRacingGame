using _Project.Scripts.Core.InputManager;
using UnityEngine;

namespace _Project.Scripts.VehicleController.Entities
{
    public class VehicleInputs : MonoBehaviour
    {
        public AxisInput Axis  => GlobalInputAdapter.Current.GetAxis(PCInputParameters.Axis);
        public ButtonInput Space => GlobalInputAdapter.Current.GetButton(PCInputParameters.Space);
        public ButtonInput Shift => GlobalInputAdapter.Current.GetButton(PCInputParameters.Shift);
        public ButtonInput Control => GlobalInputAdapter.Current.GetButton(PCInputParameters.Control);
        
        public bool IsLocked { get; private set; }

        public void LockInputs(bool isLocked)
        {
            IsLocked = isLocked;
        }
    }
}

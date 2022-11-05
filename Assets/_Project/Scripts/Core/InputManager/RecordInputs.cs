using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Core.InputManager
{
    public class RecordInputs : MonoBehaviour
    {
        private AxisInput _axis;
        private ButtonInput _space;
        private ButtonInput _shift;
        private ButtonInput _control;

        private void Start()
        {
            _axis = GlobalInputAdapter.Current.GetAxis(PCInputParameters.Axis);
            _space = GlobalInputAdapter.Current.GetButton(PCInputParameters.Space);
            _shift = GlobalInputAdapter.Current.GetButton(PCInputParameters.Shift);
            _control = GlobalInputAdapter.Current.GetButton(PCInputParameters.Control);
            StartCoroutine(Record());
        }

        private IEnumerator Record()
        {
            while (true)
            {
                _axis.Horizontal = Input.GetAxisRaw("Horizontal");
                _axis.Vertical = Input.GetAxisRaw("Vertical");
                _space.IsDown = Input.GetKey(KeyCode.Space);
                _shift.IsDown = Input.GetKey(KeyCode.LeftShift);
                _control.IsDown = Input.GetKey(KeyCode.LeftControl);
                yield return null;
            }
        }
    }
}

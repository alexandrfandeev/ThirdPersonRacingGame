using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.PauseSystem;
using UnityEngine;

namespace _Project.Scripts.VehicleController.Utilities.AI
{
    public class AiWheelsControls : MonoBehaviour
    {
        [SerializeField] private int wheelSpeedAmplifier = 150;

        private List<WheelItem> _wheels = new List<WheelItem>();
        private Rigidbody _rigidBody;
        private Coroutine _preparingRoutine;

        private Vector3 _startRotation;
        private float _turningDegree;
        private float _angleX;
        private float _axisRotation;
        private bool _valueForParticles;

        public void Initialize(Rigidbody rigidBody)
        {
            _wheels = GetComponentsInChildren<WheelItem>().ToList();
            _rigidBody = rigidBody;
            _startRotation = transform.localEulerAngles;
            _angleX = _startRotation.x;
            _preparingRoutine = StartCoroutine(PrepareToRace());
        }

        private IEnumerator PrepareToRace()
        {
            float axisRotation = 0;
            float axisMultiplier = 15f;
            while (true)
            {
                axisRotation += axisMultiplier;
                Vector3 rotationAngle = new Vector3(axisRotation, _startRotation.y, _startRotation.z);
                if (!PauseManager.Current.IsOnPause) _wheels.ForEach(x => x.Rotate(rotationAngle));
                yield return null;
            }
        }

        public void StopPreparing()
        {
            if (_preparingRoutine != null) StopCoroutine(_preparingRoutine);
        }

        public void ControlWheels(float axisRotation)
        {
            _axisRotation = axisRotation;
            _angleX += Time.deltaTime * wheelSpeedAmplifier * _rigidBody.velocity.magnitude;
            _turningDegree = _axisRotation / 1.2f; // for wheel's turn. If the car is turning on 80 degree, then the wheel will turn on ~60
            if (_wheels.Count < 1) return; 
            Vector3 rotationAngle = new Vector3(_angleX, -_turningDegree, _startRotation.z);
            _wheels.ForEach(x => x.Rotate(rotationAngle));
        }
    }
}
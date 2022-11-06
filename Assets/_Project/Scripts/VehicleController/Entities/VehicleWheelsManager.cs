using UnityEngine;

namespace _Project.Scripts.VehicleController.Entities
{
    public class VehicleWheelsManager : MonoBehaviour
    {
        [Header("Wheels Options")]
        [Range(0.8f, 1.7f)] public float Friction;
        [SerializeField] private CarController _carController;
        [HideInInspector] public GameObject[] wheelObjects;

        private WheelCollider[] _wheelColliders;
        private Vector3 _wheelPosition;
        private Quaternion _wheelRotation;
        private float _sidewaysFriction;
        private float _forwardFriction;
        
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void Initialize(float verticalValue, WheelCollider[] colliders)
        {
            _wheelColliders = colliders;
            _sidewaysFriction = _forwardFriction = Friction;
            SetUpWheels(verticalValue);
        }

        public void HandleWheels()
        {
            _sidewaysFriction = _forwardFriction = Friction;
            _carController.ForwardStifness = _forwardFriction;
            _carController.SidewaysStifness = _sidewaysFriction;
            Animate();
        }
        private void Animate()
        {
            for (int i = 0; i < _wheelColliders.Length; i++)
            {
                _wheelColliders[i].GetWorldPose(out _wheelPosition, out _wheelRotation); 
                wheelObjects[i].transform.position = _wheelPosition; 
                wheelObjects[i].transform.rotation = _wheelRotation;
            }
        }
        
        private void SetUpWheels(float verticalValue)
        {
            WheelFrictionCurve curve;

            for (int i = 0; i < _wheelColliders.Length; i++)
            {
                curve = _wheelColliders[i].forwardFriction;

                curve.asymptoteValue = 1;
                curve.extremumSlip = 0.065f;
                curve.asymptoteSlip = 0.8f;
                curve.stiffness = (verticalValue < 0) ? _forwardFriction * 2 : _forwardFriction;
                _wheelColliders[i].forwardFriction = curve;

                curve = _wheelColliders[i].sidewaysFriction;

                curve.asymptoteValue = 1;
                curve.extremumSlip = 0.065f;
                curve.asymptoteSlip = 0.8f;
                curve.stiffness = (verticalValue < 0) ? _sidewaysFriction * 2 : _sidewaysFriction;
                _wheelColliders[i].sidewaysFriction = curve;
            }
        }
    }
}

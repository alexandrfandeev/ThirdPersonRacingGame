using _Project.vehicle;
using UnityEngine;

namespace _Project.Scripts.VehicleController.Entities
{
    public class VehicleWheelsManager : MonoBehaviour
    {
        [Header("Wheels Options")] 
        public WheelCollider[] Colliders;
        [Range(0.8f, 1.7f)] public float Friction;
        
        [SerializeField] private controller controller;

        [HideInInspector] public GameObject[] wheelObjects;
        private Vector3 _wheelPosition;
        private Quaternion _wheelRotation;
        private float _sidewaysFriction;
        private float _forwardFriction;
        
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void Initialize(float verticalValue)
        {
            _sidewaysFriction = _forwardFriction = Friction;
            SetUpWheels(verticalValue);
        }

        public void HandleWheels()
        {
            _sidewaysFriction = _forwardFriction = Friction;
            controller.ForwardStifness = _forwardFriction;
            controller.SidewaysStifness = _sidewaysFriction;
            Animate();
        }
        private void Animate()
        {
            for (int i = 0; i < Colliders.Length; i++)
            {
                Colliders[i].GetWorldPose(out _wheelPosition, out _wheelRotation);
                wheelObjects[i].transform.position = _wheelPosition;
                wheelObjects[i].transform.rotation = _wheelRotation;
            }
        }
        
        private void SetUpWheels(float verticalValue)
        {
            WheelFrictionCurve curve;

            for (int i = 0; i < Colliders.Length; i++)
            {
                curve = Colliders[i].forwardFriction;

                curve.asymptoteValue = 1;
                curve.extremumSlip = 0.065f;
                curve.asymptoteSlip = 0.8f;
                curve.stiffness = (verticalValue < 0) ? _forwardFriction * 2 : _forwardFriction;
                Colliders[i].forwardFriction = curve;

                curve = Colliders[i].sidewaysFriction;

                curve.asymptoteValue = 1;
                curve.extremumSlip = 0.065f;
                curve.asymptoteSlip = 0.8f;
                curve.stiffness = (verticalValue < 0) ? _sidewaysFriction * 2 : _sidewaysFriction;
                Colliders[i].sidewaysFriction = curve;
            }
        }
    }
}

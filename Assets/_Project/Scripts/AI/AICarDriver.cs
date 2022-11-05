using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.SceneSystem;
using _Project.Scripts.VehicleController.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.AI
{
    public class AICarDriver : MonoBehaviour
    {
        [SerializeField] private List<VehicleAIMesh> _aiMeshes = new List<VehicleAIMesh>();
        
        
        private Rigidbody _rigidBody;
        private Transform _ownTransform;
        private Vector3 _targetPosition;
        private Vector3 _directionToMovePosition;
        private float _speed;
        private float _distanceToTarget;
        private float _dotProduct;
        private float _forwardAmount;
        private float _angleToDirection;
        private float _turnAmount;
        
        #region Fields
        private float _speedMax = 13f;
        private float _speedMin = -5f;
        private float _acceleration = 10f;
        private float _brakeSpeed = 100f;
        private float _reverseSpeed = 30f;
        private float _idleSlowdown = 10f;

        private float _turnSpeed;
        private float _turnSpeedMax = 300f;
        private float _turnSpeedAcceleration = 300f;
        private float _turnIdleSlowdown = 500f;
        #endregion
        
        public void Initialize(Transform target)
        {
            print(ServiceLocator.Current.Get<ISceneManager>().Scene - 2);
            _aiMeshes[ServiceLocator.Current.Get<ISceneManager>().Scene - 2].EnableMesh();
            _rigidBody = GetComponent<Rigidbody>();
            _ownTransform = transform;
            _targetPosition = target.position;
            StartCoroutine(Move());
        }
        
        private IEnumerator Move()
        {
            while (true)
            {
                float reachedTargetDistance = 4f;
                _distanceToTarget = Vector3.Distance(_ownTransform.position, _targetPosition);
                if (_distanceToTarget > reachedTargetDistance)
                {
                    //still to far, keep going
                    _directionToMovePosition = (_targetPosition - _ownTransform.position).normalized;
                    _dotProduct = Vector3.Dot(_ownTransform.forward, _directionToMovePosition);
                    if (_dotProduct > 0)
                    {
                        // target in front
                        _forwardAmount = 1f;

                        float stoppingDistance = 30f;
                        float stoppingSpeed = 50f;
                        if (_distanceToTarget < stoppingDistance && _speed > stoppingSpeed)
                        {
                            //within stopping distance and moving forward too fast 
                            _forwardAmount = -1f;
                        }
                    }

                    else
                    {
                        //target behind
                        float reverseDistance = 25f;
                        if (_distanceToTarget > reverseDistance)
                        {
                            //too far to reverse
                            _forwardAmount = 1f;
                        }

                        else
                        {
                            _forwardAmount = -1f; 
                        }
                    }
                    _angleToDirection = Vector3.SignedAngle(_ownTransform.forward, _directionToMovePosition, Vector3.up);
                    _turnAmount = _angleToDirection > 0 ? 1f : -1f;
                }

                else
                {
                    //reached target
                    _forwardAmount = _speed > 15f ? -1f : 0f;
                    _turnAmount = 0f;
                }
                SetInputs();
                yield return null;
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void SetInputs()
        {
                if (_forwardAmount > 0) {
            // Accelerating
            _speed += _forwardAmount * _acceleration * Time.deltaTime;
        }
        if (_forwardAmount < 0) {
            if (_speed > 0) {
                // Braking
                _speed += _forwardAmount * _brakeSpeed * Time.deltaTime;
            } else {
                // Reversing
                _speed += _forwardAmount * _reverseSpeed * Time.deltaTime;
            }
        }

        if (_forwardAmount == 0) {
            // Not accelerating or braking
            if (_speed > 0) {
                _speed -= _idleSlowdown * Time.deltaTime;
            }
            if (_speed < 0) {
                _speed += _idleSlowdown * Time.deltaTime;
            }
        }

        _speed = Mathf.Clamp(_speed, _speedMin, _speedMax);

        _rigidBody.velocity = transform.forward * _speed;

        if (_speed < 0) {
            // Going backwards, invert wheels
            _turnAmount = _turnAmount * -1f;
        }

        if (_turnAmount > 0 || _turnAmount < 0) {
            // Turning
            if ((_turnSpeed > 0 && _turnAmount < 0) || (_turnSpeed < 0 && _turnAmount > 0)) {
                // Changing turn direction
                float minTurnAmount = 20f;
                _turnSpeed = _turnAmount * minTurnAmount;
            }
            _turnSpeed += _turnAmount * _turnSpeedAcceleration * Time.deltaTime;
        } else {
            // Not turning
            if (_turnSpeed > 0) {
                _turnSpeed -= _turnIdleSlowdown * Time.deltaTime;
            }
            if (_turnSpeed < 0) {
                _turnSpeed += _turnIdleSlowdown * Time.deltaTime;
            }
            if (_turnSpeed > -1f && _turnSpeed < +1f) {
                // Stop rotating
                _turnSpeed = 0f;
            }
        }

        float speedNormalized = _speed / _speedMax;
        float invertSpeedNormalized = Mathf.Clamp(1 - speedNormalized, .75f, 1f);

        _turnSpeed = Mathf.Clamp(_turnSpeed, -_turnSpeedMax, _turnSpeedMax);

        _rigidBody.angularVelocity = new Vector3(0, _turnSpeed * (invertSpeedNormalized * 1f) * Mathf.Deg2Rad, 0);

        if (transform.eulerAngles.x > 2 || transform.eulerAngles.x < -2 || transform.eulerAngles.z > 2 || transform.eulerAngles.z < -2) {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        }
    }
}

using System;
using _Project.Scripts.Core;
using _Project.Scripts.Core.PauseSystem;
using _Project.Scripts.Core.SignalBus;
using UnityEngine;

namespace _Project.Scripts.VehicleController.Utilities
{
    public class CarRotator : MonoBehaviour, IPauseHandler
    {
        [SerializeField, Range(2.0f, 10.0f)] private float _rotationSpeed;

        private Transform _ownTransform;
        private Quaternion _turningAxis;
        private float _xAxis;
        private bool _isPause;
        private bool _isDragging;
        
        [Sub]
        private void OnStartScene(StartSceneLogic reference)
        {
            PauseManager.Current.Register(this);
            _ownTransform = transform;
            _isDragging = true;
        }

        private void OnMouseDrag()
        { 
            if (!_isDragging || _isPause) return;
            _xAxis = Input.GetAxis("Mouse X") * _rotationSpeed;
            _ownTransform.Rotate(Vector3.down, _xAxis);
        }

        public void SetPause()
        {
            _isPause = true;
        }

        public void Play()
        {
            _isPause = false;
        }
    }
}

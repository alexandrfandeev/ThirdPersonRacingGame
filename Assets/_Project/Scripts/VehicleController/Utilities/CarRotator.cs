using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class CarRotator : MonoBehaviour
{
    [SerializeField, Range(2.0f, 10.0f)] private float _rotationSpeed;
    
    
    private Transform _ownTransform;
    private Quaternion _turningAxis;
    private float _xAxis;
    private bool _isDragging;

    [Button()]
    public void Enable()
    {
        _ownTransform = transform;
        _isDragging = true;
    }
    private void OnMouseDrag()
    { 
        if (!_isDragging) return;
        _xAxis = Input.GetAxis("Mouse X") * _rotationSpeed;
        _ownTransform.Rotate(Vector3.down, _xAxis);
    }

    public void Disable()
    {
        _isDragging = false;
    }
}

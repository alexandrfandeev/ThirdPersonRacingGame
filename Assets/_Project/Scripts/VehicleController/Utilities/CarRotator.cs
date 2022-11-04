using Sirenix.OdinInspector;
using UnityEngine;

public class CarRotator : MonoBehaviour, IPauseHandler
{
    [SerializeField, Range(2.0f, 10.0f)] private float _rotationSpeed;
    
    
    private Transform _ownTransform;
    private Quaternion _turningAxis;
    private float _xAxis;
    private bool _isPause;
    private bool _isDragging;

    [Button()]
    public void Enable()
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

    public void Disable()
    {
        _isDragging = false;
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

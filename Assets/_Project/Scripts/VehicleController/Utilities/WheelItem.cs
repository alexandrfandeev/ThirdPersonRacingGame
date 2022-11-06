using UnityEngine;

namespace _Project.Scripts.VehicleController.Utilities
{
    public class WheelItem : MonoBehaviour
    {
        public void Rotate(Vector3 rotation)
        {
            transform.localEulerAngles = rotation;
        }
    }
}

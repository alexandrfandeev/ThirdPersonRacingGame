using UnityEngine;

namespace _Project.Scripts.VehicleController.Utilities
{
    public class VehicleAIMesh : MonoBehaviour
    {
        public void EnableMesh()
        {
            gameObject.SetActive(true);
        }

        public void DisableMesh()
        {
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

namespace _Project.Scripts.VehicleController.Utilities.AI
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

using UnityEngine;

namespace _Project.Scripts.GUi.Utilities
{
    public class View : MonoBehaviour
    {
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}

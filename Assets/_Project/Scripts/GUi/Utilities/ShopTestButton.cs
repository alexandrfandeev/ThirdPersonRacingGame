using UnityEngine;

namespace _Project.Scripts.GUi.Utilities
{
    public class ShopTestButton : MonoBehaviour
    {
        public void Buy(int count)
        {
#if !UNITY_EDITOR
                ResourcesSaveSystem.IncrementResourceAmount(Resource.Coin, count);
#endif
        }
    }
}

using _Project.Scripts.Resources;
using _Project.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.Purchasing;

namespace _Project.Scripts.IAP
{
    public class IAPManager : MonoBehaviour
    {
        private string _coin100 = "com.alexfandeev.thirdpersonracing.coin100";
        private string _coin200 = "com.alexfandeev.thirdpersonracing.coin200";
        
        
        public void OnPurchaseComplete(Product product)
        {
            if (product.definition.id == _coin100)
            {
                ResourcesSaveSystem.IncrementResourceAmount(Resource.Coin, 100);
            }

            if (product.definition.id == _coin200)
            {
                ResourcesSaveSystem.IncrementResourceAmount(Resource.Coin, 200);
            }
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(product.definition.id + " failed because " + failureReason);
        }
    }
}

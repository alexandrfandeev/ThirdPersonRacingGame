using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.User;
using _Project.Scripts.Resources;
using UnityEngine;

namespace _Project.Scripts.SaveSystem
{
    public static class ResourcesSaveSystem 
    {
        public static void IncrementResourceAmount(Resource resource, int amount)
        {
            string id = $"Resource_{resource}";
            PlayerPrefs.SetInt(id, GetResourceAmount(resource) + amount);
            Signal.Current.Fire<UserInfo>(new UserInfo {CoinsAmount = GetResourceAmount(resource), Update = true});
        }

        public static int GetResourceAmount(Resource resource)
        {
            string id = $"Resource_{resource}";
            return PlayerPrefs.GetInt(id, 0);
        }
    }
}

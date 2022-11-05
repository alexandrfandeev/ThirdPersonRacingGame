using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Core
{
    public class LocalAssetLoader
    {
        private GameObject _cachedObject;

        protected async Task<T> LoadInternal<T>(string assetID)
        {
            var handle = Addressables.InstantiateAsync(assetID);
            _cachedObject = await handle.Task;
            if (_cachedObject.TryGetComponent(out T gameObj) == false)
            {
                throw new NullReferenceException(
                    $"Object of type {typeof(T)} is null on attempt to load it from addressables");
            }

            return gameObj;
        }

        protected void UnloadInternal()
        {
            if (_cachedObject == null) return;
            _cachedObject.SetActive(false);
            Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }
    }
}

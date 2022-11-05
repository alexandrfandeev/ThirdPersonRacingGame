using System.Threading.Tasks;
using _Project.vehicle;
using UnityEngine;

namespace _Project.Scripts.Core.AssetsLoaders
{
    public class UserVehicleLocalAsset : LocalAssetLoader
    {
        public Task<controller> Load()
        {
            return LoadInternal<controller>("UserVehicle");
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}

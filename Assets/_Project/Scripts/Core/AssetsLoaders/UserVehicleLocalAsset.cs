using System.Threading.Tasks;
using _Project.Scripts.VehicleController.Entities;
using UnityEngine;

namespace _Project.Scripts.Core.AssetsLoaders
{
    public class UserVehicleLocalAsset : LocalAssetLoader
    {
        public Task<CarController> Load()
        {
            return LoadInternal<CarController>("UserVehicle");
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}

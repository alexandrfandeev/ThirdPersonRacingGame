using _Project.Scripts.AI;
using _Project.Scripts.Core.AssetsLoaders;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Resources;
using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.SceneSystem
{
    public class LevelEntity : MonoBehaviour
    {
        [SerializeField] private Transform _aiTarget;
        [SerializeField] private int _initialReward;
        [SerializeField] private int _wasCompletedReward;

        private AIVehicleLocalAsset AiAsset => AssetsProvider.Current.AIAsset;
        private UserVehicleLocalAsset UserAsset => AssetsProvider.Current.UserAsset;
        
        public async void Start()
        {
            var aiVehicle = await AiAsset.Load();
            var userVehicle = await UserAsset.Load();
            aiVehicle.Initialize(_aiTarget);
        }

        public void UnloadAssets()
        {
            AiAsset.Unload();
            UserAsset.Unload();
        }

        public int CalculateEarning()
        {
            bool isLevelComplete =
                LevelDataSaveSystem.GetLevelState(ServiceLocator.Current.Get<ISceneManager>().Scene) == 1;
             int earnings = isLevelComplete ? _wasCompletedReward : _initialReward;
             ResourcesSaveSystem.IncrementResourceAmount(Resource.Coin, earnings);
             return earnings;
        }
    }
}

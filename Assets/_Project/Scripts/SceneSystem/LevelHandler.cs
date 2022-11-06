using System;
using System.Collections;
using _Project.Scripts.AI;
using _Project.Scripts.CameraSystem;
using _Project.Scripts.Core.AssetsLoaders;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.Race;
using _Project.Scripts.Resources;
using _Project.Scripts.SaveSystem;
using _Project.Scripts.VehicleController;
using _Project.Scripts.VehicleController.Entities;
using _Project.Scripts.VehicleController.Upgrades;
using UnityEngine;

namespace _Project.Scripts.SceneSystem
{
    public class LevelHandler : MonoBehaviour, IService, ILevelHandler
    {
        [SerializeField] private LevelData _data;
        [SerializeField, Range(1, 10)] private int _raceCounter;
        [SerializeField, Range(0.1f, 3.0f)] private float _startingDelay = 1f;
        private AIVehicleLocalAsset AiAsset => AssetsProvider.Current.AIAsset;
        private UserVehicleLocalAsset UserAsset => AssetsProvider.Current.UserAsset;

        private AICarDriver _ai;
        private CarController _vehicle;
        
        public void InitializeService()
        {
            ServiceLocator.Current.Register<ILevelHandler>(this);
        }
        
        private async void Start()
        {
            _ai = await AiAsset.Load();
            _vehicle = await UserAsset.Load();
            StartCoroutine(PrepareToRace());
        }

        public void UnloadAssets()
        {
            AiAsset.Unload();
            UserAsset.Unload();
        }

        public int CalculateEarn()
        {
            bool wasLevelCompleted =
                LevelDataSaveSystem.GetLevelState(ServiceLocator.Current.Get<ISceneManager>().Scene) == 1;
            int earnings = wasLevelCompleted ? _data.CompletedLevelReward : _data.FirstTimeLevelReward;
            return earnings;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator PrepareToRace()
        {
            Signal.Current.Fire<EnableCounter>(new EnableCounter {Duration = _raceCounter, OnFinish =() =>
                {
                    _ai.StartRace();
                    _vehicle.StartRace();
                }
            });
            yield return new WaitForSeconds(_startingDelay);
            _vehicle.Initialize();
            _vehicle.ChangeColor(ServiceLocator.Current.Get<IVehicleController>().Data.Color);
            _ai.Initialize();
            ServiceLocator.Current.Get<ICameraManager>().SwitchCamera(_vehicle.transform);
        }
    }
    
    [Serializable] public class LevelData
    {
        public int CompletedLevelReward = 5;
        public int FirstTimeLevelReward = 10;
    }

    public interface ILevelHandler : IGameService
    {
        public void UnloadAssets();
        public int CalculateEarn();
    }
}

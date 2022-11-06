using System;
using _Project.Scripts.Core;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.VehicleController.Upgrades;
using UnityEngine;

namespace _Project.Scripts.VehicleController
{
    public class VehiclesController : MonoBehaviour, IService, IVehicleController
    {
        public VehicleData Data => _data;
        
        private VehicleData _data;
        
        public void InitializeService()
        {
            ServiceLocator.Current.Register<IVehicleController>(this);
        }

        [Sub]
        public void InitializeSystem(StartSceneLogic reference)
        {
            _data = new VehicleData();
            ServiceLocator.Current.Get<IUpgradeSystem>().Initialize(_data);
        }
    }

    [Serializable] public class VehicleData
    {
        public int HP;
        public Color Color;
    }

    public interface IVehicleController : IGameService
    {
        public VehicleData Data { get; }
    }
}

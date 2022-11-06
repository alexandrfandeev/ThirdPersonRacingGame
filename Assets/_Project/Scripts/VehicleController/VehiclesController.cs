using System;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.VehicleController.Upgrades;
using UnityEngine;

namespace _Project.Scripts.VehicleController
{
    public class VehiclesController : MonoBehaviour, IService, IVehicleController
    {
        [SerializeField] private CarRotator _rotator;
        
        public VehicleData Data => _data;
        
        private VehicleData _data;
        
        public void InitializeService()
        {
            ServiceLocator.Current.Register<IVehicleController>(this);
        }

        public void InitializeSystem()
        {
            _data = new VehicleData();
            ServiceLocator.Current.Get<IUpgradeSystem>().Initialize(_data);
            if (_rotator) _rotator.Enable();
        }
    }

    [Serializable] public class VehicleData
    {
        public int HP;
        public Color Color;
    }

    public interface IVehicleController : IGameService
    {
        public void InitializeSystem();
        public VehicleData Data { get; }
    }
}

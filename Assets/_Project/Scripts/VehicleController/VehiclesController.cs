using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.VehicleController.Upgrades;
using UnityEngine;

namespace _Project.Scripts.VehicleController
{
    public class VehiclesController : MonoBehaviour, IService, IVehicleController
    {
        [SerializeField] private CarRotator _rotator;
        
        public void InitializeService()
        {
            ServiceLocator.Current.Register<IVehicleController>(this);
        }

        public void InitializeSystem()
        {
            ServiceLocator.Current.Get<IUpgradeSystem>().Initialize();
            _rotator.Enable();
        }
    }

    public interface IVehicleController : IGameService
    {
        public void InitializeSystem();
    }
}

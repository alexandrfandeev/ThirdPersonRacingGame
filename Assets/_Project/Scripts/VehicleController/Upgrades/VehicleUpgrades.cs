using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using UnityEngine;

namespace _Project.Scripts.VehicleController.Upgrades
{
    public class VehicleUpgrades : MonoBehaviour, IService, IUpgradeSystem
    {
        [SerializeField] private List<UpgradeEntity> _upgrades = new List<UpgradeEntity>();

        public void InitializeService()
        {
            ServiceLocator.Current.Register<IUpgradeSystem>(this);
        }
        
        public void Initialize()
        {
            _upgrades.ForEach(x => x.Initialize());
        }

        public int UpgradePrice(UpgradeType type)
        {
            return _upgrades.Find(x => x.Type == type).CurrentPrice;
        }

        public int CurrentValue(UpgradeType type)
        {
            return _upgrades.Find(x => x.Type == type).Current;
        }

        public bool IsMaxLevel(UpgradeType type)
        {
            return _upgrades.Find(x => x.Type == type).IsMaxLevel;
        }

        public bool HaveMoney(UpgradeType type)
        {
            return _upgrades.Find(x => x.Type == type).HaveMoney;
        }
    }

    public interface IUpgradeSystem : IGameService
    {
        public void Initialize();
        public int UpgradePrice(UpgradeType type);
        public int CurrentValue(UpgradeType type);
        public bool IsMaxLevel(UpgradeType type);
        public bool HaveMoney(UpgradeType type);
    }
    
    public enum UpgradeType { Color, HealthPoints }
}

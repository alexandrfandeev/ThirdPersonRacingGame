using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.Resources;
using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.VehicleController.Upgrades.Entities
{
    public class HealthPoints : UpgradeEntity
    {
        [Header("Upgrade Values")]
        [SerializeField] private int _initialPoints = 50;
        [SerializeField] private int _pointsAdditive;
        [SerializeField] private int _maxLevel = 5;
        
        [Header("Price Values")]
        [SerializeField] private int _initialPrice = 10;
        [SerializeField] private int _additivePrice = 20;
        public override int Current => _initialPoints + (_pointsAdditive * (Level - 1));
        public override bool IsMaxLevel => Level >= _maxLevel;
        public override int CurrentPrice => _initialPrice + (_additivePrice * (Level - 1));
        public override bool HaveMoney => ResourcesSaveSystem.GetResourceAmount(Resource.Coin) >= CurrentPrice;

        public override UpgradeType Type => UpgradeType.HealthPoints;
        public override int Level => UpgradesSaveSystem.GetUpgradeLevel(Type);

        public override void Initialize()
        {
            Signal.Current.Fire<HealthPointsUpgradable>(new HealthPointsUpgradable {OnUpgrade = OnUpgrade});
        }

        public override void OnUpgrade()
        {
            if (HaveMoney)
            {
                ResourcesSaveSystem.IncrementResourceAmount(Resource.Coin, -CurrentPrice);
                UpgradesSaveSystem.SetUpgradeLevel(Type, Level + 1);
            } 
        }
    }
}

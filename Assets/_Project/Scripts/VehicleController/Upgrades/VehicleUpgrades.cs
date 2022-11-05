using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.VehicleController.Upgrades
{
    public class VehicleUpgrades : MonoBehaviour
    {
        [SerializeField] private List<UpgradeEntity> _upgrades = new List<UpgradeEntity>();

        public void Initialize()
        {
            _upgrades.ForEach(x => x.Initialize());
        }
    }
    
    public enum UpgradeType { Color, HealthPoints }
}

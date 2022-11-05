using UnityEngine;

namespace _Project.Scripts.VehicleController.Upgrades
{
    public abstract class UpgradeEntity : MonoBehaviour
    {
        public abstract UpgradeType Type { get; }
        public abstract int Level { get; }
        public abstract void Initialize();
        public abstract void OnUpgrade();
    }
}

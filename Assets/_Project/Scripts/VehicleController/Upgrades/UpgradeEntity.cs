using UnityEngine;

namespace _Project.Scripts.VehicleController.Upgrades
{
    public abstract class UpgradeEntity : MonoBehaviour
    {
        public abstract UpgradeType Type { get; }
        public abstract int Level { get; }
        public virtual int CurrentPrice { get; }
        public virtual int Current { get; }
        public virtual bool IsMaxLevel { get; }
        public virtual bool HaveMoney { get; }
        public abstract void Initialize();
        public abstract void OnUpgrade();
    }
}

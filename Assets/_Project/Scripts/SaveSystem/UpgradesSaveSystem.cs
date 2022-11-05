using _Project.Scripts.VehicleController.Upgrades;
using UnityEngine;

namespace _Project.Scripts.SaveSystem
{
    public static class UpgradesSaveSystem 
    {
        public static int GetUpgradeLevel(UpgradeType upgradeType)
        {
            string id = $"Upgrade_Type_{upgradeType}";
            return PlayerPrefs.GetInt(id, 1);
        }

        public static void SetUpgradeLevel(UpgradeType upgradeType, int level)
        {
            string id = $"Upgrade_Type_{upgradeType}";
            PlayerPrefs.SetInt(id, level);
        }
    }
}

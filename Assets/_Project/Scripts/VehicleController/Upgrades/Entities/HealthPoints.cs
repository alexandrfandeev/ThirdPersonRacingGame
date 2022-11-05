using _Project.Scripts.SaveSystem;

namespace _Project.Scripts.VehicleController.Upgrades.Entities
{
    public class HealthPoints : UpgradeEntity
    {
        public override UpgradeType Type => UpgradeType.HealthPoints;
        public override int Level => UpgradesSaveSystem.GetUpgradeLevel(Type);
        public override void Initialize()
        {
            
        }

        public override void OnUpgrade()
        {
            
        }
    }
}

namespace _Project.Scripts.Core.AssetsLoaders
{
    public class AssetsProvider 
    {
        public AIVehicleLocalAsset AIAsset { get; private set; }
        public UserVehicleLocalAsset UserAsset { get; private set; }
        
        public static AssetsProvider Current { get; private set; }
        
        public static void Initialize()
        {
            Current = new AssetsProvider();
            Current.AIAsset = new AIVehicleLocalAsset();
            Current.UserAsset = new UserVehicleLocalAsset();
        }
    }
}

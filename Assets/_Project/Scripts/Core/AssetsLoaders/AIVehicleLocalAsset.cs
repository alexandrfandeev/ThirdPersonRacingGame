using System.Threading.Tasks;
using _Project.Scripts.AI;
namespace _Project.Scripts.Core.AssetsLoaders
{
    public class AIVehicleLocalAsset : LocalAssetLoader
    {
        public Task<AICarDriver> Load()
        {
            return LoadInternal<AICarDriver>("BotAIVehicle");
        }

        public void Unload()
        {
            UnloadInternal();
        }
    }
}

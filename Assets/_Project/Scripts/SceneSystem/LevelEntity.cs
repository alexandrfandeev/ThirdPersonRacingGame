using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Resources;
using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.SceneSystem
{
    public class LevelEntity : MonoBehaviour
    {
        [SerializeField] private int _initialReward;
        [SerializeField] private int _wasCompletedReward;
        public int CalculateEarning()
        {
            bool isLevelComplete =
                LevelDataSaveSystem.GetLevelState(ServiceLocator.Current.Get<ISceneManager>().Scene) == 1;
             int earnings = isLevelComplete ? _wasCompletedReward : _initialReward;
             ResourcesSaveSystem.IncrementResourceAmount(Resource.Coin, earnings);
             return earnings;
        }
    }
}

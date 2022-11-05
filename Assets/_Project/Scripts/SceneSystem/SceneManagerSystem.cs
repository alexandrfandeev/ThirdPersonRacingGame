using _Project.Scripts.Ads;
using _Project.Scripts.Core.LocatorServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.SceneSystem
{
    public class SceneManagerSystem : MonoBehaviour, IService, ISceneManager
    {
        [SerializeField] private InterstitialAd _ad;
        public void InitializeService()
        {
            ServiceLocator.Current.Register<ISceneManager>(this);
        }

        public void LoadScene(int sceneIndex)
        {
            Debug.Log("loaded scene ..." + sceneIndex);
         //   SceneManager.LoadScene(sceneIndex + 1);
        }

        public void LoadSceneWithAd(int sceneIndex)
        {
            _ad.ShowAd();
        }
    }

    public interface ISceneManager : IGameService
    {
        public void LoadScene(int sceneIndex);
        public void LoadSceneWithAd(int sceneIndex);
    }
}

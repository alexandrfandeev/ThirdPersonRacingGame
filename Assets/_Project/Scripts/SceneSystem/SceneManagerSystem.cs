using _Project.Scripts.Ads;
using _Project.Scripts.Core.LocatorServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.SceneSystem
{
    public class SceneManagerSystem : MonoBehaviour, IService, ISceneManager
    {
        public int Scene => SceneManager.GetActiveScene().buildIndex;
        
        private InterstitialAd _ad;
        private int _adSceneID;
        
        public void InitializeService()
        {
            _ad = GetComponent<InterstitialAd>();
            ServiceLocator.Current.Register<ISceneManager>(this);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(Scene);
        }

        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadSceneWithAd(int sceneIndex)
        {
#if !UNITY_EDITOR
               SceneManager.LoadScene(sceneIndex + 1);
#endif
            
            _adSceneID = sceneIndex + 1;
            _ad.ShowAd(); 
        }

        public void LoadSceneAfterAd()
        {
            SceneManager.LoadScene(_adSceneID);
        }
    }

    public interface ISceneManager : IGameService
    {
        public void RestartScene();
        public int Scene { get; }
        public void LoadScene(int sceneIndex);
        public void LoadSceneWithAd(int sceneIndex);
        public void LoadSceneAfterAd();
    }
}

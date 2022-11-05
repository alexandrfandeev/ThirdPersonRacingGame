using _Project.Scripts.Core.LocatorServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.SceneSystem
{
    public class SceneManagerSystem : MonoBehaviour, IService, ISceneManager
    {
        public void InitializeService()
        {
            ServiceLocator.Current.Register<ISceneManager>(this);
        }

        public void LoadScene(int sceneIndex)
        {
            Debug.Log("loaded scene ..." + sceneIndex);
         //   SceneManager.LoadScene(sceneIndex + 1);
        }
    }

    public interface ISceneManager : IGameService
    {
        public void LoadScene(int sceneIndex);
    }
}

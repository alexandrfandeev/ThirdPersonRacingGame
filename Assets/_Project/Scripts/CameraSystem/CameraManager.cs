using _Project.Scripts.Core.LocatorServices;
using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.CameraSystem
{
    public class CameraManager : MonoBehaviour, IService, ICameraManager
    {
        [SerializeField] private CinemachineVirtualCamera _firstVrtualCamera;
        [SerializeField] private CinemachineVirtualCamera _secondVirtualCamera;
        public void InitializeService()
        {
            ServiceLocator.Current.Register<ICameraManager>(this);
        }

        public void SwitchCamera(Transform target)
        {
            _secondVirtualCamera.Follow = target;
            _firstVrtualCamera.gameObject.SetActive(false);
        }
    }

    public interface ICameraManager : IGameService
    {
        public void SwitchCamera(Transform target);
    }
}

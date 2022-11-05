using System.Collections.Generic;
using _Project.Scripts.Core.AssetsLoaders;
using _Project.Scripts.Core.InputManager;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.Notifications;
using _Project.Scripts.User;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class GameCore : MonoBehaviour
    {
       private List<MonoBehaviour> _initializationServices = new List<MonoBehaviour>();
       
        private void Awake()
        {
            AssetsProvider.Initialize();
            GlobalInputAdapter.Initialize();
            ServiceLocator.Initialize();
            PauseManager.Initialize();
            Signal.Initialize();
            FindAllServices();
            DOTween.KillAll();
        }

        private void Start()
        {
            ServiceLocator.Current.Get<INotification>().InitializeSystem();
            ServiceLocator.Current.Get<IUserData>().InitializeSystem();
        }


        private void FindAllServices()
        {
            _initializationServices = new List<MonoBehaviour>();
            MonoBehaviour[] objects = FindObjectsOfType<MonoBehaviour>(true);
            objects.ForEach(monoBehaviour =>
            {
                if (monoBehaviour is IService)
                {
                    _initializationServices.Add(monoBehaviour);
                }
            });
            foreach (MonoBehaviour monoBehaviour in _initializationServices)
            {
                if (monoBehaviour.TryGetComponent(out IService service))
                    service.InitializeService();
            }
        }
    }
}

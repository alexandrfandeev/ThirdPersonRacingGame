using System;
using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.Notifications;
using _Project.Scripts.User;
using _Project.Scripts.VehicleController;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField, InlineButton("FindAllServices", "Fix")] private List<MonoBehaviour> _initializationServices = new List<MonoBehaviour>();


        private void Awake()
        {
            ServiceLocator.Initialize();
            PauseManager.Initialize();
            Signal.Initialize();

            foreach (MonoBehaviour monoBehaviour in _initializationServices)
            {
                if (monoBehaviour.TryGetComponent(out IService service))
                    service.InitializeService();
            }

            DOTween.KillAll();
        }

        private void Start()
        {
            DOTween.KillAll();
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
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}

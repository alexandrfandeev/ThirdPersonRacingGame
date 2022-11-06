using System;
using _Project.Scripts.Core;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.LevelUI;
using _Project.Scripts.VehicleController;
using UnityEngine;

namespace _Project.Scripts.SceneSystem
{
    public class FinishObserver : MonoBehaviour
    {
        private bool _wasFirst = true;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out VehicleRecognizer vehicle))
            {
                switch (vehicle.Vehicle)
                {
                    case VehicleType.Bot:
                        _wasFirst = false;
                        break;
                    case VehicleType.Player:
                        ILevelHandler levelHandler = ServiceLocator.Current.Get<ILevelHandler>();
                        Signal.Current.Fire<EndSceneLogic>(new EndSceneLogic());
                        Signal.Current.Fire<FinishLevel>(new FinishLevel
                        {
                            IsWin = _wasFirst, 
                            EarnedCoins = levelHandler.CalculateEarn(),
                            OnSubmit = levelHandler.UnloadAssets
                        });
                        break;
                }
            }
        }
    }
}

using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.LevelUI;
using _Project.Scripts.VehicleController;
using UnityEngine;

namespace _Project.Scripts.SceneSystem
{
    public class FinishObserver : MonoBehaviour
    {
        [SerializeField] private LevelEntity _entity;
        
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
                        Signal.Current.Fire<FinishLevel>(new FinishLevel
                        {
                            IsWin = _wasFirst, 
                            EarnedCoins = _entity.CalculateEarning(),
                            OnSubmit = _entity.UnloadAssets
                        });
                        break;
                }
            }
        }
    }
}

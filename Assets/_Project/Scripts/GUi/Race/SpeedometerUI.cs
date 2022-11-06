using _Project.Scripts.Core;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.Utilities;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.Race
{
    public class SpeedometerUI : MonoBehaviour, IUpdateUI
    {
        [SerializeField] private View _view;
        [SerializeField] private TextMeshProUGUI _gearLabel;
        [SerializeField] private TextMeshProUGUI _speedLabel;
        [SerializeField] private RectTransform _arrow;
        
        
        private readonly float _maxSpeed = 260f;
        private readonly float _minArrowRotation = 6.14f;
        private readonly float _maxArrowRotation = -185f;

        private float _currentSpeed;
        
        [Sub]
        private void OnStart(StartSceneLogic reference)
        {
            UIUpdatesContainer.Current.Register(this);
            _gearLabel.text = "Gear: 1";
            _view.Enable();
        }

        [Sub]
        private void OnEnd(EndSceneLogic reference)
        {
            _view.Disable();
        }

        public void UpdateFloat(float value)
        {
            _currentSpeed = value * 3.6f;
            _speedLabel.text = ((int)_currentSpeed) + " km/h";
            _arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(_minArrowRotation, _maxArrowRotation, _currentSpeed / _maxSpeed));
        }

        public void UpdateInt(int number)
        {
            _gearLabel.text = "Gear : " + number;
        }
    }
}

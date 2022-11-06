using _Project.Scripts.Core;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.Utilities;
using UnityEngine;

namespace _Project.Scripts.GUi.Settings
{
    public class SettingsSystem : MonoBehaviour
    {
        [SerializeField] private View _view;
        [SerializeField] private SettingsPanel _panel;

        [Sub]
        private void OnStartLevel(StartSceneLogic reference)
        {
            _panel.Initialize();
            _view.Enable();
        }
    }
}

using UnityEngine;

namespace _Project.Scripts.GUi.Settings
{
    public class SettingsSystem : MonoBehaviour
    {
        [SerializeField] private SettingsPanel _panel;

        private void Start()
        {
            _panel.Initialize();
        }
    }
}

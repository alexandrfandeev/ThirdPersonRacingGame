using _Project.Scripts.Development;
using UnityEngine;

namespace _Project.Scripts.GUi.Settings.SettingsEntities
{
    public class MenuSettings : SettingsPanel
    {
        [SerializeField] private GraphicsApplier _graphics;
        public override void Initialize()
        {
            _graphics.Initialize();
        }

        public override void Open()
        {
            _settingsButton.interactable = false;
            UiUtilities.ScaleAndFade(() =>
            {
                PauseManager.Current.StartPause();
            }, _rectTransform, _background, 1f, 0.65f, _animationDuration);
        }

        public override void Close()
        {
            _settingsButton.interactable = true;
            UiUtilities.ScaleAndFade(() =>
            {
                PauseManager.Current.StopPause();
            }, _rectTransform, _background, 0f, 0f, _animationDuration);
        }
    }
}

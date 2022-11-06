using _Project.Scripts.Development;
using UnityEngine;

namespace _Project.Scripts.GUi.Settings.SettingsEntities
{
    public class MenuSettings : SettingsPanel
    {
        [SerializeField] private GraphicsApplier _graphics;
        public override void Initialize()
        {
            base.Initialize();
            _graphics.Initialize();
        }

        public override void Open()
        {
            UiUtilities.ScaleAndFade(() =>
            {
                PauseManager.Current.StartPause();
            }, _rectTransform, _background, 1f, 0.65f, _animationDuration);
        }

        public override void Close()
        {
            UiUtilities.ScaleAndFade(() =>
            {
                PauseManager.Current.StopPause();
            }, _rectTransform, _background, 0f, 0f, _animationDuration);
        }
    }
}

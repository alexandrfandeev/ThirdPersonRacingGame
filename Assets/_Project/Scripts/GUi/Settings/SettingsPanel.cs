using _Project.Scripts.Development;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Settings
{
    public class SettingsPanel : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private GraphicsApplier _graphics;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _background;
        [SerializeField] private float _animationDuration;

        public void Initialize()
        {
            PauseManager.Current.Register(this);
            _graphics.Initialize();
        }
    
    
        public void Open()
        {
            UiUtilities.ScaleAndFade(() =>
            {
                PauseManager.Current.StartPause();
            }, _rectTransform, _background, 1f, 0.65f, _animationDuration);
        }

        public void Close()
        {
            UiUtilities.ScaleAndFade(() =>
            {
                PauseManager.Current.StopPause();
            }, _rectTransform, _background, 0f, 0f, _animationDuration);
        }

        public void SetPause()
        {
            _settingsButton.interactable = false;
        }

        public void Play()
        {
            _settingsButton.interactable = true;
        }
    }
}

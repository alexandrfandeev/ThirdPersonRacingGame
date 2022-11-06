using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Settings
{
    public abstract class SettingsPanel : MonoBehaviour, IPauseHandler
    {
        [SerializeField] protected Button _settingsButton;
        [SerializeField] protected RectTransform _rectTransform;
        [SerializeField] protected Image _background;
        [SerializeField] protected float _animationDuration = 0.35f;

        public virtual void Initialize()
        {
            PauseManager.Current.Register(this);
        }
        public abstract void Open();

        public abstract void Close();
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

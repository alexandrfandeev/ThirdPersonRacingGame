using System;
using _Project.Scripts.Core.PauseSystem;
using _Project.Scripts.Development;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace _Project.Scripts.GUi.User.Entities
{
    public class UserCreationTab : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private RectTransform _usernameTab;
        [SerializeField] private Button _confirmNameButton;
        [SerializeField] private Image _background;
        [SerializeField] private float _apparitionDuration;
        
        private Action<string> _onCreate;
        private Action _onPress;
        private string _userName;
        
        
        public void Open(Action<string> onCreate, Action onPress)
        {
            _onCreate = onCreate;
            _onPress = onPress;
            _confirmNameButton.onClick.AddListener(CreateSuccessfully);
            UiUtilities.ScaleAndFade(PauseManager.Current.StartPause, _usernameTab, _background, 1f, 0.65f, _apparitionDuration);
        }


        private void CreateSuccessfully()
        {
            _userName = _inputField.text;
            _onCreate?.Invoke(_userName);
            _onPress?.Invoke();
            UiUtilities.ScaleAndFade(PauseManager.Current.StopPause, _usernameTab, _background, 0f, 0f, _apparitionDuration);
        }
    }
}

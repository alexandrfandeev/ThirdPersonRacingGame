using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.User.Entities
{
    public class UserCreationTab : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private RectTransform _usernameTab;
        [SerializeField] private Button _confirmNameButton;
        [SerializeField] private float _apparitionDuration;
        
        private Action<string> _onCreate;
        private string _userName;
        
        
        public void Open(Action<string> onCreate)
        {
            _onCreate = onCreate;
            _confirmNameButton.onClick.AddListener(CreateSuccessfully);
            _usernameTab.DOScale(1f, _apparitionDuration);
        }


        private void CreateSuccessfully()
        {
            _userName = _inputField.text;
            _onCreate?.Invoke(_userName);
            _usernameTab.DOScale(0f, _apparitionDuration);
        }
    }
}

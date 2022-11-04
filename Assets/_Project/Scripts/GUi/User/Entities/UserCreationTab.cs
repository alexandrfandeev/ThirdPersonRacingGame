using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.User.Entities
{
    public class UserCreationTab : MonoBehaviour
    {
        [SerializeField] private RectTransform _usernameTab;
        [SerializeField] private Button _confirmNameButton;
        [SerializeField] private float _apparitionDuration;
        
        private Action _onCreate;
        
        
        public void Open(Action onCreate)
        {
            _onCreate = onCreate;
            _confirmNameButton.onClick.AddListener(CreateSuccessfully);
            _usernameTab.DOScale(1f, _apparitionDuration);
        }


        private void CreateSuccessfully()
        {
            _onCreate?.Invoke();
            _usernameTab.DOScale(0f, _apparitionDuration);
        }
    }
}

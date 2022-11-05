using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.ColorChanger
{
    public class ColorButton : MonoBehaviour
    {
        [SerializeField] private Image _border;
        public int Value;

        private Action<int> _onSelect;
        private bool _isSelected;

        public void Initialize(Action<int> onSelect)
        {
            _onSelect = onSelect;
        }
        
        
        public void Select()
        {
            if (_isSelected) return;
            _border.DOFade(1f, 0.3f);
            _border.rectTransform.DOScale(1.2f, 0.3f);
            _isSelected = true;
            _onSelect?.Invoke(Value);
        }

        public void Deselect()
        {
            _border.DOFade(0f, 0.3f);
            _isSelected = false;
            _border.rectTransform.DOScale(1f, 0.3f);
        }
    }
}

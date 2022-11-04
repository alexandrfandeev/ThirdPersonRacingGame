using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Button _settingsButton;
    [SerializeField] private GraphicsApplier _graphics;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _background;
    [SerializeField] private float _animationDuration;

    public void Initialize()
    {
        _graphics.Initialize();
    }
    
    
    public void Open()
    {
        _settingsButton.interactable = false;
        _rectTransform.DOScale(1f, _animationDuration);
        _background.DOFade(0.65f, _animationDuration);
    }

    public void Close()
    {
        _settingsButton.interactable = true;
        _rectTransform.DOScale(0f, _animationDuration);
        _background.DOFade(0f, _animationDuration);
    }
}

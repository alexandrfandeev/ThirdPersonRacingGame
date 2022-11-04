using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Development
{
    public static class UiUtilities
    {
        public static void ScaleAndFade(Action action, RectTransform rect, Image img, float scale, float fade, float duration)
        {
            action?.Invoke();
            rect.DOScale(scale, duration);
            img.DOFade(fade, duration);
        }
    }
}

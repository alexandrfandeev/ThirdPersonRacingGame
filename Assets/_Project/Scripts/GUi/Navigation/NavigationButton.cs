using System;
using _Project.Scripts.Core.PauseSystem;
using _Project.Scripts.GUi.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.Navigation
{
    public abstract class NavigationButton : View, IPauseHandler
    {
        [SerializeField] protected float _animationDuration;
        [SerializeField] protected Image _background;
        [SerializeField] protected RectTransform _rect;
        [SerializeField] protected Button _ownButton;

        public void Initialize()
        {
            PauseManager.Current.Register(this);
        }
        public abstract NavigationType NavType { get; }

        public abstract void OnInteract();
        public abstract void OnClose();
        public abstract void SetPause();
        public abstract void Play();
    }

    [Serializable] public enum NavigationType { Shop, Race, Mechanic }

}

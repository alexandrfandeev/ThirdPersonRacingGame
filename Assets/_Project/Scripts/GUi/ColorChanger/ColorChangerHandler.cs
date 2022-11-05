using System;
using System.Collections.Generic;
using _Project.Scripts.Core.SignalBus;
using UnityEngine;

namespace _Project.Scripts.GUi.ColorChanger
{
    public class ColorChangerHandler : MonoBehaviour
    {
        [SerializeField] private List<ColorButton> _buttons = new List<ColorButton>();

        private Action<int> _onSelectColor;

        [Sub]
        private void OnSelectButtons(StartChangeColor reference)
        {
            _onSelectColor = reference.OnChangeColor;
            _buttons.ForEach(x => x.Initialize(OnSelectNecessaryColor));
            ColorButton necessary = _buttons.Find(x => x.Value == reference.CurrentID);
            necessary.Select();
        }

        private void OnSelectNecessaryColor(int colorID)
        {
            _onSelectColor?.Invoke(colorID);
            _buttons.FindAll(x => x.Value != colorID).ForEach(x => x.Deselect());
        }
    }

    public struct StartChangeColor
    {
        public int CurrentID;
        public Action<int> OnChangeColor;
    }
}

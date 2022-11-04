using System.Collections.Generic;
using _Project.Scripts.Core.SignalBus;
using UnityEngine;

namespace _Project.Scripts.GUi.Navigation
{
    public class MenuBarUi : MonoBehaviour
    {
        [SerializeField] private List<NavigationButton> _buttons = new List<NavigationButton>();


        private void Start()
        {
            _buttons.ForEach(x => x.Initialize());
        }

        [Sub]
        private void OnActivateButtons(MenuBarActivity reference)
        {
            if (reference.Enable)
            {
                _buttons.ForEach(x => x.Enable());
            }

            else
            {
                
            }
        }
    }

    public struct MenuBarActivity
    {
        public bool Enable;
    }
}

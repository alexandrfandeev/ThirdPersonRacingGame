using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.SceneSystem;
using UnityEngine;
namespace _Project.Scripts.GUi.RaceSelector
{
    public class RaceSelectionHandler : MonoBehaviour
    {
        public void SelectFirstRace()
        {
            OnSelectRace(1);
        }
        
        public void SelectSecondRace()
        {
            OnSelectRace(2);
        }


        private void OnSelectRace(int selectionIndex)
        {
            ServiceLocator.Current.Get<ISceneManager>().LoadSceneWithAd(selectionIndex);
        }
        
        
    }
}

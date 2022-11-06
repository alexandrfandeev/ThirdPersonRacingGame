using System.Collections;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Development;
using _Project.Scripts.SceneSystem;
using UnityEngine;
namespace _Project.Scripts.GUi.Settings.SettingsEntities
{
    public class RaceSettings : SettingsPanel
    {
        public override void Open()
        {
            _settingsButton.interactable = false;
            UiUtilities.ScaleAndFade(null, _rectTransform, _background, 1f, 0.65f, _animationDuration);
            StartCoroutine(AnimationUtilities.WaitForAction(() => Time.timeScale = 0f, _animationDuration));
        }

        public override void Close()
        {
            _settingsButton.interactable = true;
            Time.timeScale = 1f;
            UiUtilities.ScaleAndFade(null, _rectTransform, _background, 0f, 0f, _animationDuration);
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            ServiceLocator.Current.Get<ISceneManager>().RestartScene();
        }

        public void ReturnToMenu()
        {
           Time.timeScale = 1f;
            ServiceLocator.Current.Get<ISceneManager>().LoadScene(1);
        }
    }
}

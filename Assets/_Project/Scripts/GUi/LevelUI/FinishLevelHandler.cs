using System;
using System.Collections;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.Development;
using _Project.Scripts.Resources;
using _Project.Scripts.SaveSystem;
using _Project.Scripts.SceneSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts.GUi.LevelUI
{
    public class FinishLevelHandler : MonoBehaviour
    {
        [SerializeField] private float _delayOnFinish;
        [SerializeField] private float _animationDuration;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _coinsAmount;

        [SerializeField] private GameObject _winView;
        [SerializeField] private GameObject _loseView;

        private Action _onSubmit;
        private int _earnAmount;
        
        
        [Sub]
        private void OnFinishLevel(FinishLevel reference)
        {
            _onSubmit = reference.OnSubmit;
            PauseManager.Current.StartPause();
            if (reference.IsWin)
            {
                LevelDataSaveSystem.SetLevelComplete(ServiceLocator.Current.Get<ISceneManager>().Scene);
                _earnAmount = reference.EarnedCoins;
                _winView.SetActive(true);
                _coinsAmount.text = _earnAmount.ToString();
            }
            else _loseView.SetActive(true);
            StartCoroutine(OpenWithDelay());
        }

        private IEnumerator OpenWithDelay()
        {
            yield return new WaitForSeconds(_delayOnFinish);
            UiUtilities.ScaleAndFade(null, _rect, _background, 1f, 0.65f, _animationDuration);
        }

        public void OnRestartLevel()
        {
            _onSubmit?.Invoke();
            PauseManager.Current.StopPause();
            ServiceLocator.Current.Get<ISceneManager>().RestartScene();
        }

        public void BackToMenu()
        {
            _onSubmit?.Invoke();
            ResourcesSaveSystem.IncrementResourceAmount(Resource.Coin, _earnAmount);
            PauseManager.Current.StopPause();
            ServiceLocator.Current.Get<ISceneManager>().LoadScene(1);
        }
    }


    public struct FinishLevel
    {
        public bool IsWin;
        public int EarnedCoins;
        public Action OnSubmit;
    }
}

using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.Race
{
    public class RaceCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterFont;
        
        
        public void StartCount(Action onFinishCounter, int counterTime)
        {
            _counterFont.gameObject.SetActive(true);
            StartCoroutine(Count(onFinishCounter, counterTime));
        }

        private IEnumerator Count(Action onFinish, int totalTime)
        {
            _counterFont.text = totalTime.ToString();
            while (totalTime > 0)
            {
                yield return new WaitForSeconds(1f);
                totalTime -= 1;
                _counterFont.text = totalTime.ToString();
            }
            
            _counterFont.gameObject.SetActive(false);
            onFinish?.Invoke();
        }
    }
}

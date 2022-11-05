using System;
using _Project.Scripts.Core.SignalBus;
using UnityEngine;

namespace _Project.Scripts.GUi.Race
{
    public class RaceUiHandler : MonoBehaviour
    {
        [SerializeField] private RaceCounter _counter;

        [Sub]
        private void OnEnableCounter(EnableCounter reference)
        {
            _counter.StartCount(reference.OnFinish, reference.Duration);
        }
    }

    public struct EnableCounter
    {
        public Action OnFinish;
        public int Duration;
    }
}

using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Development
{
    public static class AnimationUtilities
    {
        public static IEnumerator WaitForAction(Action action, float waitingTime)
        {
            yield return new WaitForSeconds(waitingTime);
            action?.Invoke();
        }
    }
}

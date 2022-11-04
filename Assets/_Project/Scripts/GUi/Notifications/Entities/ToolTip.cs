using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.Notifications.Entities
{
    public class ToolTip : NotificationEntity
    {
        [SerializeField] private float _initialY;
        [SerializeField] private TextMeshProUGUI _toolTipText;
        
        public override NotificationType Notification => NotificationType.ToolTip;
        public override void ShowNotification(string description)
        {
            _toolTipText.text = description;
            _rect.DOMoveY(0f, _animationTime).OnComplete(() => StartCoroutine(WaitingSomeTime()));
        }

        private IEnumerator WaitingSomeTime()
        {
            yield return new WaitForSeconds(_animationTime);
            _rect.DOMoveY(_initialY, _animationTime).OnComplete(Disable);
        }

        public override void DisableNotification()
        {
            
        }
    }
}

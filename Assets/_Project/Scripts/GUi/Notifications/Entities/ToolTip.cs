using System.Collections;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.Notifications.Entities
{
    public class ToolTip : NotificationEntity
    {
        [SerializeField] private TextMeshProUGUI _toolTipText;
        
        public override NotificationType Notification => NotificationType.ToolTip;
        public override void ShowNotification(string description)
        {
            _toolTipText.text = description;
            StartCoroutine(WaitingSomeTime());
        }

        private IEnumerator WaitingSomeTime()
        {
            yield return new WaitForSeconds(_animationTime);
            gameObject.SetActive(false);
        }

        public override void DisableNotification()
        {
            
        }
    }
}

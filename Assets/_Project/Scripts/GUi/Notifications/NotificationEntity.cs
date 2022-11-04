using System;
using _Project.Scripts.GUi.Utilities;
using UnityEngine;

namespace _Project.Scripts.GUi.Notifications
{
    public abstract class NotificationEntity : View
    {
        [SerializeField] protected RectTransform _rect;
        [SerializeField] protected float _animationTime;

        public abstract NotificationType Notification { get; }
        
        public abstract void ShowNotification(string description);
        public abstract void DisableNotification();
    }
    
    
    [Serializable] public enum NotificationType { Question, ToolTip }
}

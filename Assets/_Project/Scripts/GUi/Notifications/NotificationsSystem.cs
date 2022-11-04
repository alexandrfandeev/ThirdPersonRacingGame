using System.Collections.Generic;
using _Project.Scripts.Core.LocatorServices;
using UnityEngine;

namespace _Project.Scripts.GUi.Notifications
{
    public class NotificationsSystem : MonoBehaviour, IService, INotification
    {
        [SerializeField] private  List<NotificationEntity> _entities = new List<NotificationEntity>();


        public void InitializeService()
        {
            ServiceLocator.Current.Register<INotification>(this);
        }

        public void InitializeSystem()
        {
            _entities.AddRange(GetComponentsInChildren<NotificationEntity>());
        }

        public void ShowNotification(NotificationType type, string description)
        {
            NotificationEntity notification =  _entities.Find(x => x.Notification == type);
            notification.ShowNotification(description);
        }

        public void DisableNotification(NotificationType type)
        {
            NotificationEntity notification =  _entities.Find(x => x.Notification == type);
            notification.DisableNotification();
        }
    }
    
    public interface INotification : IGameService
    {
        public void InitializeSystem();
        public void ShowNotification(NotificationType type, string description);
        public void DisableNotification(NotificationType type);
    }
}

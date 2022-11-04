using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.Notifications;
using _Project.Scripts.GUi.User;
using _Project.Scripts.SaveSystem;
using UnityEngine;

namespace _Project.Scripts.User
{
    public class UserDataController : MonoBehaviour, IService, IUserData
    {
        public void InitializeService()
        {
            ServiceLocator.Current.Register<IUserData>(this);
        }

        public void InitializeSystem()
        {
            if (UserDataSaveSystem.GetUserStatus() == 0) CreateUserData();
            else InitializeUserData();
        }

        private void CreateUserData()
        {
            Signal.Current.Fire<CreateUser>(new CreateUser
            {
                OnCreate = UserDataSaveSystem.SetUserName
            });
        }

        private void InitializeUserData()
        {
            string welcomeText = "Welcome back, " + UserDataSaveSystem.GetUserName();
            ServiceLocator.Current.Get<INotification>().ShowNotification(NotificationType.ToolTip, welcomeText);
        }
    }

    public interface IUserData : IGameService
    {
        public void InitializeSystem();
    }
}



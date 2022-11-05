using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.Navigation;
using _Project.Scripts.GUi.Notifications;
using _Project.Scripts.GUi.User;
using _Project.Scripts.Resources;
using _Project.Scripts.SaveSystem;
using _Project.Scripts.SceneSystem;
using _Project.Scripts.VehicleController;
using UnityEngine;
using UnityEngine.Purchasing;

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
                OnCreate = UserDataSaveSystem.SetUserName,
                OnPress = SubmitUser
            });
        }

        private void SubmitUser()
        {
            ServiceLocator.Current.Get<IVehicleController>().InitializeSystem();
            Signal.Current.Fire<UserInfo>(new UserInfo
            {
                CoinsAmount = ResourcesSaveSystem.GetResourceAmount(Resource.Coin),
                Name = UserDataSaveSystem.GetUserName()
            });
            Signal.Current.Fire<MenuBarActivity>(new MenuBarActivity {Enable = true});
        }

        private void InitializeUserData()
        {
            string userName = UserDataSaveSystem.GetUserName();
            string welcomeText = "Welcome back, " + userName;
            ServiceLocator.Current.Get<IVehicleController>().InitializeSystem();
            Signal.Current.Fire<UserInfo>(new UserInfo
                { CoinsAmount = ResourcesSaveSystem.GetResourceAmount(Resource.Coin), Name = userName });
            Signal.Current.Fire<MenuBarActivity>(new MenuBarActivity { Enable = true });
            if (ServiceLocator.Current.Get<ISceneManager>().Scene == 1)
                ServiceLocator.Current.Get<INotification>().ShowNotification(NotificationType.ToolTip, welcomeText);
        }
    }

    public interface IUserData : IGameService
    {
        public void InitializeSystem();
    }
}



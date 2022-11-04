using System;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.User.Entities;
using UnityEngine;

namespace _Project.Scripts.GUi.User
{
    public class UserUiHandler : MonoBehaviour
    {
        [SerializeField] private UserDataUi _dataUI;
        [SerializeField] private UserCreationTab _creationTab;


        private void Awake()
        {
            _dataUI.Disable();
        }

        [Sub]
        private void OnCreateUser(CreateUser reference)
        {
            _creationTab.Open(reference.OnCreate, reference.OnPress);
        }

        [Sub]
        private void OnUpdateUserData(UserInfo reference)
        {
            if (reference.Update)
            {
                _dataUI.UpdateCoins(reference.CoinsAmount);
            }

            else
            {
                _dataUI.Initialize(reference.CoinsAmount, reference.Name);   
            }
        }
    }


    public struct CreateUser
    {
        public Action<string> OnCreate;
        public Action OnPress;
    }

    public struct UserInfo
    {
        public string Name;
        public bool Update;
        public int CoinsAmount;
    }
}

using System;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.GUi.User.Entities;
using UnityEngine;

namespace _Project.Scripts.GUi.User
{
    public class UserUiHandler : MonoBehaviour
    {
        [SerializeField] private UserCreationTab _creationTab;

        [Sub]
        private void OnCreateUser(CreateUser reference)
        {
            _creationTab.Open(reference.OnCreate);
        }
    }


    public struct CreateUser
    {
        public Action<string> OnCreate;
    }
}

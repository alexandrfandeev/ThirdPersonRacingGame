using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace _Project.Scripts.Ads
{
    public class UnityServicesInitializer : MonoBehaviour
    {
        public string environment = "production";

        private async void Awake()
        {
            try
            {
                InitializationOptions options = new InitializationOptions().SetEnvironmentName(environment);
                await UnityServices.InitializeAsync(options);
            }
            catch (Exception exception)
            {
                // An error occurred during services initialization.
                Debug.Log(exception);
            }
            
        }
    }
}

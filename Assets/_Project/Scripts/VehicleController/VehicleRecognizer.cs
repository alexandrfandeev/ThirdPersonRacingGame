using System;
using UnityEngine;

namespace _Project.Scripts.VehicleController
{
    public class VehicleRecognizer : MonoBehaviour
    {
        public VehicleType Vehicle;
    }
    
    [Serializable] public enum VehicleType { Player, Bot }
}

using System;
using UnityEngine;

namespace _Project.vehicle
{
    public class VehicleInputs : MonoBehaviour
    {
        public float vertical;
        public float horizontal;
        public bool handbrake;
        public bool boosting;

        private bool _inputLock = true;

        public void LockInputs(bool isLocked)
        {
            _inputLock = isLocked;
        }


        void Update()
        {
            keyboard();
        }

        public void keyboard()
        {
            if (!_inputLock)
            {
                handbrake = (Input.GetAxis("Jump") != 0) ? true : false;
            }

            else
            {
                handbrake = true;
            }
            
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical"); 
            boosting = true;
        }



    }
}

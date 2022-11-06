using System.Collections;
using _Project.Scripts.Core;
using _Project.Scripts.Development;
using _Project.Scripts.GUi.Race;
using Sirenix.Utilities;
using UnityEngine;

namespace _Project.Scripts.VehicleController.Entities
{
    [RequireComponent(typeof(VehicleWheelsManager))]
    [RequireComponent(typeof(VehicleInputs))]
    public class CarController : MonoBehaviour
    {
        [Header("Managers")] 
        [SerializeField] private VehicleInputs _inputManager;
        [SerializeField] private VehicleWheelsManager _wheelsManager;
        [SerializeField] private VehicleUtilities _utilities;

        [Header("Components")] 
        [SerializeField] private Transform _centerOfMass;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private WheelCollider[] _wheels;
        
        [Header("Power Curve")] 
        public AnimationCurve enginePower;

        [Header("Variables")] 
        public float maxRPM;
        [Range(1.5f, 4)] public float finalDrive;
        public float[] gears;
        [Range(5, 20)] public float DownForceValue;
        [Range(0.01f, 0.02f)] public float dragAmount;
        [Range(0, 1)] public float EngineSmoothTime = 0.2f;


        [HideInInspector] public float ForwardStifness;
        [HideInInspector] public float SidewaysStifness;
        [HideInInspector] public float KPH;
        
        private WheelFrictionCurve _forwardFriction, _sidewaysFriction;

        private int _gearNum = 1;
        private float _engineRpm;
        private float _totalPower;
        private float[] _wheelSlip;
        private float _finalTurnAngle;
        private float _radius = 4;
        private float _wheelsRpm;
        private float _horizontal;
        private float _acceleration;
        private float _vertical;
        private float _downforce;
        private float _gearChangeRate;
        private float _breakPower;
        private float _engineLerpValue;
        private float _engineLoad = 1;

        private bool _reverse;
        private bool _engineLerp;
        
        public void Initialize()
        {
            _wheelSlip = new float[_wheels.Length];
            _rigidbody.centerOfMass = _centerOfMass.localPosition;
            _utilities.Initialize();
            _inputManager.LockInputs(true);
            _wheelsManager.Initialize(_inputManager.Axis.Vertical, _wheels);
            StartCoroutine(AnimationUtilities.WaitForAction(() => StartCoroutine(Drive()), 1f));
        }

        public void StartRace()
        {
            _inputManager.LockInputs(false);
        }

        private IEnumerator Drive()
        {
            while (true)
            {
                UIUpdatesContainer.Current.UpdateFloat(_rigidbody.velocity.magnitude);
                _wheelsManager.HandleWheels();
                AddDownForce();
                SteerVehicle();
                CalculateEnginePower();
                Friction();
                ManualDrive();
                yield return null;
            }
            // ReSharper disable once IteratorNeverReturns
        }



        private void CalculateEnginePower()
        {
            LerpEngine();
            WheelRpm();

            _acceleration = _vertical > 0 ? _vertical : _wheelsRpm <= 1 ? _vertical : 0;

            if (!IsGrounded())
            {
                _acceleration = _engineRpm > 1000 ? _acceleration / 2 : _acceleration;
            }


            if (_engineRpm >= maxRPM)
            {
                SetEngineLerp(maxRPM - 1000);
            }

            if (!_engineLerp)
            {
                _engineRpm = Mathf.Lerp(_engineRpm, 1000f + Mathf.Abs(_wheelsRpm) * finalDrive * (gears[_gearNum]),
                    (EngineSmoothTime * 10) * Time.deltaTime);
                _totalPower = enginePower.Evaluate(_engineRpm) * (gears[_gearNum] * finalDrive) * _acceleration;
            }


            _engineLoad = Mathf.Lerp(_engineLoad, _vertical - ((_engineRpm - 1000) / maxRPM),
                (EngineSmoothTime * 10) * Time.deltaTime);

            MoveVehicle();
        }

        private void WheelRpm()
        {
            float sum = 0;
            int R = 0;
            for (int i = 0; i < 4; i++)
            {
                sum += _wheels[i].rpm;
                R++;
            }

            _wheelsRpm = (R != 0) ? sum / R : 0;

            if (_wheelsRpm < 0 && !_reverse)
            {
                _reverse = true;
            }
            else if (_wheelsRpm > 0 && _reverse)
            {
                _reverse = false;
            }
        }

        private void ManualDrive()
        {
            if (_inputManager.Shift.IsDown && _gearNum + 1 < gears.Length && Time.time >= _gearChangeRate)
            { 
                _gearNum += 1;
                UIUpdatesContainer.Current.UpdateInt(_gearNum);
                _gearChangeRate = Time.time + 1f / 3f;
                SetEngineLerp(_engineRpm - (_engineRpm > 1500 ? 1000 : 700));
            }

            if (_inputManager.Control.IsDown && _gearNum >= 1 && Time.time >= _gearChangeRate)
            {
                _gearChangeRate = Time.time + 1f / 3f;
                _gearNum--;
                UIUpdatesContainer.Current.UpdateInt(_gearNum);
                SetEngineLerp(_engineRpm - (_engineRpm > 1500 ? 1000 : 700));
            }
        }

        private bool IsGrounded()
        {
            return _wheels[0].isGrounded && _wheels[1].isGrounded && _wheels[2].isGrounded && _wheels[3].isGrounded;
        }

        private void MoveVehicle()
        {
            _wheels.ForEach(x => x.motorTorque = (_vertical == 0) ? 0 : _totalPower / _wheels.Length);
            
            for (int i = 0; i < _wheels.Length; i++)
            {
                if (KPH <= 1 && KPH >= -1 && _vertical == 0)
                {
                    _breakPower = 5;
                }
                else
                {
                    if (_vertical < 0 && KPH > 1 && !_reverse)
                        _breakPower = (_wheelSlip[i] <= 1) ? _breakPower + -_vertical * 20 :
                            _breakPower > 0 ? _breakPower + _vertical * 20 : 0;
                    else _breakPower = 0;
                }

                _wheels[i].brakeTorque = _breakPower;
            }

            _wheels[2].brakeTorque = _wheels[3].brakeTorque = _inputManager.Space.IsDown || _inputManager.IsLocked ? 999f : 0f;

            _rigidbody.angularDrag = (KPH > 100) ? KPH / 100 : 0;
            _rigidbody.drag = dragAmount + (KPH / 40000);

            KPH = _rigidbody.velocity.magnitude * 3.6f;
        }

        private void SteerVehicle()
        {
            _vertical = _inputManager.Axis.Vertical;
            _horizontal = Mathf.Lerp(_horizontal, _inputManager.Axis.Horizontal,
                (_inputManager.Axis.Horizontal != 0) ? 5 * Time.deltaTime : 5 * 2 * Time.deltaTime);

            _finalTurnAngle = (_radius > 5) ? _radius : 5;

            if (_horizontal > 0)
            {
                _wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (_finalTurnAngle - (1.5f / 2))) * _horizontal;
                _wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (_finalTurnAngle + (1.5f / 2))) * _horizontal;
            }
            else if (_horizontal < 0)
            {
                _wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (_finalTurnAngle + (1.5f / 2))) * _horizontal;
                _wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (_finalTurnAngle - (1.5f / 2))) * _horizontal;

            }
            else
            {
                _wheels[0].steerAngle = 0;
                _wheels[1].steerAngle = 0;
            }

        }

        public void ChangeColor(Color color)
        {
            _utilities.ChangeColor(color);
        }

        private void AddDownForce()
        {
            _downforce = Mathf.Abs(DownForceValue * _rigidbody.velocity.magnitude);
            _downforce = KPH > 60 ? _downforce : 0;
            _rigidbody.AddForce(-transform.up * _downforce);
        }

        private void Friction()
        {
            WheelHit hit;
            float sum = 0;
            float[] sidewaysSlip = new float[_wheels.Length];
            for (int i = 0; i < _wheels.Length; i++)
            {
                if (_wheels[i].GetGroundHit(out hit) && i >= 2)
                {
                    _forwardFriction = _wheels[i].forwardFriction;
                    _forwardFriction.stiffness =
                        _inputManager.Space.IsDown || _inputManager.IsLocked ? .55f : ForwardStifness;
                    _wheels[i].forwardFriction = _forwardFriction;

                    _sidewaysFriction = _wheels[i].sidewaysFriction;
                    _sidewaysFriction.stiffness =
                        _inputManager.Space.IsDown || _inputManager.IsLocked ? .55f : SidewaysStifness;
                    _wheels[i].sidewaysFriction = _sidewaysFriction;


                    sum += Mathf.Abs(hit.sidewaysSlip);

                }

                _wheelSlip[i] = Mathf.Abs(hit.forwardSlip) + Mathf.Abs(hit.sidewaysSlip);
                sidewaysSlip[i] = Mathf.Abs(hit.sidewaysSlip);


            }

            sum /= _wheels.Length - 2;
            _radius = (KPH > 60) ? 4 + (sum * -25) + KPH / 8 : 4;

        }

        private void SetEngineLerp(float num)
        {
            _engineLerp = true;
            _engineLerpValue = num;
        }

        private void LerpEngine()
        {
            if (_engineLerp)
            {
                _totalPower = 0;
                _engineRpm = Mathf.Lerp(_engineRpm, _engineLerpValue, (EngineSmoothTime * 10) * Time.deltaTime);
                _engineLerp = !(_engineRpm <= _engineLerpValue + 100);
            }
        }
        private void OnGUISelected()
        {
     Debug.Log("ss");
            // foreach (float item in _wheelSlip)
            // {
            //     s += item.ToString("0.0") + " ";
            // }
            //
            // float pos = 50;
            //
            // GUI.Label(new Rect(20, pos, 200, 20), "currentGear: " + _gearNum.ToString("0"));
            // pos += 25f;
            // GUI.HorizontalSlider(new Rect(20, pos, 200, 20), _engineRpm, 0, maxRPM);
            // pos += 25f;
            // GUI.Label(new Rect(20, pos, 200, 20), "wheel slip: " + s);
            // pos += 25f;
            // GUI.Label(new Rect(20, pos, 200, 20), "Torque: " + _totalPower.ToString("0"));
            // pos += 25f;
            // GUI.Label(new Rect(20, pos, 200, 20), "KPH: " + KPH.ToString("0"));
            // pos += 25f;
            // GUI.HorizontalSlider(new Rect(20, pos, 200, 20), _engineLoad, 0.0F, 1.0F);
            // pos += 25f;
            // GUI.Label(new Rect(20, pos, 200, 20), "brakes: " + _breakPower.ToString("0"));
            // pos += 25f;
        }
    }
}
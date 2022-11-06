using System.Collections;
using UnityEngine;

namespace _Project.Scripts.VehicleController.Entities
{
    [RequireComponent(typeof(VehicleWheelsManager)) ]
    [RequireComponent(typeof(VehicleInputs)) ]
    public class CarController : MonoBehaviour{
    
        internal enum driveType{
            frontWheelDrive,
            rearWheelDrive,
            allWheelDrive
        }
        [SerializeField]private driveType drive;

        //scripts ->
        private VehicleInputs IM;
        private VehicleWheelsManager wheelsmanager;

        //components
        private WheelFrictionCurve  forwardFriction,sidewaysFriction;
        private new Rigidbody rigidbody;
        private WheelCollider[] wheels ;
        private GameObject centerOfMass;
        [Header("Power Curve")]
        public AnimationCurve enginePower;


        //  public Material brakeLights;

        [Header("Variables")]
        public bool isAutomatic;
        public float maxRPM , minRPM;
        [Range(1.5f,4)]public float finalDrive ;
        public float[] gears;
        [Range(5,20)]public float DownForceValue ;
        [Range(0.01f,0.02f)]public float dragAmount ;
        [Range (0,1)] public float EngineSmoothTime = 0.2f ;


        [HideInInspector]public float ForwardStifness;
        [HideInInspector]public float SidewaysStifness;
        [HideInInspector]public float KPH;

        private VehicleUtilities _utilities;

        private int gearNum = 1;
        private float engineRPM;
        private float totalPower;
        private float[] wheelSlip;
        private float finalTurnAngle;
        private float radius  = 4;
        private float wheelsRPM  ;
        private float horizontal ;
        private float acceleration;
        private float vertical ;
        private float downforce ;
        private float gearChangeRate;
        private float brakPower;
        private float engineLerpValue;
        private float engineLoad = 1;

        private bool reverse = false;
        private bool lightsFlag ;
        private bool engineLerp ;

        private IEnumerator Start() {
            GetObjects();
            wheelsmanager.Initialize(IM.Axis.Vertical);
            yield return new WaitForSeconds(1f);
            StartCoroutine(Drive());
        }

        public void StartRace()
        {
            IM.LockInputs(false);
        }

        private IEnumerator Drive()
        {
            while (true)
            {
                wheelsmanager.HandleWheels();
                AddDownForce();
                SteerVehicle();
                calculateEnginePower();
                Friction();
                if(isAutomatic)
                    shifter();
                else
                    Manual();
                yield return null;
            }
        }

        void FixedUpdate(){
            ActivateLights();    
        }

    

        private void ActivateLights() {
            if (IM.Axis.Vertical < 0 || KPH <= 1) turnLightsOn();
            else turnLightsOff();
        }
   
        private void turnLightsOn(){
            if (lightsFlag) return;
            //brakeLights.SetColor("_EmissionColor", new Color(255f,35f,35f) * 0.115f);
            lightsFlag = true;
        }    
   
        private void turnLightsOff(){
            if (!lightsFlag) return;
            //  brakeLights.SetColor("_EmissionColor", Color.black);
            lightsFlag = false;
        }

        private void calculateEnginePower(){
            lerpEngine();
            wheelRPM();

            acceleration = vertical > 0 ?  vertical : wheelsRPM <= 1 ? vertical  : 0 ;
        
            if(!isGrounded()){
                acceleration = engineRPM > 1000 ? acceleration / 2 : acceleration; 
            }


            if(engineRPM >= maxRPM){
                setEngineLerp(maxRPM - 1000);
            }
            if(!engineLerp){
                engineRPM = Mathf.Lerp(engineRPM,1000f + Mathf.Abs(wheelsRPM) *  finalDrive *  (gears[gearNum]) , (EngineSmoothTime * 10) * Time.deltaTime);
                totalPower = enginePower.Evaluate(engineRPM) * (gears[gearNum] * finalDrive ) * acceleration  ;
            }
        
        
            engineLoad = Mathf.Lerp(engineLoad,vertical - ((engineRPM - 1000) / maxRPM ),(EngineSmoothTime * 10) * Time.deltaTime);

            moveVehicle();
        }

        private void wheelRPM(){
            float sum = 0;
            int R = 0;
            for (int i = 0; i < 4; i++)
            {
                sum += wheels[i].rpm;
                R++;
            }
            wheelsRPM = (R != 0) ? sum / R : 0;
 
            if(wheelsRPM < 0 && !reverse ){
                reverse = true;
                //if (gameObject.tag != "AI") manager.changeGear();
            }
            else if(wheelsRPM > 0 && reverse){
                reverse = false;
                //if (gameObject.tag != "AI") manager.changeGear();
            }
        }

        private void Manual(){

            if (IM.Shift.IsDown && gearNum <= gears.Length && Time.time >= gearChangeRate)
            {
                gearNum  = gearNum +1;
                gearChangeRate = Time.time + 1f/3f ;
                setEngineLerp(engineRPM - ( engineRPM > 1500 ? 1000 : 700));
            }

            if (IM.Control.IsDown && gearNum >= 1 && Time.time >= gearChangeRate)
            {
                gearChangeRate = Time.time + 1f/3f ;
                gearNum --;
                setEngineLerp(engineRPM - ( engineRPM > 1500 ? 1000 : 700));
            }
        }

        private void shifter(){
            if(!isGrounded())return;

            if(engineRPM > maxRPM  && gearNum < gears.Length-1 && !reverse && Time.time >= gearChangeRate  && KPH >55){
                gearNum ++;
                setEngineLerp(engineRPM - (engineRPM / 3));
                gearChangeRate = Time.time + 1f/1f ;
            }
            if(engineRPM < minRPM && gearNum > 0 && Time.time >= gearChangeRate){
                gearChangeRate = Time.time + 0.15f ;
                setEngineLerp(engineRPM + (engineRPM / 2));
                gearNum --;
            }

        }
 
        public bool isGrounded(){
            if(wheels[0].isGrounded &&wheels[1].isGrounded &&wheels[2].isGrounded &&wheels[3].isGrounded )
                return true;
            else
                return false;
        }

        private void moveVehicle(){
            if(drive == driveType.rearWheelDrive){
                for (int i = 2; i < wheels.Length; i++){
                    wheels[i].motorTorque = (vertical == 0) ? 0 : totalPower / (wheels.Length - 2) ;
                }
            }
            else if(drive == driveType.frontWheelDrive){
                for (int i = 0; i < wheels.Length - 2; i++){
                    wheels[i].motorTorque =  (vertical == 0) ? 0 : totalPower / (wheels.Length - 2) ;
                }
            }
            else{
                for (int i = 0; i < wheels.Length; i++){
                    wheels[i].motorTorque =  (vertical == 0) ? 0 : totalPower / wheels.Length;
                }
            }


            for (int i = 0; i < wheels.Length; i++){
                if(KPH <= 1 && KPH >= -1 && vertical == 0){
                    brakPower = 5;
                } else{
                    if(vertical < 0 && KPH > 1 && !reverse)
                        brakPower =  (wheelSlip[i] <= 1) ? brakPower + -vertical * 20 : brakPower > 0 ? brakPower  + vertical * 20 : 0 ;
                    else 
                        brakPower = 0;
                }
                wheels[i].brakeTorque = brakPower;
            }
            
            wheels[2].brakeTorque = wheels[3].brakeTorque = IM.Space.IsDown || IM.IsLocked ? 999f : 0f;

            rigidbody.angularDrag = (KPH > 100)? KPH / 100 : 0;
            rigidbody.drag = dragAmount + (KPH / 40000) ;

            KPH = rigidbody.velocity.magnitude * 3.6f;

        }

        private void SteerVehicle(){

            vertical = IM.Axis.Vertical;
            horizontal = Mathf.Lerp(horizontal , IM.Axis.Horizontal , (IM.Axis.Horizontal != 0) ? 5 * Time.deltaTime : 5 * 2 * Time.deltaTime);

            finalTurnAngle = (radius > 5 ) ? radius : 5  ;

            if (horizontal > 0 ) {
                //rear tracks size is set to 1.5f       wheel base has been set to 2.55f
                wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (finalTurnAngle - (1.5f / 2))) * horizontal;
                wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (finalTurnAngle + (1.5f / 2))) * horizontal;
            } else if (horizontal < 0 ) {                                                          
                wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (finalTurnAngle + (1.5f / 2))) * horizontal;
                wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (finalTurnAngle - (1.5f / 2))) * horizontal;
                //transform.Rotate(Vector3.up * steerHelping);

            } else {
                wheels[0].steerAngle =0;
                wheels[1].steerAngle =0;
            }

        }
   
        private void GetObjects()
        {
            _utilities = GetComponentInChildren<VehicleUtilities>();
            IM = GetComponent<VehicleInputs>();
            rigidbody = GetComponent<Rigidbody>();
            wheelsmanager = GetComponent<VehicleWheelsManager>();
            wheels = wheelsmanager.Colliders;
            wheelSlip = new float[wheels.Length];
            rigidbody.centerOfMass = gameObject.transform.Find("centerOfMas").gameObject.transform.localPosition;
            _utilities.Initialize();
            IM.LockInputs(true);
        }

        public void ChangeColor(Color color)
        {
            _utilities.ChangeColor(color);
        }

        private void AddDownForce()
        {
            downforce = Mathf.Abs(DownForceValue * rigidbody.velocity.magnitude);
            downforce = KPH > 60 ? downforce : 0;
            rigidbody.AddForce(-transform.up * downforce);
        }

        private void Friction(){
    
            WheelHit hit;
            float sum = 0;
            float[] sidewaysSlip = new float[wheels.Length];
            for (int i = 0; i < wheels.Length ; i++){
                if(wheels[i].GetGroundHit(out hit) && i >= 2 ){
                    forwardFriction = wheels[i].forwardFriction; 
                    forwardFriction.stiffness =  IM.Space.IsDown || IM.IsLocked ?  .55f : ForwardStifness;
                    wheels[i].forwardFriction = forwardFriction;

                    sidewaysFriction = wheels[i].sidewaysFriction; 
                    sidewaysFriction.stiffness =  IM.Space.IsDown || IM.IsLocked ? .55f : SidewaysStifness;
                    wheels[i].sidewaysFriction = sidewaysFriction;
                    

                    sum += Mathf.Abs(hit.sidewaysSlip);

                }

                wheelSlip[i] = Mathf.Abs( hit.forwardSlip ) + Mathf.Abs(hit.sidewaysSlip) ;
                sidewaysSlip[i] = Mathf.Abs(hit.sidewaysSlip);


            }

            sum /= wheels.Length - 2 ;
            radius = (KPH > 60) ?  4 + (sum * -25) + KPH / 8 : 4;
        
        }
   
        private void setEngineLerp(float num){
            engineLerp = true;
            engineLerpValue = num;
        }

        public void lerpEngine(){
            if(engineLerp){
                totalPower = 0;
                engineRPM = Mathf.Lerp(engineRPM,engineLerpValue,(EngineSmoothTime* 10) * Time.deltaTime );
                engineLerp = engineRPM <= engineLerpValue + 100 ? false : true;
            }
        }   

        private string s ;
 
        void OnGUI(){
            s = "";
            foreach (float item in wheelSlip){
                s +=  item.ToString("0.0") + " ";
            }
            float pos = 50;

            GUI.Label(new Rect(20, pos, 200, 20),"currentGear: " + gearNum.ToString("0"));
            pos+=25f;
            GUI.HorizontalSlider(new Rect(20, pos, 200, 20), engineRPM,0,maxRPM);
            pos+=25f;
            GUI.Label(new Rect(20, pos, 200, 20),"wheel slip: " + s);
            pos+=25f;
            GUI.Label(new Rect(20, pos, 200, 20),"Torque: " + totalPower.ToString("0"));
            pos+=25f;
            GUI.Label(new Rect(20, pos, 200, 20),"KPH: " + KPH.ToString("0"));
            pos+=25f;
            GUI.HorizontalSlider(new Rect(20, pos, 200, 20), engineLoad, 0.0F, 1.0F);
            pos+=25f;
            GUI.Label(new Rect(20, pos, 200, 20),"brakes: " + brakPower.ToString("0"));
            pos+=25f;

        

        
        }
    }
}
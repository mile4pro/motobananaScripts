using UnityEngine;



public class BananaMotorManager : MonoBehaviour {

    public float    enginePower = 3f,
                    enginePowerChange = 3f,
                    maxSpeed = 3f;

    public float    maxAngularSpeed = 150f,
                    maxAngularSpeedChange = 150,
                    angularTurnRightFactor = 0.85f;

    public float    inertia = 15f,
                    inertiaChange = 15f,
                    inertiaMin = 2.5f,
                    inertiaMax = 17.5f;

    public float    skill = 0.15f,
                    skillChange = 0.15f,
                    skillMin = 0.49f,
                    skillMax = 0.01f;

    public bool     enginePowerChangeFL,
                    inertiaChangeFL,
                    skillChangeFL,
                    maxAngularSpeedChangeFL,
                    endRaceFL;

    float           changeFactorTime = 1f,
                    changeTime;



    public BananaMotorManager(float _engPwr, float _angSpd, float _skillMin, float _skillMax){
        enginePowerChange = _engPwr;
        maxSpeed = _engPwr;
        maxAngularSpeedChange = _angSpd;
        skillMin = _skillMin;
        skillMax = _skillMax;
        enginePowerChangeFL = true;
        maxAngularSpeedChangeFL = true;
    }



    public bool checkSkill(){

        countChangeTime();

        if (skill - skillChange > changeTime) {
            skill -= changeTime;
            return true;
        }
        else if (skill - skillChange < -changeTime){
            skill += changeTime;
            return true;
        }
        else {
            skill = skillChange;
            return false;
        }
    }



    public bool checkInertia(){

        countChangeTime();

        if (inertia - inertiaChange > changeTime) {
            inertia -= changeTime;
            return true;
        }
        else if (inertia - inertiaChange < -changeTime){
            inertia += changeTime;
            return true;
        }
        else {
            inertia = inertiaChange;
            return false;
        }
    }



    public bool checkEnginePower(){

        countChangeTime();

        if (enginePower - enginePowerChange > changeTime) {
            enginePower -= changeTime;
            maxSpeed = enginePower;
            return true;
        }
        else if (enginePower - enginePowerChange < -changeTime){
            enginePower += changeTime;
            maxSpeed = enginePower;
            return true;
        }
        else {
            enginePower = enginePowerChange;
            maxSpeed = enginePower;
            return false;
        }
    }



    public bool checkMaxAngularSpeed(){

        countChangeTime();

        if (maxAngularSpeed - maxAngularSpeedChange > changeTime) {
            maxAngularSpeed -= changeTime;
            return true;
        }
        else if (maxAngularSpeed - maxAngularSpeedChange < -changeTime){
            maxAngularSpeed += changeTime;
            return true;
        }
        else {
            maxAngularSpeed = maxAngularSpeedChange;
            return false;
        }
    }



    void countChangeTime(){

        changeTime = changeFactorTime * Time.deltaTime;
    }



    public void randomInertia(){

        inertia = (Random.value * (inertiaMax - inertiaMin)) + inertiaMin;
        inertiaChangeFL = true;
    }


    public void randomSkill(){

        skill = (Random.value * (skillMax - skillMin) + skillMin);
    }




    public void checkMotorChanges(){

        if(enginePowerChangeFL){
            enginePowerChangeFL = checkEnginePower();
        }

        if(inertiaChangeFL){
            inertiaChangeFL = checkInertia();
        }

        if(skillChangeFL){
            skillChangeFL = checkSkill();
        }

        if(maxAngularSpeedChangeFL){
            maxAngularSpeedChangeFL = checkMaxAngularSpeed();
        }
    }



    public void randomEndRaceMotorValues(){

        float tmpRandom = Random.value;

        if (endRaceFL){
        enginePower = tmpRandom + 0.5f;
        maxSpeed = enginePower;
        }
		
        endRaceFL = true;
		
        inertiaMin = tmpRandom * 2.5f;
        inertiaMax = tmpRandom * 17.5f;
        skillMin = 0.75f;
        skillMax = 0.5f;
        maxAngularSpeed = (tmpRandom * 50f) + 75f;

        enginePowerChangeFL = true;
        inertiaChangeFL = true;
        skillChangeFL = true;
        maxAngularSpeedChangeFL = true;
    }



    public void setEndRaceFL(bool _FL){
        endRaceFL = _FL;
    }
    public bool getEndRaceFL(){
        return endRaceFL;
    }

}

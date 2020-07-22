using UnityEngine;



public class AudioMotorManager : MonoBehaviour {

    [SerializeField]
    AudioClip[]     soundsMotor;

    [SerializeField]
    AudioSource[]   valvesMotor;

    int     soundsMotorLenght, valvesMotorLenght,
            actualValveNr = 0;

    int[] randomIntArr = new int[4];

    float   timeOneTick = .06f, timeOneTickTmp, timeTmpDelta, timeTmp,
            defaultVolume = 0.3f, defaultVolumeEndRace = 0.075f;

    bool    endRaceFL, startEngineSoundFL;




    void Start(){

            soundsMotorLenght = soundsMotor.Length;
            valvesMotorLenght = valvesMotor.Length;
            randomSoundValveArr();
            loadValvesSounds();
    }


    void randomSoundValveArr(){
        float tmpSoundLenght = (float)soundsMotorLenght;
        for (int i = 0; i < randomIntArr.Length; i++){
            randomIntArr[i] = (int)Random.Range(0f, tmpSoundLenght - 0.001f);
            //Debug.Log("randomIntArr[" + i + "]: " + randomIntArr[i]);
        }
    }


    void loadValvesSounds(){
        for (int i = 0; i < valvesMotorLenght; i++){
            valvesMotor[i].clip = soundsMotor[randomIntArr[i]];
            valvesMotor[i].volume = defaultVolume;
            valvesMotor[i].minDistance = 0.5f;
            valvesMotor[i].maxDistance = 1.5f;
            valvesMotor[i].rolloffMode = AudioRolloffMode.Logarithmic;
            valvesMotor[i].spatialBlend = 0.4f;
        }
    }


    public bool checkAudioMotor(float _hwMn, float _motorEnginePower){

        timeOneTickTmp = Mathf.Max(0.001f, timeOneTick - (_hwMn*0.25f) - ((_motorEnginePower-2)*0.05f));
		
        timeTmp = Time.time;
        if (timeTmp - timeTmpDelta > timeOneTickTmp){
			
            valvesMotor[actualValveNr].Play();

            valvesMotor[actualValveNr].pitch = Mathf.Min(7.5f, Mathf.Max(2f, 1f + (7.5f*_hwMn))) * Mathf.Max(0.15f, Time.timeScale);

            valvesMotor[actualValveNr].volume = valvesMotor[actualValveNr].pitch / 7.5f;
            if (endRaceFL) {valvesMotor[actualValveNr].volume = valvesMotor[actualValveNr].volume * 0.2f;}

            actualValveNr += 1;
            if (actualValveNr > valvesMotorLenght-1) {actualValveNr = 0;}

            timeTmpDelta = timeTmp;

            return true;
        }

        return false;
    }


    public void setEndRaceAudioVolume(){
        endRaceFL = true;
    }



    public void audioMotorBeforeStartRace(float _motorEnginePower){

        if (Random.value < 0.0175f) {startEngineSoundFL = true;}
        if (startEngineSoundFL) {checkAudioMotor(-0.3f, _motorEnginePower); }
    }

}

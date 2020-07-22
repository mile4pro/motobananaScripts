using UnityEngine;

public class BananaRaceData : MonoBehaviour {


    float       timeStart, timeEnd, timeRace,
                timeLapStart, timeLapEnd, timeLastLap;

    int         hwMnLap = 2, actualNrLap = -1, actualPlace = -1;

    bool        endRaceFL, endRaceVisualEffectFL;



    public void setTimeLapStart(float _timeLapStart){
        timeLapStart = _timeLapStart;
    }

    public void setTimeLapEnd(float _timeLapEnd){
        timeLapEnd = _timeLapEnd;
    }

    public float getTimeLastLap(){
        timeLastLap = timeLapEnd - timeLapStart;
        //Debug.Log("time lap: " + timeLastLap);
        return timeLastLap;
    }


    public void setTimeStart(float _timeStart){
        timeStart = _timeStart;
        //Debug.Log("Time start: " + timeStart);
    }

    public void setTimeEnd(float _timeEnd){
        timeEnd = _timeEnd;
    }

    public float getTimeRace(){
        timeRace = timeEnd - timeStart;
        //Debug.Log("time RACE: " + timeRace);
        return timeRace;
    }

    public float getTimeRaceDoNotCount(){
        return timeRace;
    }


    public void addNrActualLap(){
        actualNrLap += 1;
    }

    public int getActualNrLap(){
        return actualNrLap;
    }


    public void setHwMnLap(int _hwMnLap){
        hwMnLap = _hwMnLap;
    }


    public void setActualPlace(int _actualPlace){
        actualPlace = _actualPlace;
    }


    public int getActualPlace(){
        return actualPlace;
    }



    public bool checkEndRace(){

        if (actualNrLap >= hwMnLap){
            endRaceFL = true;
        }
        else{
            endRaceFL = false;
        }

        return endRaceFL;
    }



    public bool getEndRaceFL(){
        return endRaceFL;
    }

    public bool getEndRaceVisualEffectFL(){
        return endRaceVisualEffectFL;
    }

    public void setEndRaceVisualEffectFL(bool _FL){
        endRaceVisualEffectFL = _FL;
    }


    public bool startLap(){

        float tmpTime = Time.time;

        /*if (actualNrLap < 0){
            setTimeStart(tmpTime);
        }
        else{
            setTimeLapEnd(tmpTime);
            getTimeLastLap();
        }

        addNrActualLap();*/

        addNrActualLap();
        if (actualNrLap>0){
            setTimeLapEnd(tmpTime);
            getTimeLastLap();
        }

        setTimeLapStart(tmpTime);

        if (actualNrLap >= hwMnLap){
            setTimeEnd(tmpTime);
            getTimeRace();
            Debug.Log("timeRace: " + timeRace);
            endRaceFL = true;
        }
        return endRaceFL;
    }

}

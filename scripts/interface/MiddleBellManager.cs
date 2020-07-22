using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class MiddleBellManager : MonoBehaviour {

    Animator    animator;

    [SerializeField]
    GameObject  textLapNrInfoActualObj, textLapNrInfoAllObj,
                textPositionInfoActualObj, textPositionInfoAllObj;

    [SerializeField]
    GameObject  timeRaceInfoMinuteObj, timeRaceInfoSecondObj, timeRaceInfoMilliObj,
                timeLapInfoMinuteObj, timeLapInfoSecondObj, timeLapInfoMilliObj;

    Text        textTimeRaceInfoMinute, textTimeRaceInfoSecond, textTimeRaceInfoMilli,
                textTimeLapInfoMinute, textTimeLapInfoSecond, textTimeLapInfoMilli;

    [SerializeField]
    GameObject  endRaceInterfaceObj, playerDataObj;

    PlayerData playerData;

    int         hwMnLap;
    float       timeStartRace, timeStartLap, timeRace, timeLap, bestTimeLap;
    bool        runTimersFL, updateTextTimerLapFL = true;
    //Text        textLapNrInfoActual, textLapNrInfoAll;

    [SerializeField]
    GameObject  lapNrInfo, positionNrInfo, raceTimeInfo, lapTimeInfo;



    void Update(){

        if(runTimersFL){
            updateTimers();
        }

    }



    void Start(){
        //textLapNrInfoActual = textLapNrInfoActualObj.GetComponent<Text>();
        //textLapNrInfoAll = textLapNrInfoAllObj.GetComponent<Text>();
        textTimeRaceInfoMinute = timeRaceInfoMinuteObj.GetComponent<Text>();
        textTimeRaceInfoSecond = timeRaceInfoSecondObj.GetComponent<Text>();
        textTimeRaceInfoMilli = timeRaceInfoMilliObj.GetComponent<Text>();
        textTimeLapInfoMinute = timeLapInfoMinuteObj.GetComponent<Text>();
        textTimeLapInfoSecond = timeLapInfoSecondObj.GetComponent<Text>();
        textTimeLapInfoMilli = timeLapInfoMilliObj.GetComponent<Text>();

        playerData = playerDataObj.GetComponent<PlayerData>();
    }


    public bool setAnimator(Animator _animator){
        animator = _animator;
        return true;
    }


//hw mn lap info
    public void setBeginTextHwMnLap(int _hwMn){
        //textLapNrInfoActual.text = "1";
        //textLapNrInfoAll.text = _hwMn.ToString();
        hwMnLap = _hwMn;
        textLapNrInfoActualObj.GetComponent<Text>().text = "1";
        textLapNrInfoAllObj.GetComponent<Text>().text = _hwMn.ToString();
    }

    public bool setTextWhichLap(int _hwMn){
        int tmpNrLap = _hwMn + 1;
        if (tmpNrLap < 1) {tmpNrLap = 1;}
        if (tmpNrLap > hwMnLap) {tmpNrLap = hwMnLap;}
        textLapNrInfoActualObj.GetComponent<Text>().text = tmpNrLap.ToString();
        return true;
    }




    public void setBeginTextPosition(int _hwMn, int _position){
        textPositionInfoActualObj.GetComponent<Text>().text = _position.ToString();
        textPositionInfoAllObj.GetComponent<Text>().text = _hwMn.ToString();
    }



//******************************************
//********************************
//info place in race checker
    class placeClass{
        public bool isPlayerFL = false;
        public int  valueInRace = -1, startingNr = -1;
        public placeClass(int _startingPlace, bool _isPlayerFL){
            startingNr = _startingPlace;
            isPlayerFL = _isPlayerFL;
        }
    }
    List<placeClass> placeList;
    public bool clearPlaceChecker(){
        placeList.Clear();
        return true;
    }
    public bool addToPlaceChecker(int _startingPlace, bool _isPlayerFL){
        placeList.Add(new placeClass(_startingPlace, _isPlayerFL));
        return true;
    }
    public int checkPlace(int _valueInRace, int _startingPlace, bool _isPlayerFL){

        int tmpReturnIndex;
        //Debug.Log("<<<... CHECK PLACE ...>>>");
        //Debug.Log("_valueInRace: " + _valueInRace + "_startingPlace: " + _startingPlace + "_isPlayerFL: " + _isPlayerFL);
        int tmpActualIndex = placeList.FindIndex(L => L.startingNr == _startingPlace);

        placeClass tmpPlaceListObj = placeList[tmpActualIndex];
        tmpPlaceListObj.valueInRace = _valueInRace;
        placeList.RemoveAt(tmpActualIndex);
        //Debug.Log("tmpActualIndex: " + tmpActualIndex);
        //Debug.Log("tmpActualIndex: " + tmpActualIndex);
        int tmpNewIndex = placeList.FindIndex(L => L.valueInRace < _valueInRace);
        //Debug.Log("tmpNewIndex: " + tmpNewIndex + "placeList[tmpNewIndex].startingNr: " + placeList[tmpNewIndex].startingNr);
        //Debug.Log("tmpNewIndex: " + tmpNewIndex);
        if(tmpNewIndex > -1 && tmpActualIndex > tmpNewIndex){

            /*placeList[tmpActualIndex].valueInRace = _valueInRace;
            placeList.Insert(tmpNewIndex, placeList[tmpActualIndex]);
            placeList.RemoveAt(tmpActualIndex+1);*/

            //placeClass tmpPlaceListObj = placeList[tmpActualIndex];
            //placeList.RemoveAt(tmpActualIndex);
            placeList.Insert(tmpNewIndex, tmpPlaceListObj);
            //placeList[tmpNewIndex].valueInRace = _valueInRace;
            tmpReturnIndex = tmpNewIndex;

        }
        else{
            placeList.Insert(tmpActualIndex, tmpPlaceListObj);
            //placeList[tmpActualIndex].valueInRace = _valueInRace;
            tmpNewIndex = tmpActualIndex;
        }

        textPositionInfoActualObj.GetComponent<Text>().text =  checkPlayerPlace().ToString();
        //Debug.Log("checkPlayerPlace(): " + checkPlayerPlace());
        //debugList();
        return tmpNewIndex;
    }
    public int checkPlayerPlace(){
        int tmpPlayerIndex = placeList.FindIndex(L => L.isPlayerFL == true);
        return (tmpPlayerIndex + 1);
    }
    public bool setPlaceList(GameObject[] _bananas){
        if (placeList != null) {clearPlaceChecker();}
        else {placeList = new List<placeClass>();}
        for (int i=0; i<_bananas.Length; i++){
            bool tmpIsPlayerFL = false;
            if (i == _bananas.Length-1) {tmpIsPlayerFL = true;}
            addToPlaceChecker(i, tmpIsPlayerFL);
        }
        //Debug.Log("placeList.Count:" + placeList.Count);
        //Debug.Log("............................................");
        /*foreach(placeClass s in placeList){
            Debug.Log("s.isPlayerFL: " + s.isPlayerFL);
            Debug.Log("s.valueInRace: " + s.valueInRace);
            Debug.Log("s.startingNr: " + s.startingNr);
        }*/
        //Debug.Log("............................................");
        return true;
    }

    void debugList(){
        Debug.Log("...**************************...");
        foreach(placeClass s in placeList){
            Debug.Log("s.isPlayerFL: " + s.isPlayerFL);
            Debug.Log("s.valueInRace: " + s.valueInRace);
            Debug.Log("s.startingNr: " + s.startingNr);
        }
        Debug.Log("...**************************...");
    }

//***************************************************************************
//******************************************************
//**********************************


//info timers

    void updateTimers(){

        float tmpTime = Time.time;

        timeRace = tmpTime - timeStartRace;
        timeLap = tmpTime - timeStartLap;

        updateTextTimer(timeRace, textTimeRaceInfoMinute, textTimeRaceInfoSecond, textTimeRaceInfoMilli);
        if (updateTextTimerLapFL){
            updateTextTimer(timeLap, textTimeLapInfoMinute, textTimeLapInfoSecond, textTimeLapInfoMilli);
        }
    }


    public bool setTimeStartRace (float _time){
        timeStartRace = _time;
        return true;
    }
    public bool setTimeStartLap (float _time){
        timeStartLap = _time;
        return true;
    }

    public bool resetTimerLap(){
        checkBestTimeLap();
        setTimeStartLap(Time.time);
        runAnimationPulseTimerLap();
        return true;
    }


    public bool checkBestTimeLap(){
        if (bestTimeLap > 0){
            if (timeLap < bestTimeLap){
                bestTimeLap = timeLap;
                return true;
            }
            else{
                return false;
            }
        }
        else{
            bestTimeLap = timeLap;
            return false;
        }
    }

    public bool resetBestTimeLap(){
        bestTimeLap = -1;
        return true;
    }


    public bool resetTimers(){
        resetBestTimeLap();
        updateTextTimer(0f, textTimeRaceInfoMinute, textTimeRaceInfoSecond, textTimeRaceInfoMilli);
        updateTextTimer(0f, textTimeLapInfoMinute, textTimeLapInfoSecond, textTimeLapInfoMilli);
        updateTextTimerLapFL = true;
        return true;
    }

    public float getBestTimeLap(){
        return bestTimeLap;
    }


    void updateTextTimer(float _time, Text _min, Text _sec, Text _mill){

        int tmpMin, tmpSec, tmpMill;

        tmpMin = (int)(_time/60f);
        tmpSec = (int)(_time%60);
        tmpMill = (int)((_time - Mathf.Floor(_time)) * 1000);

        _min.text = tmpMin.ToString("00");
        _sec.text = tmpSec.ToString("00");
        _mill.text = tmpMill.ToString("000");
    }



    public void setRunTimersFL(bool _FL){
        if (_FL){
            float tmpTime = Time.time;
            setTimeStartRace(tmpTime);
            setTimeStartLap(tmpTime);
        }
        else{
            checkBestTimeLap();
        }
        runTimersFL = _FL;
    }



    public bool runAnimationPulseTimerLap(){
        updateTextTimerLapFL = false;
        animator.SetBool("pulseLapTimeFL", true);
        return true;
    }

    public bool endAnimationPulseTimerLap(){
        //Debug.Log("endAnimationPulseTimerLap.......");
        updateTextTimerLapFL = true;
        return true;
    }




    //**************************************************
    //*******   end race   **********************
    public void showEndRaceInterface(int _playerPlace){
        
        endRaceInterfaceObj.GetComponent<EndRaceInterfaceManager>().showEndRaceInterface(_playerPlace, timeRace, bestTimeLap);
    }



    //*****************************************************
    //********* statistics
    public bool statsAddLapRaceCount(){

        return playerData.addLapRaceCount();
    }



    //set position race interface elements
    public void setRaceInterfacePosition(TrackManager _trackMgr){

        _trackMgr.setMiddleBellElementsPosition(lapNrInfo,
                                                positionNrInfo,
                                                raceTimeInfo,
                                                lapTimeInfo);
    }



}

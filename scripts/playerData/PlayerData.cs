using UnityEngine;

public class PlayerData : MonoBehaviour {


    int     minLvl, maxLvl, actualLvl,
            actualTrack, massLvl, enginePowerLvl,
            money,
            advCnt;

    float   lapRecord, actualLvlLapRecord, actualLvlRecord, actualNrOppRecord;
            //thisRaceBestTimeLap, thisRaceTime;

    //string  tmpKeyNameActualLvlLapRecord, tmpKeyNameActualLvlRecord;


//************************************************
//******   SAFE player data   ************
//******************************
    int[] safeArrSmallD = new int[] {94349, 94427, 94477, 94583 , 100741, 135211, 154369, 185543, 197683, 214439, 233347, 332641, 384403};
    int[] safeArrBigD = new int[] {703897, 704009, 704027, 704177, 743111, 798443, 852847, 916613, 988759, 1028113, 1119557, 1179551, 1233259};
    int[] bigD = new int[30], smallD = new int[30];



    void checkPlayerPrefs(){

        int tmpCheckFL = PlayerPrefs.GetInt("checkFL", 0);
        if (tmpCheckFL == 0){
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("checkFL", 1);
            setSafeD();
            loadPlayerData();
            //Debug.Log("checkPlayerPrefs <-- RESET DATA --> ");
        }
        else{
            loadPlayerData();
            //Debug.Log("checkPlayerPrefs <-- DATA OK --> ");
        }
    }



    void setSafeD(){

        for (int i = 0; i<smallD.Length; i++){

            smallD[i] = (int)Random.Range(0f, (float)safeArrSmallD.Length);
            if (smallD[i] > (safeArrSmallD.Length-1)) {smallD[i] = safeArrSmallD.Length-1;}
            string _tmpName = "smallD" + i.ToString("000");
            PlayerPrefs.SetInt(_tmpName, smallD[i]);
        }

        for (int i = 0; i<bigD.Length; i++){

            bigD[i] = (int)Random.Range(0f, (float)safeArrBigD.Length);
            if (bigD[i] > (safeArrBigD.Length-1)) {bigD[i] = safeArrBigD.Length-1;}
            string _tmpName = "bigD" + i.ToString("000");
            PlayerPrefs.SetInt(_tmpName, bigD[i]);
        }
    }



    bool safeDataVariableSet(string _nameVar, int _HwMn, string _nameType, int _min, int _arrIdx){

        string tmpNameSafe = "S" + _nameVar + "S";

        string tmpNameSmallD = "smallD" + _arrIdx.ToString("000");
        string tmpNameBigD = "bigD" + _arrIdx.ToString("000");

        /*int tmpSmallDidx = PlayerPrefs.GetInt(tmpNameSmallD, -1);
        if (tmpSmallDidx < 0){
            tmpSmallDidx = (int)Random.Range(0, smallD.Length);
            if (tmpSmallDidx > smallD.Length - 1){
                tmpSmallDidx = smallD.Length - 1;
            }
            PlayerPrefs.SetInt(tmpNameSmallD, tmpSmallDidx);
        }

        int tmpBigDidx = PlayerPrefs.GetInt(tmpNameBigD, -1);
        if (tmpBigDidx < 0){
            tmpBigDidx = (int)Random.Range(0, bigD.Length);
            if (tmpBigDidx > bigD.Length - 1){
                tmpBigDidx = bigD.Length - 1;
            }
            PlayerPrefs.SetInt(tmpNameSmallD, tmpBigDidx);
        }*/

        int tmpSmallDi = PlayerPrefs.GetInt(tmpNameSmallD, -1);
        int tmpSmallD = safeArrSmallD[tmpSmallDi];
        int tmpBigDi = PlayerPrefs.GetInt(tmpNameBigD, -1);
        int tmpBigD = safeArrBigD[tmpBigDi];

        if (_nameType == "int"){

            int tmpInt = PlayerPrefs.GetInt(_nameVar, _min);
            int tmpSafe = PlayerPrefs.GetInt(tmpNameSafe, -1);
            int tmpSafeCheck = (tmpInt * tmpSmallD) % tmpBigD;

            /*Debug.Log(  "<-- SAFE DATA SET -->\n" +
                        "tmpNameSafe: " + tmpNameSafe + ",\n " +
                        "tmpNameSmallD: " + tmpNameSmallD + ",\n " +
                        "tmpNameBigD: " + tmpNameBigD + ",\n " +
                        "tmpSmallD: " + tmpSmallD + ",\n " +
                        "tmpBigD: " + tmpBigD + ",\n " +
                        "tmpInt: " + tmpInt + ",\n " +
                        "tmpSafe: " + tmpSafe + ",\n " +
                        "tmpSafeCheck: " + tmpSafeCheck);*/

            if (tmpSafe == tmpSafeCheck || tmpInt == _min){

                //Debug.Log( "----- SAFE DATA SET --> ..OK..");
                PlayerPrefs.SetInt(_nameVar, _HwMn);
                int tmpSafeNew = (_HwMn * tmpSmallD) % tmpBigD;
                PlayerPrefs.SetInt(tmpNameSafe, tmpSafeNew);
                return true;
            }
            else{

                //Debug.Log( "----- SAFE DATA SET --> ..FALSE - DELETE..");
                PlayerPrefs.SetInt("checkFL", 0);
                checkPlayerPrefs();
                return false;
            }
        }

        /*else if (_nameType == "time"){

            float tmpFloat = PlayerPrefs.GetFloat(_nameVar, _min);
            int tmpInt = (int)(tmpFloat * 1000);
            int tmpSafe = PlayerPrefs.GetInt(tmpNameSafe, -1);
            int tmpSafeCheck = (tmpInt * tmpSmallD) % tmpBigD;

            if (tmpSafe == tmpSafeCheck || tmpInt == _min){

                Debug.Log( "----- SAFE DATA SET --> ..OK..");
                PlayerPrefs.SetFloat(_nameVar, _HwMn);
                int tmpSafeNew = (((int)(_HwMn * 1000)) * tmpSmallD) % tmpBigD;
                PlayerPrefs.SetInt(tmpNameSafe, tmpSafeNew);
                return true;
            }
            else{

                Debug.Log( "----- SAFE DATA SET --> ..FALSE - DELETE..");
                PlayerPrefs.SetInt("checkFL", 0);
                checkPlayerPrefs();
                return false;
            }
        }*/

        return true;
    }



    /*bool safeDataVariableSet(string _nameVar, float _HwMn, string _nameType, float _min, int _arrIdx){

        string tmpNameSafe = "S" + _nameVar + "S";

        string tmpNameSmallD = "smallD" + _arrIdx.ToString("000");
        string tmpNameBigD = "bigD" + _arrIdx.ToString("000");


        int tmpSmallDi = PlayerPrefs.GetInt(tmpNameSmallD, -1);
        int tmpSmallD = safeArrSmallD[tmpSmallDi];
        int tmpBigDi = PlayerPrefs.GetInt(tmpNameBigD, -1);
        int tmpBigD = safeArrBigD[tmpBigDi];


        if (_nameType == "time"){

            float tmpFloat = PlayerPrefs.GetFloat(_nameVar, _min);
            int tmpInt = (int)(tmpFloat * 1000);
            int tmpSafe = PlayerPrefs.GetInt(tmpNameSafe, -1);
            int tmpSafeCheck = (tmpInt * tmpSmallD) % tmpBigD;

            if (tmpSafe == tmpSafeCheck || tmpInt == (int)_min){

                Debug.Log( "----- SAFE DATA TIME SET --> ..OK..");
                PlayerPrefs.SetFloat(_nameVar, _HwMn);
                int tmpSafeNew = (((int)(_HwMn * 1000)) * tmpSmallD) % tmpBigD;
                PlayerPrefs.SetInt(tmpNameSafe, tmpSafeNew);
                return true;
            }
            else{

                Debug.Log( "----- SAFE DATA TIME SET --> ..FALSE - DELETE..");
                PlayerPrefs.SetInt("checkFL", 0);
                checkPlayerPrefs();
                return false;
            }
        }

        return true;
    }*/



    int safeDataVariableIntGet(string _nameVar, int _min, int _arrIdx){

        string tmpNameSafe = "S" + _nameVar + "S";

        string tmpNameSmallD = "smallD" + _arrIdx.ToString("000");
        string tmpNameBigD = "bigD" + _arrIdx.ToString("000");

        /*int tmpSmallDidx = PlayerPrefs.GetInt(tmpNameSmallD, -1);
        if (tmpSmallDidx < 0){
            tmpSmallDidx = (int)Random.Range(0, smallD.Length);
            if (tmpSmallDidx > smallD.Length - 1){
                tmpSmallDidx = smallD.Length - 1;
            }
            PlayerPrefs.SetInt(tmpNameSmallD, tmpSmallDidx);
        }

        int tmpBigDidx = PlayerPrefs.GetInt(tmpNameBigD, -1);
        if (tmpBigDidx < 0){
            tmpBigDidx = (int)Random.Range(0, bigD.Length);
            if (tmpBigDidx > bigD.Length - 1){
                tmpBigDidx = bigD.Length - 1;
            }
            PlayerPrefs.GetInt(tmpNameBigD, tmpBigDidx);
        }*/

        int tmpSmallDi = PlayerPrefs.GetInt(tmpNameSmallD, -1);
        int tmpSmallD = safeArrSmallD[tmpSmallDi];
        int tmpBigDi = PlayerPrefs.GetInt(tmpNameBigD, -1);
        int tmpBigD = safeArrBigD[tmpBigDi];

        int tmpInt = PlayerPrefs.GetInt(_nameVar, _min);
        int tmpSafe = PlayerPrefs.GetInt(tmpNameSafe, -1);
        int tmpSafeCheck = (tmpInt * tmpSmallD) % tmpBigD;

        /*Debug.Log("tmpNameSafe: " + tmpNameSafe + ", " +
                    "tmpNameSmallD: " + tmpNameSmallD + ", " +
                    "tmpNameBigD: " + tmpNameBigD + ", " +
                    "tmpSmallD: " + tmpSmallD + ", " +
                    "tmpBigD: " + tmpBigD + ", " +
                    "tmpInt: " + tmpInt + ", " +
                    "tmpSafe: " + tmpSafe + ", " +
                    "tmpSafeCheck: " + tmpSafeCheck);*/

        if (tmpSafe == tmpSafeCheck || tmpInt == _min){

            //Debug.Log( "----- SAFE DATA GET --> ..OK..");
            return tmpInt;
        }
        else{

            //Debug.Log( "----- SAFE DATA GET --> ..FALSE - DELETE..");
            PlayerPrefs.SetInt("checkFL", 0);
            checkPlayerPrefs();
            tmpInt = PlayerPrefs.GetInt(_nameVar, _min);
            return tmpInt;
        }
    }



    /*float safeDataVariableTimeGet(string _nameVar, float _min, int _arrIdx){

        string tmpNameSafe = "S" + _nameVar + "S";

        string tmpNameSmallD = "smallD" + _arrIdx.ToString("000");
        string tmpNameBigD = "bigD" + _arrIdx.ToString("000");


        int tmpSmallDi = PlayerPrefs.GetInt(tmpNameSmallD, -1);
        int tmpSmallD = safeArrSmallD[tmpSmallDi];
        int tmpBigDi = PlayerPrefs.GetInt(tmpNameBigD, -1);
        int tmpBigD = safeArrBigD[tmpBigDi];

        float tmpFloat = PlayerPrefs.GetFloat(_nameVar, _min);
        int tmpInt = (int)(tmpFloat * 1000);
        int tmpSafe = PlayerPrefs.GetInt(tmpNameSafe, -1);
        int tmpSafeCheck = (tmpInt * tmpSmallD) % tmpBigD;


        if (tmpSafe == tmpSafeCheck || tmpInt == (int)_min){

            //Debug.Log( "----- SAFE DATA GET --> ..OK..");
            return tmpFloat;
        }
        else{

            //Debug.Log( "----- SAFE DATA GET --> ..FALSE - DELETE..");
            PlayerPrefs.SetInt("checkFL", 0);
            checkPlayerPrefs();
            tmpFloat = PlayerPrefs.GetFloat(_nameVar, _min);
            return tmpFloat;
        }
    }*/



//************************************************************
//******************************************
//*************************

    void Start(){

        checkPlayerPrefs();
        //loadPlayerData();
    }



    bool loadPlayerData(){

        actualTrack = PlayerPrefs.GetInt("actualTrack", 0);
        minLvl = PlayerPrefs.GetInt("minLvl", 1);

        //maxLvl = PlayerPrefs.GetInt("maxLvl", 1);
        //maxLvl = safeDataVariableIntGet("maxLvl", 1, 0);
        getMaxLvl();

        //actualLvl = PlayerPrefs.GetInt("actualLvl", 1);
        //actualLvl = safeDataVariableIntGet("actualLvl", 1, 1);
        getActualLvl();

        //massLvl = PlayerPrefs.GetInt("massLvl", 1);
        massLvl = safeDataVariableIntGet("massLvl", 1, 2);
        //enginePowerLvl = PlayerPrefs.GetInt("enginePowerLvl", 1);
        enginePowerLvl = safeDataVariableIntGet("enginePowerLvl", 1, 3);

        //money = PlayerPrefs.GetInt("money", 0);
        money = safeDataVariableIntGet("money", 0, 4);

        //lapRecord = PlayerPrefs.GetFloat("lapRecord", 9999);
        getLapRecord();
        //lapRecord = safeDataVariableTimeGet("lapRecord", 0f, 6);

        advCnt = PlayerPrefs.GetInt("advCnt", 7);

        PlayerPrefs.SetFloat("deltaTraceOfTiresColorDark", 0f);

        return true;
    }



    string determineKeyNameActualLvlLapRecord(){

        string tmpName = "actualLvlLapRecord" + actualTrack + "V" + actualLvl;
        return tmpName;
    }



    string determineKeyNameNumberOpponentsLapRecord(){

        int nrOppTmp = actualLvl%10;
        if (nrOppTmp == 0) {nrOppTmp = 10;};
        string tmpName = "numberOpponentsRaceRecord" + actualTrack + "V" + nrOppTmp;
        return tmpName;
    }



    string determineKeyNameActualLvlRecord(){

        string tmpName = "actualLvlRecord" + actualTrack + "V" + actualLvl;
        return tmpName;
    }



    string determineKeyNameLapRecordEver(){

        string tmpName = "lapRecord" + actualTrack + "V";
        return tmpName;
    }



    public bool setActualLvlLapRecord(float _actualLvlLapRecord){

        actualLvlLapRecord = _actualLvlLapRecord;
        PlayerPrefs.SetFloat(determineKeyNameActualLvlLapRecord(), actualLvlLapRecord);
        return true;
    }



    public bool setActualLvlRecord(float _actualLvlRecord){

        actualLvlRecord = _actualLvlRecord;
        PlayerPrefs.SetFloat(determineKeyNameActualLvlRecord(), actualLvlRecord);
        return true;
    }



    public bool setLapRecord(float _lapRecord){

        /*safeDataVariableSet("lapRecord", _lapRecord, "time", 0f, 6);
        lapRecord = getLapRecord();*/
        lapRecord = _lapRecord;

        if (actualTrack == 0){
            PlayerPrefs.SetFloat("lapRecord", lapRecord);
        }

        else{
            PlayerPrefs.SetFloat(determineKeyNameLapRecordEver(), lapRecord);
        }

        return true;
    }



    public bool setNrOppRecord(float _raceTime){

        actualNrOppRecord = _raceTime;
        PlayerPrefs.SetFloat(determineKeyNameNumberOpponentsLapRecord(), actualNrOppRecord);
        return true;
    }


//***************************************
//******   MONEY   **********

    public bool addMoney(int _hwMn){

        getMoney();
        money += _hwMn;     //if game
        //money += 150000;     //if dev test
        setMoney();
        return true;
    }



    public bool takeMoney(int _hwMn){

        getMoney();
        money -= _hwMn;
        setMoney();
        return true;
    }



    public int getMoney(){

        money = safeDataVariableIntGet("money", 0, 4);
        //money = PlayerPrefs.GetInt("money", 0);
        return money;
    }



    bool setMoney(){

        safeDataVariableSet("money", money, "int", 0, 4);       //if game
        //safeDataVariableSet("money", 99999999, "int", 0, 4);  //if dev test add more $
        money = getMoney();
        //PlayerPrefs.SetInt("money", money);
        return true;
    }




//**************************************
//******  UPGRADES  *************

    public bool updateEnginePowerLvl(){

        //enginePowerLvl = PlayerPrefs.GetInt("enginePowerLvl", 1);
        enginePowerLvl = getEnginePowerLvl();
        enginePowerLvl += 1;
        //PlayerPrefs.SetInt("enginePowerLvl", enginePowerLvl);
        setEnginePowerLvlLvl(enginePowerLvl);
        return true;
    }



    public bool updateMassLvl(){

        //massLvl = PlayerPrefs.GetInt("massLvl", 1);
        massLvl = getMassLvl();
        massLvl += 1;
        //PlayerPrefs.SetInt("massLvl", massLvl);
        setMassLvl(massLvl);
        return true;
    }



//*********************************************



    public bool setMassLvl(int _massLvl){

        //massLvl = _massLvl;
        safeDataVariableSet("massLvl", _massLvl, "int", 1, 2);
        massLvl = getMassLvl();
        //PlayerPrefs.SetInt("massLvl", massLvl);
        return true;
    }



    public bool setEnginePowerLvlLvl(int _enginePowerLvl){

        //enginePowerLvl = _enginePowerLvl;
        safeDataVariableSet("enginePowerLvl", _enginePowerLvl, "int", 1, 3);
        enginePowerLvl = getEnginePowerLvl();
        //PlayerPrefs.SetInt("enginePowerLvl", enginePowerLvl);
        return true;
    }



    public bool setMaxLvl(int _maxLvl){

        int tmpSafeNr = 0;

        if(actualTrack == 0){
            //maxLvl = _maxLvl;
            //safeDataVariableSet("maxLvl", 111, "int", 1, tmpSafeNr);      //if dev test
            safeDataVariableSet("maxLvl", _maxLvl, "int", 1, 0);            //if game
            maxLvl = getMaxLvl();
            //PlayerPrefs.SetInt("maxLvl", maxLvl);
        }
        else{

            tmpSafeNr += (9 + actualTrack);
            string tmpName = "maxLvl" + actualTrack.ToString();
            safeDataVariableSet(tmpName, _maxLvl, "int", 1, tmpSafeNr);
            maxLvl = getMaxLvl();
        }

        return true;
    }



    public bool setActualLvl(int _actualLvl){

        //actualLvl = _actualLvl;
        int tmpSafeNr = 1;

        if(actualTrack == 0){
            safeDataVariableSet("actualLvl", _actualLvl, "int", 1, tmpSafeNr);
            actualLvl = getActualLvl();
        }
        else{

            tmpSafeNr += (18 + actualTrack);
            string tmpName = "actualLvl" + actualTrack.ToString();
            safeDataVariableSet(tmpName, _actualLvl, "int", 1, tmpSafeNr);
            actualLvl = getActualLvl();
        }
        //PlayerPrefs.SetInt("actualLvl", actualLvl);
        return true;
    }



    public bool setActualTrack(int _actuaTrack){

        actualTrack = _actuaTrack;
        PlayerPrefs.SetInt("actualTrack", actualTrack);
        return true;
    }



    public int getEnginePowerLvl(){

        enginePowerLvl = safeDataVariableIntGet("enginePowerLvl", 1, 3);
        //enginePowerLvl = PlayerPrefs.GetInt("enginePowerLvl", 1);
        return enginePowerLvl;
    }



    public int getMassLvl(){

        massLvl = safeDataVariableIntGet("massLvl", 1, 2);
        //massLvl = PlayerPrefs.GetInt("massLvl", 1);
        return massLvl;
    }



    public int getMinLvl(){

        return minLvl;
    }



    public int getMaxLvl(){

        int tmpSafeNr = 0;

        if (actualTrack == 0){

            maxLvl = safeDataVariableIntGet("maxLvl", 1, tmpSafeNr);
        }
        else{

            tmpSafeNr += (9 + actualTrack);
            string tmpName = "maxLvl" + actualTrack.ToString();
            maxLvl = safeDataVariableIntGet(tmpName, 1, tmpSafeNr);
        }

        return maxLvl;
    }



    public int getMaxLvl(int _trackNr){

        int tmpSafeNr = 0,
            tmpMaxLvl = 0;

        if (_trackNr == 0){

            tmpMaxLvl = safeDataVariableIntGet("maxLvl", 1, tmpSafeNr);
        }
        else{

            tmpSafeNr += (9 + _trackNr);
            string tmpName = "maxLvl" + _trackNr.ToString();
            tmpMaxLvl = safeDataVariableIntGet(tmpName, 1, tmpSafeNr);
        }

        return tmpMaxLvl;
    }



    public int getActualLvl(){

        int tmpSafeNr = 1;

        if (actualTrack == 0){

            actualLvl = safeDataVariableIntGet("actualLvl", 1, tmpSafeNr);
        }
        else{

            tmpSafeNr += (18 + actualTrack);
            string tmpName = "actualLvl" + actualTrack.ToString();
            actualLvl = safeDataVariableIntGet(tmpName, 1, tmpSafeNr);
        }

        return actualLvl;
    }



    public int getActualTrack(){

        return actualTrack;
    }



    public float getActualLvlLapRecord(){

        actualLvlLapRecord = PlayerPrefs.GetFloat(determineKeyNameActualLvlLapRecord(), 9999);
        return actualLvlLapRecord;
    }



    public float getActualLvlRecord(){

        actualLvlRecord = PlayerPrefs.GetFloat(determineKeyNameActualLvlRecord(), 9999);
        return actualLvlRecord;
    }



    public float getLapRecord(){

        //lapRecord = safeDataVariableTimeGet("lapRecord", 0f, 6);
        if (actualTrack == 0){
            lapRecord = PlayerPrefs.GetFloat("lapRecord", 9999);
        }

        else{
            lapRecord = PlayerPrefs.GetFloat(determineKeyNameLapRecordEver(), 9999);
        }

        return lapRecord;
    }



    public float getNrOppRecord(){

        actualNrOppRecord = PlayerPrefs.GetFloat(determineKeyNameNumberOpponentsLapRecord(), 9999);
        return actualNrOppRecord;
    }


    /*public float getThisRaceBestTimeLap(){
        return thisRaceBestTimeLap;
    }
    public bool setThisRaceBestTimeLap(float _time){
        if (thisRaceBestTimeLap>0){
            if(_time<thisRaceBestTimeLap){
                thisRaceBestTimeLap = _time;
                return true;
            }
            return false;
        }
        else{
            thisRaceBestTimeLap = _time;
            return false;
        }
    }


    public float getThisRaceTime(){
        return thisRaceTime;
    }
    public bool setThisRaceTime(float _time){
        thisRaceTime = _time;
        return true;
    }*/



//***********************************************************
//******** new stats 20190508
    public int getStartRaceCount(){

        int tmpCnt = PlayerPrefs.GetInt("startRaceCount", 0);
        return tmpCnt;
    }



    public bool addStartRaceCount(){

        int tmpCnt = getStartRaceCount();
        tmpCnt += 1;
        PlayerPrefs.SetInt("startRaceCount", tmpCnt);
        return true;
    }



    public int getEndRaceCount(){

        int tmpCnt = PlayerPrefs.GetInt("endRaceCount", 0);
        return tmpCnt;
    }



    public bool addEndRaceCount(){

        int tmpCnt = getEndRaceCount();
        tmpCnt += 1;
        PlayerPrefs.SetInt("endRaceCount", tmpCnt);
        return true;
    }



    public int getLapRaceCount(){

        int tmpCnt = PlayerPrefs.GetInt("lapRaceCount", 0);
        return tmpCnt;
    }



    public bool addLapRaceCount(){

        int tmpCnt = getLapRaceCount();
        tmpCnt += 1;
        PlayerPrefs.SetInt("lapRaceCount", tmpCnt);
        return true;
    }



    public bool setLvlRaceCountFL(int _nrLvl, bool _FL){

        string tmpName = "lvlRaceCountFL" + _nrLvl.ToString("00000");

        if(_FL){

            PlayerPrefs.SetInt(tmpName, 1);
            return true;
        }
        else{

            return false;
        }
    }



    public bool getLvlRaceCountFL(int _nrLvl){

        string tmpName = "lvlRaceCountFL" + _nrLvl.ToString("00000");
        int tmpNr = PlayerPrefs.GetInt(tmpName, 0);

        if (tmpNr != 0){

            return true;
        }
        else {

            return false;
        }
    }



    public int getLvlRaceCount(int _nrLvl){

        string tmpName = "lvlRaceCount" + _nrLvl.ToString("00000");
        int tmpCnt = PlayerPrefs.GetInt(tmpName, 0);
        return tmpCnt;
    }



    public bool addLvlRaceCount(int _nrLvl){

        int tmpCnt = getLvlRaceCount(_nrLvl);
        tmpCnt += 1;
        string tmpName = "lvlRaceCount" + _nrLvl.ToString("00000");
        PlayerPrefs.SetInt(tmpName, tmpCnt);
        return true;
    }



    public int getPlaceRaceCount(int _place){

        string tmpName = "placeRaceCount" + _place.ToString("000");
        int tmpCnt = PlayerPrefs.GetInt(tmpName, 0);
        return tmpCnt;
    }



    public bool addPlaceRaceCount(int _place){

        string tmpName = "placeRaceCount" + _place.ToString("000");
        int tmpCnt = getPlaceRaceCount(_place);
        tmpCnt += 1;
        PlayerPrefs.SetInt(tmpName, tmpCnt);
        return true;
    }




//Advertisements data manager
    public int getAdvCnt(){

        advCnt = PlayerPrefs.GetInt("advCnt", 7);
        return advCnt;
    }


    public bool oddAdvCnt(){

        getAdvCnt();
        advCnt -= 1;
        PlayerPrefs.SetInt("advCnt", advCnt);
        return true;
    }


    public bool setAdvCnt(int _hwMn){

        PlayerPrefs.SetInt("advCnt", _hwMn);
        return true;
    }



    public void setAdvPriceSmall(int _hwMn){

        int tmpHwMn = (int)(  ( (float)_hwMn ) / 8f  );
        if (tmpHwMn > getAdvPriceSmall()){

            safeDataVariableSet("advPriceSmall", tmpHwMn, "int", 1, 5);
        }
    }


    public int getAdvPriceSmall(){

        return safeDataVariableIntGet("advPriceSmall", 10, 5);
    }

//*************************
//Options
    public bool getOptGraDustOnRoad(){

        int tmpNr =  PlayerPrefs.GetInt("optGraDustOnRoad", 1);
        if (tmpNr == 0){
            return false;
        }
        else {
            return true;
        }
    }


    public bool setOptGraDustOnRoad(bool _FL){

        if (_FL){
            PlayerPrefs.SetInt("optGraDustOnRoad", 1);
        }
        else {
            PlayerPrefs.SetInt("optGraDustOnRoad", 0);
        }
        return true;
    }


    public bool getOptGraTraceOfTires(){

        int tmpNr = PlayerPrefs.GetInt("optGraTraceOfTires", 1);
        if (tmpNr == 0){
            return false;
        }
        else {
            return true;
        }
    }

    public bool setOptGraTraceOfTires(bool _FL){

        if (_FL){
            PlayerPrefs.SetInt("optGraTraceOfTires", 1);
        }
        else {
            PlayerPrefs.SetInt("optGraTraceOfTires", 0);
        }
        return true;
    }


    public bool getOptGraTraceOfTiresColorLight(){

        int tmpNr = PlayerPrefs.GetInt("optGraTraceOfTiresColorLight", 0);
        if (tmpNr == 0){
            return false;
        }
        else {
            return true;
        }
    }


    public bool setOptGraTraceOfTiresColorLight(bool _FL){

        if (_FL){
            PlayerPrefs.SetInt("optGraTraceOfTiresColorLight", 1);
        }
        else {
            PlayerPrefs.SetInt("optGraTraceOfTiresColorLight", 0);
        }
        return true;
    }



    public bool getOptSoundSounds(){

        int tmpNr = PlayerPrefs.GetInt("optSoundSounds", 1);
        if (tmpNr == 0){
            return false;
        }
        else {
            return true;
        }
    }

    public bool setOptSoundSounds(bool _FL){

        if (_FL){
            PlayerPrefs.SetInt("optSoundSounds", 1);
        }
        else {
            PlayerPrefs.SetInt("optSoundSounds", 0);
        }
        return true;
    }



    public bool getOptSoundSoundsAll(){

        int tmpNr = PlayerPrefs.GetInt("optSoundSoundsAll", 1);
        if (tmpNr == 0){
            return false;
        }
        else {
            return true;
        }
    }


    public bool setOptSoundSoundsAll(bool _FL){

        if (_FL){
            PlayerPrefs.SetInt("optSoundSoundsAll", 1);
        }
        else {
            PlayerPrefs.SetInt("optSoundSoundsAll", 0);
        }
        return true;
    }



    public bool getOptMoreStats(){

        int tmpNr = PlayerPrefs.GetInt("optMoreStats", 0);
        if (tmpNr == 0){
            return false;
        }
        else {
            return true;
        }
    }


    public bool setOptMoreStats(bool _FL){

        if (_FL){
            PlayerPrefs.SetInt("optMoreStats", 1);
        }
        else {
            PlayerPrefs.SetInt("optMoreStats", 0);
        }
        return true;
    }



//.......................................
//delta Trace Of TiresColorDark

    public bool deltaTraceOfTiresColorDarkOdd(float _odd, float _min){

        float tmpDelta = PlayerPrefs.GetFloat("deltaTraceOfTiresColorDark", 0f);
        if (tmpDelta < _min){ tmpDelta += _odd; }
        PlayerPrefs.SetFloat("deltaTraceOfTiresColorDark", tmpDelta);
        //Debug.Log("deltaTraceOfTiresColorDark: " + getDeltaTraceOfTiresColorDark());
        return true;
    }



    public float getDeltaTraceOfTiresColorDark(){

        return PlayerPrefs.GetFloat("deltaTraceOfTiresColorDark", 0f);
    }



    public float getDeltaTraceOfTiresColorDarkRandom(){

        float tmpDeltaColorDark = PlayerPrefs.GetFloat("deltaTraceOfTiresColorDark", 0f);
        return Random.Range(tmpDeltaColorDark - 0.1f, tmpDeltaColorDark);
    }




    public void developResetBtn(){

        safeDataVariableSet("massLvl", 1, "int", 1, 2);
        safeDataVariableSet("enginePowerLvl", 1, "int", 1, 3);
    }



//********************************************
//**************SHOP*****************
    public void shopStuffSetState(string _name, int _stateNr){          //_stateNr, 0 to buy, 1 owned, 2 active

        PlayerPrefs.SetInt(_name, _stateNr);
    }


    public int shopStuffGetState(string _name){

        return PlayerPrefs.GetInt(_name, 0);
    }



    public string shopStuffGetActive(string _name){

        string tmpName = "";

        switch (_name)
        {
            case "track":
                tmpName = shopStuffGetActiveTrack();
              break;
            case "look":
                tmpName = shopStuffGetActiveLook();
              break;
            case "smoke":
                tmpName = shopStuffGetActiveSmoke();
              break;
            default:
                Debug.Log("data stuffActive get error...");
              break;
        }

        return tmpName;
    }



    public void shopStuffSetActive(string _nameType, string _name){

        switch (_nameType)
      {
          case "track":
              shopStuffSetActiveTrack(_name);
              break;
          case "look":
              shopStuffSetActiveLook(_name);
              break;
          case "smoke":
              shopStuffSetActiveSmoke(_name);
              break;
          default:
              Debug.Log("data stuffActive set error...");
              break;
      }
    }



    void shopStuffSetActiveTrack(string _name){

        PlayerPrefs.SetString("shpActiveTrack", _name);
        //****set actual track ->v
        string tmpTrackName = _name[5].ToString() + _name[6].ToString();    //_name="track02": _name[5]=0, _name[6]=2; tmpTrackName = "02"
        int tmpTrackNr = System.Int32.Parse(tmpTrackName);                  //tmpTrackNr = System.Int32.Parse("02") ...=2
        setActualTrack(tmpTrackNr - 1);
        //*****     ->^
    }

    string shopStuffGetActiveTrack(){

        return PlayerPrefs.GetString("shpActiveTrack", "track01");
    }


    void shopStuffSetActiveLook(string _name){

        PlayerPrefs.SetString("shpActiveLook", _name);
    }

    string shopStuffGetActiveLook(){

        return PlayerPrefs.GetString("shpActiveLook", "look01");
    }


    void shopStuffSetActiveSmoke(string _name){

        PlayerPrefs.SetString("shpActiveSmoke", _name);
    }

    string shopStuffGetActiveSmoke(){

        return PlayerPrefs.GetString("shpActiveSmoke", "smoke01");
    }





}

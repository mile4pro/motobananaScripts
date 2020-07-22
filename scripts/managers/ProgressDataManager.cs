using UnityEngine;

public class ProgressDataManager : MonoBehaviour {


    [SerializeField]
    GameObject  playerDataObj;
    PlayerData  playerData;

    int         stepMoneyWin = 1,
                stepMoneyUpgrade = 225;



    void Start(){

        playerData = playerDataObj.GetComponent<PlayerData>();
    }



    public int moneyForWinLvl(int _lvl){

        return hwMnMoneyForLvl(_lvl, 0, stepMoneyWin);
    }



    public int moneyForUpgrade(int _upgradeLvl){

        return hwMnMoneyForLvlUpgrade(_upgradeLvl, 0, stepMoneyUpgrade);
    }



    int hwMnMoneyForLvl(int _lvl, int _moneyStart, int _moneyStep){

        int tmpLvl = _lvl + (playerData.getActualTrack()*15);
        int tmpMoney = _moneyStart,
            tmpStep = 0;
        for(int i = 0; i < tmpLvl; i++){
            tmpStep += _moneyStep;
            tmpMoney += tmpStep;
        }
        return tmpMoney;
    }



    int hwMnMoneyForLvlUpgrade(int _lvl, int _moneyStart, int _moneyStep){

        int tmpLvl = _lvl;
        int tmpMoney = _moneyStart,
            tmpStep = 0;
        for(int i = 0; i < tmpLvl; i++){
            tmpStep += _moneyStep;
            tmpMoney += tmpStep;
        }
        return tmpMoney;
    }


//******************************************
//*******  END RACE PRIZE  *************
    public bool endRace(int _place){

        PlayerData tmpPlayerData = playerDataObj.GetComponent<PlayerData>();

        if (_place<2){

            int tmpActualLvl = tmpPlayerData.getActualLvl();
            tmpPlayerData.addMoney(moneyForWinLvl(tmpActualLvl));
            tmpPlayerData.setAdvPriceSmall(moneyForWinLvl(tmpActualLvl));
            int tmpMaxLvl = tmpPlayerData.getMaxLvl();


            if (tmpActualLvl < tmpMaxLvl){
                return false;
            }
            else{     //unlock next lvl
                int tmpNewLvl = tmpMaxLvl + 1;
                //Debug.Log("max lvl before... " + tmpPlayerData.getMaxLvl());
                tmpPlayerData.setMaxLvl(tmpNewLvl);
                tmpPlayerData.setActualLvl(tmpNewLvl);
                //Debug.Log("max lvl after... " + tmpPlayerData.getMaxLvl());
                return true;
            }


            return false;
        }

        return false;
    }



//**************************************************************************
//********************************************************
//*********  check lap record ******************
    public bool checkLapRecordThisLvl(float _timeBestLapThisLvl){

        float tmpActualRec = playerData.getActualLvlLapRecord();
        if (_timeBestLapThisLvl < tmpActualRec){
            playerData.setActualLvlLapRecord(_timeBestLapThisLvl);
            if (tmpActualRec > 9998) {return false;}
            return true;
        }
        else{
            return false;
        }
    }



    public bool checkLapRecordEver(float _timeBestLap){

        float tmpActualRec = playerData.getLapRecord();
        if (_timeBestLap < tmpActualRec){
            playerData.setLapRecord(_timeBestLap);
            if (tmpActualRec > 9998) {return false;}
            return true;
        }
        else{
            return false;
        }
    }



    public bool checkLapRecordThisLvlPlayerData(float _timeBestLap){

        float tmpActualRec = playerData.getActualLvlLapRecord();
        if (_timeBestLap < tmpActualRec){
            playerData.setActualLvlLapRecord(_timeBestLap);
            if (tmpActualRec > 9998) {return false;}
            return true;
        }
        else{
            return false;
        }
    }



    public bool checkRaceRecordThisLvlPlayerData(float _timeRace){

        float tmpActualRec = playerData.getActualLvlRecord();
        if (_timeRace < tmpActualRec){
            playerData.setActualLvlRecord(_timeRace);
            if (tmpActualRec > 9998) {return false;}
            return true;
        }
        else{
            return false;
        }
    }



    public bool checkRaceRecordNrOppPlayerData(float _timeRace){

        float tmpActualRec = playerData.getNrOppRecord();
        if (_timeRace < tmpActualRec){
            playerData.setNrOppRecord(_timeRace);
            if (tmpActualRec > 9998) {return false;}
            return true;
        }
        else{
            return false;
        }
    }



    public PlayerData getPlayerData(){
        return playerData;
    }

}

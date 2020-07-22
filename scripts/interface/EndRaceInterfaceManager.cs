using UnityEngine;
using UnityEngine.UI;

public class EndRaceInterfaceManager : MonoBehaviour {

    [SerializeField]
    GameObject  lvlNrInterfaceObj,
                raceInterfaceObj,
                gameMngrObj,
                topgroundObj;

    [SerializeField]
    GameObject  timeRaceInfoMinuteObj, timeRaceInfoSecondObj, timeRaceInfoMilliObj,
                timeLapInfoMinuteObj, timeLapInfoSecondObj, timeLapInfoMilliObj,
                textMoneyHwMnObj;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject textPlaceObj;

    [SerializeField]
    GameObject progressDataObj;
    ProgressDataManager progressDataMgr;

    [SerializeField]
    GameObject  recordTextRecordObj, recordTextBestLapLvlObj,
                recordTextBestLapEverObj, recordTextBestRaceLvlObj,
                recordTextBestRaceFoeObj;
    bool        recordTextRecordFL, recordTextBestLapLvlFL,
                recordTextBestLapEverFL, recordTextBestRaceLvlFL,
                recordTextBestRaceFoeFL;

    [SerializeField]
    GameObject  restartBtnObj;
    bool        restartFL;

    AdvManager advMgr;

    [SerializeField]
    GameObject  animationInformationInterfaceObj;
    bool        showAnimationInformationFL = false;

    [SerializeField]
    GameObject  nextBtnTextObj;

    [SerializeField]
    GameObject  advBtnObj;
    AdvBtnManagerEndRace    advBtnMgr;


    void Start(){
        
        advMgr = gameMngrObj.GetComponent<GameMngr>().getAdvMgr();
        advBtnMgr = advBtnObj.GetComponent<AdvBtnManagerEndRace>();
    }



    public void showEndRaceInterface(int _playerPlace, float _timeRace, float _timeBestLap){

        gameObject.SetActive(true);
        setInfosText(_playerPlace, _timeRace, _timeBestLap);
        textPlaceObj.GetComponent<Text>().text = _playerPlace.ToString();
        animator.SetBool("showEndRaceFL", true);
        //moneyFoRace(_playerPlace);
        endRaceDataCheck(_playerPlace, _timeRace, _timeBestLap);
        checkRecordAnimation();
        progressDataObj.GetComponent<ProgressDataManager>().getPlayerData().deltaTraceOfTiresColorDarkOdd(0.01f, 0.8f);
        setStatistics(_playerPlace);

    }


    public void animatorEndShowEndRace(){
        animator.SetBool("showEndRaceFL", false);
        restartBtnObj.GetComponent<Button>().interactable = true;
        //checkRecordAnimation();
    }


    public void animatorStartDisappear(){
        topgroundObj.SetActive(true);
        animator.SetBool("disappearFL", true);
    }


    public void animatorEndDisappear(){
        animator.SetBool("disappearFL", false);
        resetRecordFL();
        restartBtnObj.GetComponent<Button>().interactable = false;
        topgroundObj.SetActive(false);
        gameObject.SetActive(false);
        raceInterfaceObj.GetComponent<RaceInterfaceManager>().resetRaceData();
        raceInterfaceObj.SetActive(false);
        gameMngrObj.GetComponent<GameMngr>().forceEndLvl();


        if (restartFL){

            restartFL = false;
            gameMngrObj.GetComponent<GameMngr>().loadLvlRestart();
            gameMngrObj.GetComponent<GameMngr>().raceInterfaceStart();
        }
        else if (showAnimationInformationFL){

            showAnimationInformationFL = false;
            animationInformationInterfaceObj.SetActive(true);
            animationInformationInterfaceObj.GetComponent<AnimationInformationInterfaceManager>()
                                            .showAnmiationInformation(
                                            progressDataObj.GetComponent<ProgressDataManager>().getPlayerData().getMaxLvl()
                                            );
            /*animationInformationInterfaceObj.GetComponent<AnimationInformationInterfaceManager>()
                                            .showAnmiationInformation(31);*/
        }
        else{

            lvlNrInterfaceObj.SetActive(true);
            lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().animatorStartShow();
            //devAnimationInformationTEST(41);
        }

    }



    void devAnimationInformationTEST(int _nrLVL){

        showAnimationInformationFL = false;
        animationInformationInterfaceObj.SetActive(true);
        animationInformationInterfaceObj.GetComponent<AnimationInformationInterfaceManager>()
                                        .showAnmiationInformation(_nrLVL);
    }



    public void restartBtn(){
        restartFL = true;
        animatorStartDisappear();
        advMgr.checkAdv();
    }



//record animation
    public void animatorRecordInfoStart(){
        animator.SetBool("recordEndRaceFL", true);
    }

    public void animatorRecordInfoEnd(){
        animator.SetBool("recordEndRaceFL", false);
    }

    void checkRecordAnimation(){
        bool tmpAnimationStartFL = false;
        if(recordTextBestLapEverFL){
            recordTextBestLapEverObj.SetActive(true);
            tmpAnimationStartFL = true;
        }
        else if(recordTextBestLapLvlFL){
            recordTextBestLapLvlObj.SetActive(true);
            tmpAnimationStartFL = true;
        }

        if(recordTextBestRaceFoeFL){
            recordTextBestRaceFoeObj.SetActive(true);
            tmpAnimationStartFL = true;
            int tmpHwMnFoes = gameMngrObj.GetComponent<GameMngr>().getHwMnBananas()-1;
            string tmpTextFoes = " opponent";
            if (tmpHwMnFoes > 1) tmpTextFoes = " opponents";
            recordTextBestRaceFoeObj.GetComponent<Text>().text = "new best race\nwhen " + tmpHwMnFoes + tmpTextFoes;
        }
        else if(recordTextBestRaceLvlFL){
            recordTextBestRaceLvlObj.SetActive(true);
            tmpAnimationStartFL = true;
        }

        if(tmpAnimationStartFL){
            recordTextRecordObj.SetActive(true);
            //animatorRecordInfoStart();
        }
    }

    void resetRecordFL(){
        recordTextBestLapEverFL = false;
        recordTextBestLapLvlFL = false;
        recordTextBestRaceFoeFL = false;
        recordTextBestRaceLvlFL = false;
        recordTextRecordFL = false;
        recordTextBestLapEverObj.SetActive(false);
        recordTextBestLapLvlObj.SetActive(false);
        recordTextBestRaceFoeObj.SetActive(false);
        recordTextBestRaceLvlObj.SetActive(false);
        recordTextRecordObj.SetActive(false);
    }




    void endRaceDataCheck(int _place, float _timeRace, float _timeBestLap){

        ProgressDataManager progressDataMgrTmp = progressDataObj.GetComponent<ProgressDataManager>();

        recordTextBestLapEverFL = progressDataMgrTmp.checkLapRecordEver(_timeBestLap);    //true if new record, false if not
        recordTextBestLapLvlFL = progressDataMgrTmp.checkLapRecordThisLvlPlayerData(_timeBestLap);   //true if new record, false if not
        recordTextBestRaceLvlFL = progressDataMgrTmp.checkRaceRecordThisLvlPlayerData(_timeRace);   //true if new record, false if not
        recordTextBestRaceFoeFL = progressDataMgrTmp.checkRaceRecordNrOppPlayerData(_timeRace);   //true if new record, false if not

        Text nextBtnTextTmp = nextBtnTextObj.GetComponent<Text>();

        if (progressDataMgrTmp.endRace(_place)){

            nextBtnTextTmp.text = "Unlock Next";
            nextBtnTextTmp.fontSize = 44;
            restartBtnObj.SetActive(false);
            int tmpNrNewLvl = progressDataMgrTmp.getPlayerData().getMaxLvl();
            if (tmpNrNewLvl > 10 && (tmpNrNewLvl-1)%10 == 0) {showAnimationInformationFL = true;}
        }
        else{

            nextBtnTextTmp.text = "End";
            nextBtnTextTmp.fontSize = 50;
            restartBtnObj.SetActive(true);
        };
    }

    /*public void moneyFoRace(int _place){
        progressDataObj.GetComponent<ProgressDataManager>().endRace(_place);
    }*/



    void updateTextTimer(float _time, Text _min, Text _sec, Text _mill){

        int tmpMin, tmpSec, tmpMill;

        tmpMin = (int)(_time/60f);
        tmpSec = (int)(_time%60);
        tmpMill = (int)((_time - Mathf.Floor(_time)) * 1000);

        _min.text = tmpMin.ToString("00");
        _sec.text = tmpSec.ToString("00");
        _mill.text = tmpMill.ToString("000");
    }



    bool setInfosText(int _playerPlace, float _timeRace, float _timeBestLap){

        updateTextTimer(    _timeRace,
                            timeRaceInfoMinuteObj.GetComponent<Text>(),
                            timeRaceInfoSecondObj.GetComponent<Text>(),
                            timeRaceInfoMilliObj.GetComponent<Text>()
                        );

        updateTextTimer(    _timeBestLap,
                            timeLapInfoMinuteObj.GetComponent<Text>(),
                            timeLapInfoSecondObj.GetComponent<Text>(),
                            timeLapInfoMilliObj.GetComponent<Text>()
                        );

        int tmpActualLvl = lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().getActualLvl();
        int tmpMoneyForThusLvl = progressDataObj.GetComponent<ProgressDataManager>().moneyForWinLvl(tmpActualLvl);

        textMoneyHwMnObj.transform.parent.gameObject.SetActive(true);
        if(_playerPlace < 2){
            textMoneyHwMnObj.GetComponent<Text>().text = tmpMoneyForThusLvl.ToString() + "$";
        }
        else{
            textMoneyHwMnObj.GetComponent<Text>().text = "0$";
            textMoneyHwMnObj.transform.parent.gameObject.SetActive(false);
        }

        //test adv btn
        if(advBtnMgr != null){

            advBtnMgr.checkAdv(_playerPlace, tmpActualLvl, tmpMoneyForThusLvl, animatorShowCashForAdStart);
        }
        else{

            advBtnObj.GetComponent<AdvBtnManagerEndRace>().checkAdv(_playerPlace, tmpActualLvl, tmpMoneyForThusLvl, animatorShowCashForAdStart);
        }

        return true;
    }



    bool setStatistics(int _playerPlace){

        GameMngr gameMngrTmp = gameMngrObj.GetComponent<GameMngr>();
        gameMngrTmp.statsEndRace(_playerPlace);
        return true;
    }



    public void animatorShowCashForAdStart(){

        animator.SetBool("cashForAdFL", true);
    }

    public void animatorShowCashForAdEnd(){

        animator.SetBool("cashForAdFL", false);
    }

}

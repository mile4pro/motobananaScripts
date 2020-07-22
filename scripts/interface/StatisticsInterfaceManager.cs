using UnityEngine;
using UnityEngine.UI;

public class StatisticsInterfaceManager : MonoBehaviour {

    [SerializeField]
    GameObject textNrLvlObj, textPrizeObj;
    Text textNrLvl, textPrize;

    [SerializeField]
    GameObject progressDataObj, playerDataObj;
    ProgressDataManager progressDataMgr;
    PlayerData playerDataMgr;

    [SerializeField]
    GameObject lapRecEverTextObj, lapRecThisLvlObj, raceRecThisLvlObj, raceRecNrOppObj;
    Text lapRecEverText, lapRecThisLvlText, raceRecThisLvlText, raceRecNrOppText;

    [SerializeField]
    GameObject raceRecNrOppTitleObj;
    Text raceRecNrOppTitleText;

    [SerializeField]
    GameObject tapForLvlInfoObj;

    [SerializeField]
    GameObject textWonAfterCountLvlInfoObj, textWonAfterCountLvlInfoShadowObj;
    Text textWonAfterCountLvlInfo, textWonAfterCountLvlInfoShadow;


    void Start(){

        textNrLvl = textNrLvlObj.GetComponent<Text>();
        textPrize = textPrizeObj.GetComponent<Text>();

        lapRecEverText = lapRecEverTextObj.GetComponent<Text>();
        lapRecThisLvlText = lapRecThisLvlObj.GetComponent<Text>();
        raceRecThisLvlText = raceRecThisLvlObj.GetComponent<Text>();
        raceRecNrOppText = raceRecNrOppObj.GetComponent<Text>();

        raceRecNrOppTitleText = raceRecNrOppTitleObj.GetComponent<Text>();

        textWonAfterCountLvlInfo = textWonAfterCountLvlInfoObj.GetComponent<Text>();
        textWonAfterCountLvlInfoShadow = textWonAfterCountLvlInfoShadowObj.GetComponent<Text>();

        progressDataMgr = progressDataObj.GetComponent<ProgressDataManager>();
        playerDataMgr = playerDataObj.GetComponent<PlayerData>();

        updateStatisticsInterface();
        setStatiscicsInterfaceOff();
    }



// on or off statistics interface
    void setStatiscicsInterfaceOn(){

        gameObject.SetActive(true);
        tapForLvlInfoObj.SetActive(false);
    }



    public void setStatiscicsInterfaceOff(){

        gameObject.SetActive(false);
        tapForLvlInfoObj.SetActive(true);
    }



    public void clickStatisticsOnOffButton(){

        if (gameObject.activeSelf){
            setStatiscicsInterfaceOff();
        }
        else{
            setStatiscicsInterfaceOn();
            updateStatisticsInterface();
        }
    }



//statistics info Update
    void updateTextPrice(int _hwMn){

        textPrize.text = _hwMn.ToString() + "$";
    }



    void updateTextNrLvl(int _hwMn){

        textNrLvl.text = _hwMn.ToString();
    }



    public void updateStatisticsInterface(){

        int tmpActualLvl = playerDataMgr.getActualLvl();
        updateTextNrLvl(tmpActualLvl);
        updateTextPrice(progressDataMgr.moneyForWinLvl(tmpActualLvl));

        /*if (playerDataMgr.getLapRecord() < 9999){
            setTextTime(lapRecEverText, playerDataMgr.getLapRecord());
        }
        else {      //if no record, when first race
            setTextTime(lapRecEverText, 0f);
        }*/
        updateTimeTextRecord();

        updateTextWonAfterCountLvlInfo(tmpActualLvl);
    }




//info time records
    void setTextTime(Text _textTime, float _time){

        int tmpMin, tmpSec, tmpMill;
        string tmpMinStr, tmpSecStr, tmpMillStr;

        tmpMin = (int)(_time/60f);
        tmpSec = (int)(_time%60);
        tmpMill = (int)((_time - Mathf.Floor(_time)) * 1000);

        tmpMinStr = tmpMin.ToString("00");
        tmpSecStr = tmpSec.ToString("00");
        tmpMillStr = tmpMill.ToString("000");

        _textTime.text = tmpMinStr + ":" + tmpSecStr + ":" + tmpMillStr;
    }



    void updateTimeTextRecord(){

        if (playerDataMgr.getLapRecord() < 9999){
            setTextTime(lapRecEverText, playerDataMgr.getLapRecord());
        }
        else {      //if no record, when first race
            setTextTime(lapRecEverText, 0f);
        }

        if (playerDataMgr.getActualLvlLapRecord() < 9999){
            setTextTime(lapRecThisLvlText, playerDataMgr.getActualLvlLapRecord());
        }
        else {      //if no record, when first race
            setTextTime(lapRecThisLvlText, 0f);
        }

        if (playerDataMgr.getActualLvlRecord() < 9999){
            setTextTime(raceRecThisLvlText, playerDataMgr.getActualLvlRecord());
        }
        else {      //if no record, when first race
            setTextTime(raceRecThisLvlText, 0f);
        }

        if (playerDataMgr.getNrOppRecord() < 9999){
            setTextTime(raceRecNrOppText, playerDataMgr.getNrOppRecord());
        }
        else {      //if no record, when first race
            setTextTime(raceRecNrOppText, 0f);
        }

        updateRaceRecNrOppTitleText();
    }



    void updateRaceRecNrOppTitleText(){

        int nrOppTmp = playerDataMgr.getActualLvl() % 10;
        if(nrOppTmp == 0) {nrOppTmp = 10;};
        raceRecNrOppTitleText.text = "best race " + nrOppTmp + " foe";
    }



    void updateTextWonAfterCountLvlInfo(int _actualLvl){

        bool tmpShowMoreStatsFL = playerDataMgr.getOptMoreStats();

        if(tmpShowMoreStatsFL && playerDataMgr.getLvlRaceCountFL(_actualLvl)){

            textWonAfterCountLvlInfoObj.SetActive(true);
            int tmpNrRace = playerDataMgr.getLvlRaceCount(_actualLvl);
            textWonAfterCountLvlInfo.text = "won after " + tmpNrRace.ToString() + "th race";
            textWonAfterCountLvlInfoShadow.text = "won after " + tmpNrRace.ToString() + "th race";
        }
        else{
            textWonAfterCountLvlInfoObj.SetActive(false);
        }
    }

}

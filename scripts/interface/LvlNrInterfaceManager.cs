using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Collections;

public class LvlNrInterfaceManager : MonoBehaviour {

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject mainMenuInterfaceObj;
    MainMenuManager mainMenuMngr;

    [SerializeField]
    GameObject playerDataObj;
    PlayerData playerData;

    [SerializeField]
    GameObject nrLvlTextObj;
    Text nrLvlText;

    [SerializeField]
    GameObject gameMngrObj;
    GameMngr gameMngr;

    [SerializeField]
    GameObject textMoneyObj;
    Text textMoney;

    [SerializeField]
    GameObject upgradesObj;
    UpgradesManager upgrades;

    [SerializeField]
    GameObject  preLvlBtn, nextLvlBtn, topGroundObj;

    [SerializeField]
    GameObject statisticsInterfaceObj;
    StatisticsInterfaceManager statisticsInterface;

    AdvManager advMgr;

    int actualLvl, minLvl, maxLvl,
        actualNrTrack = 0;

    bool backBtnFL, trophyMapBtnFL;

    [SerializeField]
    SpriteAtlas     soundBtnAtlas;
    [SerializeField]
    GameObject      soundBtnObj;
    [SerializeField]
    GameObject      trophyMapInterfaceObj;

    [SerializeField]
    GameObject      shopObj;
    ShopManager     shopMgr;
    bool            shopFL, shopOffFL = true;

    [SerializeField]
    GameObject      bumniBtnObj;
    [SerializeField]
    SpriteAtlas     lookAllInOneLvlNrInterfaceAtlas;

    [SerializeField]
    GameObject      trackBtnObj;
    LvlNrInterfaceTrackBtnMnr   trackBtnMnr;

    [SerializeField]
    GameObject      advBtnLvlNrMgrObj;
    LvlNrInterfaceAdButtonManager   advBtnLvlNrMgr;


    void Start(){

        mainMenuMngr = mainMenuInterfaceObj.GetComponent<MainMenuManager>();
        playerData = playerDataObj.GetComponent<PlayerData>();
        actualNrTrack = playerData.getActualTrack();
        actualLvl = playerData.getActualLvl();
        minLvl = playerData.getMinLvl();
        maxLvl = playerData.getMaxLvl();
        nrLvlText = nrLvlTextObj.GetComponent<Text>();
        setNrLvlText();
        checkRangeNrLvl();
        gameMngr = gameMngrObj.GetComponent<GameMngr>();
        textMoney = textMoneyObj.GetComponent<Text>();
        upgrades = upgradesObj.GetComponent<UpgradesManager>();
        advMgr = gameMngr.getAdvMgr();
        statisticsInterface = statisticsInterfaceObj.GetComponent<StatisticsInterfaceManager>();
        checkSoundsBtnOption();

        shopMgr = shopObj.GetComponent<ShopManager>();

        trackBtnMnr = trackBtnObj.GetComponent<LvlNrInterfaceTrackBtnMnr>();
        advBtnLvlNrMgr = advBtnLvlNrMgrObj.GetComponent<LvlNrInterfaceAdButtonManager>();
    }



    void Update(){

        checkTextScale();
    }



    void checkTextScale(){

        if(textMoneyObj.transform.localScale.y > 1){
            StartCoroutine(checkScaleTextMoney());
        }

        if(nrLvlTextObj.transform.localScale.x > 1){
            StartCoroutine(checkScaleTextLevel());
        }
    }



    public void setNrLvlText(){

        if(nrLvlText.text != actualLvl.ToString()){
            nrLvlText.text = actualLvl.ToString();
            nrLvlTextObj.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
        }
    }


    IEnumerator checkScaleTextLevel(){

        yield return new WaitForSeconds(0.02f);
        nrLvlTextObj.transform.localScale -= new Vector3(.005f, .005f, .005f);
    }



    public int getActualLvl(){

        return actualLvl;
    }



//*****************
//*** BUTTONS ***

    public void addNrLvl(){

        actualLvl += 1;
        playerData.setActualLvl(actualLvl);
        setNrLvlText();
        checkRangeNrLvl();
        updateStatisticsInterface();
    }



    public void oddNrLvl(){

        actualLvl -= 1;
        playerData.setActualLvl(actualLvl);
        setNrLvlText();
        checkRangeNrLvl();
        updateStatisticsInterface();
    }



    void checkRangeNrLvl(){

        preLvlBtn.SetActive(true);
        nextLvlBtn.SetActive(true);

        if (actualLvl < minLvl+1){
            preLvlBtn.SetActive(false);
        }
        if (actualLvl > maxLvl-1){
            nextLvlBtn.SetActive(false);
        }
    }



    public void refreshLvlInfo(){

        actualLvl = playerData.getActualLvl();
        minLvl = playerData.getMinLvl();
        maxLvl = playerData.getMaxLvl();
        setNrLvlText();
        checkRangeNrLvl();
    }



    void startLvl(){

        gameMngr.loadLvl(actualNrTrack, actualLvl);
        gameMngr.raceInterfaceStart();
    }



    public void backBtn(){

        backBtnFL = true;
        animatorStartHide();
    }



    void updateStatisticsInterface(){

        if(statisticsInterfaceObj.activeSelf){
            statisticsInterface.updateStatisticsInterface();
        }
    }



    public void soundsBtn(){

        Debug.Log("soundBtn...");
        bool tmpSoundsAllFL = playerData.getOptSoundSoundsAll();
        playerData.setOptSoundSoundsAll(!tmpSoundsAllFL);
        checkSoundsBtnOption();
    }



    void checkSoundsBtnOption(){

        if (playerData == null){
            playerData = playerDataObj.GetComponent<PlayerData>();
        }

        if (playerData.getOptSoundSoundsAll()){
            AudioListener.volume = 1f;
            soundBtnObj.GetComponent<Image>().sprite = soundBtnAtlas.GetSprite("soundButtonON");
        }
        else{
            AudioListener.volume = 0f;
            soundBtnObj.GetComponent<Image>().sprite = soundBtnAtlas.GetSprite("soundButtonOFF");
        }
    }



    public void trophyMapBtn(){

        trophyMapBtnFL = true;
        animatorStartHide();
    }



//******************
//*** MONEY ***

    public void refreshTextMoney(){

        int tmpHwMn = playerData.getMoney();
        if (tmpHwMn > 999999) {tmpHwMn = 999999;}

        if (textMoney.text != tmpHwMn.ToString()){
            textMoney.text = tmpHwMn.ToString();
            //textMoneyObj.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            textMoneyObj.transform.localScale = new Vector3(1f, 1.25f, 1f);
        }
    }



    IEnumerator checkScaleTextMoney(){

        yield return new WaitForSeconds(0.02f);
        //textMoneyObj.transform.localScale -= new Vector3(.01f, .01f, .01f);
        textMoneyObj.transform.localScale -= new Vector3(0f, .01f, 0f);
    }


//******************
//*** UPDATES ***

    public void refreshTextUpgrades(){

        upgrades.refreshTextUpgrades();
    }



//******************
//*** ANIMATOR ***

    public void animatorStartShow(){

        topGroundObj.SetActive(true);
        gameObject.SetActive(true);
        animator.SetBool("showFL", true);
        checkSoundsBtnOption();
        checkBumniButtonImage();
        refreshTrackBtn();
        //refreshTextMoney();
    }



    public void animatorEndShow(){

        animator.SetBool("showFL", false);
        topGroundObj.SetActive(false);
        refreshTextMoney();
        refreshTextUpgrades();
        refreshLvlInfo();
        gameMngr.setMainCameraAudioListener(true);
        advBtnLvlNrMgr.refresh();
    }



    public void animatorStartHide(){

        if (!backBtnFL && !trophyMapBtnFL && !shopFL) {advMgr.checkAdv();}
        topGroundObj.SetActive(true);
        animator.SetBool("hideFL", true);
    }



    public void animatorEndHide(){

        animator.SetBool("hideFL", false);
        topGroundObj.SetActive(false);
        //statisticsInterfaceObj.SetActive(false);
        if (!trophyMapBtnFL) {
            statisticsInterface.setStatiscicsInterfaceOff();
        }

        if (!shopFL){
            gameObject.SetActive(false);
        }

        if (shopFL){
            if (!shopOffFL) {
                shopMgr.setOnLvlInterfaceStuff(false);
                shopOffFL = true;
            }
            else{
                shopMgr.setOnLvlInterfaceStuff(true);
                backBtnFL = false;
                shopFL = false;
            }
            animatorStartShow();
        }
        else if (!backBtnFL && !trophyMapBtnFL){
            startLvl();
        }
        else if(backBtnFL){
            backBtnFL = false;
            mainMenuMngr.animatorStartShowMenu();
        }
        else{
            trophyMapBtnFL = false;
            trophyMapInterfaceObj.GetComponent<TrophyMapInterfaceManager>().animatorShowStart();
        }

        advBtnLvlNrMgr.endHide();

    }



    public void animatorUpgradeBumniStart(){

        animator.SetBool("upgradeBumniFL", true);
    }



    public void animatorUpgradeBumniEnd(){

        animator.SetBool("upgradeBumniFL", false);
    }



    public void animatorLeaderboardsWaitStart(){

        animator.SetBool("leaderboardWaitFL", true);
    }

    public void animatorLeaderboardsWaitEnd(){

        animator.SetBool("leaderboardWaitFL", false);
    }



    public void animatorLeaderboardsErrorStart(){

        animator.SetBool("leaderboardErrorFL", true);
    }

    public void animatorLeaderboardsErrorEnd(){

        animatorLeaderboardsWaitEnd();
        animator.SetBool("leaderboardErrorFL", false);
    }



    public void animatorMoneyNoEnoughStart(){

        if(!animator.GetBool("moneyNoEnoughFL")){

            animator.SetBool("moneyNoEnoughFL", true);
        }
    }


    public void animatorMoneyNoEnoughEnd(){

        animator.SetBool("moneyNoEnoughFL", false);
    }



    public int getActualNrTrack(){

        actualNrTrack = playerData.getActualTrack();
        return actualNrTrack;
    }

    public void setActualNrTrack(int _hwMn){

        actualNrTrack = _hwMn;
        playerData.setActualTrack(actualNrTrack);
    }




    public void developResetBtn(){

        playerData.developResetBtn();
        refreshTextUpgrades();
    }



    public int checkShopState(string _name){

        return playerData.shopStuffGetState(_name);
    }



    public void bumniShopBtn(){

        shopOffFL = false;
        shopFL = true;
        animatorStartHide();
    }



    void checkBumniButtonImage(){

        bumniBtnObj.GetComponent<Image>().sprite = lookAllInOneLvlNrInterfaceAtlas.GetSprite( playerData.shopStuffGetActive("look") );
    }



    void refreshTrackBtn(){
        if(trackBtnMnr != null){
            trackBtnMnr.refreshActualTrackNr();
        }
        else{
            trackBtnObj.GetComponent<LvlNrInterfaceTrackBtnMnr>().refreshActualTrackNr();
        }
    }


    public void shopStuffTrackActive(int _trackNr){
        string tmpName = "track" + _trackNr.ToString("00");
        Debug.Log("tmpName: " + tmpName);
        playerData.shopStuffSetActive("track", tmpName);
    }

}

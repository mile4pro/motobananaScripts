using UnityEngine;
using UnityEngine.Advertisements;


public class AdvManager : MonoBehaviour {

    [SerializeField]
    GameObject playerDataObj;
    PlayerData playerData;

    int advCnt, advCntFin = 7, advCntSkp = 3;

    public delegate void setOnOffDelegateType(bool _FL, bool _adOKFL);
    setOnOffDelegateType setOnOffDelegate;
    int     hwMnCashForAdv = 0;

    public delegate void refreshTxtMoneyLvlInterfaceDelegateType();
    refreshTxtMoneyLvlInterfaceDelegateType     refreshMoneyTxtDelegate;

    void Start(){
        playerData = playerDataObj.GetComponent<PlayerData>();
    }


    int checkAdvCnt(){
        advCnt = playerData.getAdvCnt();
        if (advCnt > advCntFin-1){
            advCnt = -1;
            playerData.setAdvCnt(-1);
            return advCnt;
        }
        return advCnt;
    }


    public void checkAdv(){

        Debug.Log("playerData.getAdvCnt(): " + playerData.getAdvCnt());
        playerData.oddAdvCnt();
        if(checkAdvCnt() < 0 && Advertisement.IsReady()){
            Time.timeScale = 0;
            Advertisement.Show("", new ShowOptions(){resultCallback = HandleAdResult});
        }

    }



//adv btn

    public bool chackAdvBtnEndRaceIsReady(setOnOffDelegateType _setOnOff){

        if(Advertisement.IsReady()){

            _setOnOff(true, false);    //on btn
            return true;
        }
        else{

            _setOnOff(false, false);   //off btn
            return false;
        }
    }


    public bool checkAdvBtnEndRace(int _hwMnCashForAdv, setOnOffDelegateType _setOnOff){

        setOnOffDelegate = _setOnOff;
        hwMnCashForAdv = _hwMnCashForAdv;

        if(Advertisement.IsReady()){

            Time.timeScale = 0;
            Advertisement.Show("", new ShowOptions(){resultCallback = HandleAdResultAdvBtnEndRace});
            return true;
        }
        else{

            setOnOffDelegate(false, false);
            return false;
        }
    }



    void HandleAdResultAdvBtnEndRace(ShowResult result){

        switch(result){

            case ShowResult.Finished:
                Time.timeScale = 1;
                Debug.Log("add Finished");
                playerData.setAdvCnt(advCntFin);
                playerData.addMoney(hwMnCashForAdv);
                hwMnCashForAdv = 0;
                setOnOffDelegate(false, true);
            break;


            case ShowResult.Skipped:
                Time.timeScale = 1;
                Debug.Log("add Skipped");
                playerData.setAdvCnt(advCntSkp);
                hwMnCashForAdv = 0;
                setOnOffDelegate(false, false);
            break;


            case ShowResult.Failed:
                Time.timeScale = 1;
                Debug.Log("add Failed");
                advCnt = 3;
                hwMnCashForAdv = 0;
                setOnOffDelegate(false, false);
            break;


            default:
            break;
    }
}
//**********adv btn end region



    private void HandleAdResult(ShowResult result){

        switch(result){

            case ShowResult.Finished:
                Time.timeScale = 1;
                Debug.Log("add Finished");
                playerData.setAdvCnt(advCntFin);
            break;


            case ShowResult.Skipped:
                Time.timeScale = 1;
                Debug.Log("add Skipped");
                playerData.setAdvCnt(advCntSkp);
            break;


            case ShowResult.Failed:
                Time.timeScale = 1;
                Debug.Log("add Failed");
                advCnt = 3;
            break;


            default:
            break;

        }
    }



//ad btn nrLvlInterface
    public void playAdLvlNrInterfaceBtn(refreshTxtMoneyLvlInterfaceDelegateType _refreshTextMoney){

        refreshMoneyTxtDelegate = _refreshTextMoney;

        if(Advertisement.IsReady()){

            Time.timeScale = 0;
            Advertisement.Show("", new ShowOptions(){resultCallback = HandleAdResultLvlNrInterfaceBtn});
        }
        else{


        }
    }



    void HandleAdResultLvlNrInterfaceBtn(ShowResult result){

        switch(result){

            case ShowResult.Finished:
                Time.timeScale = 1;
                Debug.Log("add Finished");
                playerData.setAdvCnt(advCntFin);
                playerData.addMoney(playerData.getAdvPriceSmall());
                refreshMoneyTxtDelegate();
            break;


            case ShowResult.Skipped:
                Time.timeScale = 1;
                Debug.Log("add Skipped");
                playerData.setAdvCnt(advCntSkp);
            break;


            case ShowResult.Failed:
                Time.timeScale = 1;
                Debug.Log("add Failed");
                advCnt = 3;
            break;


            default:
            break;
        }
    }




    public bool checkAdReady(){

        return Advertisement.IsReady();
    }
}

using UnityEngine;
using UnityEngine.UI;



public class AdvBtnManagerEndRace : MonoBehaviour {

    [SerializeField]
    GameObject    textHwMnObj, cashHwMnOKTextObj, gameMngrObj;
    Text          textHwMn;

    int           hwMnCashForAdv;

    public delegate void animationCashAdOkType();
    animationCashAdOkType animationCashAdOk;


    void Start(){

        textHwMn = textHwMnObj.GetComponent<Text>();
    }



    public void tapBtn(){

        gameMngrObj.GetComponent<GameMngr>().getAdvMgr().checkAdvBtnEndRace(hwMnCashForAdv, setOnOff);
    }



    public void checkAdv(int _playerPlace, int _actualLvl, int moneyForWinThisLvl, animationCashAdOkType _animationCashAdOk){

        cashHwMnOKTextObj.SetActive(false);
        animationCashAdOk = _animationCashAdOk;
        hwMnCashForAdv = 0;
        gameMngrObj.GetComponent<GameMngr>().getAdvMgr().chackAdvBtnEndRaceIsReady(setOnOff);
        //Debug.Log("<---- checkAdv ---->");
        //Debug.Log("_playerPlace: " + _playerPlace + ", _actualLvl: " + _actualLvl + ", moneyForWinThisLvl: " + moneyForWinThisLvl);

        int tmpHwMnCashForAdv = moneyForWinThisLvl < 10 ? 10 : (int)(moneyForWinThisLvl * 0.99f);

        //Debug.Log("tmpHwMnCashForAdv: " + tmpHwMnCashForAdv);
        int tmpNumberOpponents = (_actualLvl % 10) == 0 ? 10 : (_actualLvl % 10);
        //Debug.Log("tmpNumberOpponents: " + tmpNumberOpponents);

        if(_playerPlace > 1){

            float tmpRatioPlace = ((float)(tmpNumberOpponents + 1 - _playerPlace)) / ((float)(tmpNumberOpponents + 1));
            //Debug.Log("tmpRatioPlace: " + tmpRatioPlace);
            if (tmpRatioPlace == 0) {tmpRatioPlace = 1f/11f;}
            //Debug.Log("tmpRatioPlace: " + tmpRatioPlace);
            tmpHwMnCashForAdv = (int)(moneyForWinThisLvl * tmpRatioPlace);
            //Debug.Log("tmpHwMnCashForAdv: " + tmpHwMnCashForAdv);
            if (tmpHwMnCashForAdv < 10) {tmpHwMnCashForAdv = 10;}
            //Debug.Log("tmpHwMnCashForAdv: " + tmpHwMnCashForAdv);
        }


        if(textHwMn != null){

            textHwMn.text = '+' + tmpHwMnCashForAdv.ToString() + '$';
        }
        else{

            textHwMnObj.GetComponent<Text>().text = '+' + tmpHwMnCashForAdv.ToString() + '$';
        }

        hwMnCashForAdv = tmpHwMnCashForAdv;

        //Debug.Log("|---- checkAdv ----| ...end");
    }



    public void setOnOff(bool _FL, bool _adOKFL = false){

        gameObject.SetActive(_FL);

        if(_adOKFL){

            cashHwMnOKTextObj.GetComponent<Text>().text = '+' + hwMnCashForAdv.ToString() + '$';
            cashHwMnOKTextObj.SetActive(true);
            animationCashAdOk();
        }

    }


}

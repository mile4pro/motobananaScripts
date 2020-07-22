using UnityEngine;
using UnityEngine.UI;

public class UpBellManager : MonoBehaviour {

    [SerializeField]
    GameObject  textNrLevelObj, textMoneyObj, playerDataObj;

    //Text textNrLevel, textMoney;

    //int actualLvl;


    /*void Start(){
        textNrLevel = textNrLevelObj.GetComponent<Text>();
        textMoney = textMoneyObj.GetComponent<Text>();
        //setTextNrLevel(actualLvl);
    }*/


    public void setTextNrLevel(int _nrLvl){
        //textNrLevel.text = _nrLvl.ToString();
        textNrLevelObj.GetComponent<Text>().text = _nrLvl.ToString();
        //actualLvl = _nrLvl;
    }


    public void setTextMoney(int _hwMn){
        //textMoney.text = _hwMn.ToString();
        int tmpHwMn = _hwMn;
        if (tmpHwMn > 999999) {tmpHwMn = 999999;}
        textMoneyObj.GetComponent<Text>().text = tmpHwMn.ToString();
    }

    public void refreshTextHwMnMoney(){
        int tmpMoney = playerDataObj.GetComponent<PlayerData>().getMoney();
        setTextMoney(tmpMoney);
    }

}

using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour {

    [SerializeField]
    GameObject              progressDataObj;
    ProgressDataManager     progressData;
    PlayerData              playerData;

    [SerializeField]
    GameObject              textEngineLvlObj, textEngineMoneyObj,
                            textMassLvlObj, textMassMoneyObj;

    Text                    textEngineLvl, textEngineMoney,
                            textMassLvl, textMassMoney;

    [SerializeField]
    GameObject              lvlNrInterfaceObj, audioClipsObj;

    int                     maxLvlEngine = 30, maxLvlMass = 25;



    void Start(){

        progressData = progressDataObj.GetComponent<ProgressDataManager>();
        playerData = progressData.getPlayerData();

        textEngineLvl = textEngineLvlObj.GetComponent<Text>();
        textEngineMoney = textEngineMoneyObj.GetComponent<Text>();
        textMassLvl = textMassLvlObj.GetComponent<Text>();
        textMassMoney = textMassMoneyObj.GetComponent<Text>();
    }



    public void setTextEngineLvl(int _lvl){

        textEngineLvl.text = _lvl.ToString();
    }



    public void setTextEngineMoney(int _hwMn, bool _maxLvlFL = false){

        if (_maxLvlFL){

            textEngineMoney.text = "max  ";
        }
        else{

            textEngineMoney.text = _hwMn.ToString();
        }
    }



    public void setTextMassLvl(int _lvl){

        textMassLvl.text = _lvl.ToString();
    }



    public void setTextMassMoney(int _hwMn, bool _maxLvlFL = false){

        if (_maxLvlFL){

            textMassMoney.text = "max  ";
        }
        else{

            textMassMoney.text = _hwMn.ToString();
        }
    }



    public void refreshTextUpgrades(){

        int tmpEngineLvl = playerData.getEnginePowerLvl();
        int tmpMassLvl = playerData.getMassLvl();

        int tmpEngineMoney = progressData.moneyForUpgrade(tmpEngineLvl);
        int tmpMassMoney = progressData.moneyForUpgrade(tmpMassLvl);

        setTextEngineLvl(tmpEngineLvl);
        setTextEngineMoney(tmpEngineMoney, maxLvlEngine < tmpEngineLvl);

        setTextMassLvl(tmpMassLvl);
        setTextMassMoney(tmpMassMoney, maxLvlMass < tmpMassLvl);

    }



//*********************************************
//***********   BUTTONS  *****************

    public void buttonUpgradeEngine(){

        int tmpMoney = playerData.getMoney();
        int tmpEngineLvl = playerData.getEnginePowerLvl();
        int tmpCostEngine = progressData.moneyForUpgrade(tmpEngineLvl);


        if (tmpEngineLvl > maxLvlEngine){

            Debug.Log("max engine level...");
        }
        else if (tmpMoney < tmpCostEngine){

            Debug.Log("NO ENOUGH MONEY FOR ENGINE UPGRADE");
            lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().animatorMoneyNoEnoughStart();
        }
        else{

            upgradeEffects();
            playerData.updateEnginePowerLvl();
            playerData.takeMoney(tmpCostEngine);
            refreshTextUpgrades();
            lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().refreshTextMoney();
        }
    }



    public void buttonUpgradeMass(){

        int tmpMoney = playerData.getMoney();
        int tmpMassLvl = playerData.getMassLvl();
        int tmpCostMass = progressData.moneyForUpgrade(tmpMassLvl);

        if (tmpMassLvl > maxLvlMass){

            Debug.Log("max mass level...");
        }
        else if (tmpMoney < tmpCostMass){

            Debug.Log("NO ENOUGH MONEY FOR MASS UPGRADE");
            lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().animatorMoneyNoEnoughStart();
        }
        else{

            upgradeEffects();
            playerData.updateMassLvl();
            playerData.takeMoney(tmpCostMass);
            refreshTextUpgrades();
            lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().refreshTextMoney();
        }
    }



    void upgradeEffects(){

        audioClipsObj.GetComponent<AudioClipsManager>().playUpgrade01();
        lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().animatorUpgradeBumniStart();
    }
}

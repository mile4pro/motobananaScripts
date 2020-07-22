using UnityEngine;



public class OptionsInterfaceOtherManager : MonoBehaviour
{

    [SerializeField]
    GameObject  developResetBtnObj, areYourSureObj,
                txtAreYourSureObj, txtUpgradesResetObj,
                btnYesObj, btnNoObj, btnOkObj,
                backBtnObj;



    public void clickDevelopResetBtn(){

        developResetBtnObj.SetActive(false);
        areYourSureObj.SetActive(true);

        txtAreYourSureObj.SetActive(true);
        txtUpgradesResetObj.SetActive(false);

        btnYesObj.SetActive(true);
        btnNoObj.SetActive(true);
        btnOkObj.SetActive(false);

        backBtnObj.SetActive(false);
    }



    public void clickNoBtn(){

        developResetBtnObj.SetActive(true);
        areYourSureObj.SetActive(false);

        txtAreYourSureObj.SetActive(false);
        txtUpgradesResetObj.SetActive(false);

        btnYesObj.SetActive(false);
        btnNoObj.SetActive(false);
        btnOkObj.SetActive(false);

        backBtnObj.SetActive(true);
    }



    public void clickYesBtn(){

        developResetBtnObj.SetActive(false);
        areYourSureObj.SetActive(true);

        txtAreYourSureObj.SetActive(false);
        txtUpgradesResetObj.SetActive(true);

        btnYesObj.SetActive(false);
        btnNoObj.SetActive(false);
        btnOkObj.SetActive(true);

        backBtnObj.SetActive(false);
    }



    public void clickOkBtn(){

        clickNoBtn();
    }



}

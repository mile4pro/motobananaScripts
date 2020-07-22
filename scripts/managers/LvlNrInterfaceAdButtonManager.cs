using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class LvlNrInterfaceAdButtonManager : MonoBehaviour {

    [SerializeField]
    GameObject  gameMgrObj, txtHwMnObj, lvlNrInterfaceObj;

    AdvManager      adMnr;

    bool tap1th, tap2th;

    float moveSideX = 95f, moveSideBackTime = 2f, hidePosX = -70f;

    Coroutine moveSideXcourotine;

    [SerializeField]
    Animator animator;

    Text txtHwMn;



    void Start(){

        adMnr = gameMgrObj.GetComponent<GameMngr>().getAdvMgr();
        //animator.SetBool("show1FL", true);
        checkAdReady();
        txtHwMn = txtHwMnObj.GetComponent<Text>();
        setTxtHwMn();
    }



    public void tapBtn(){

        if (tap2th){

        }
        else if(tap1th){

            tap2th = true;
            if (moveSideXcourotine != null) {StopCoroutine(moveSideXcourotine);}
            //gameMgrObj.GetComponent<GameMngr>().getAdvMgr().playAdLvlNrInterfaceBtn(lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().refreshTextMoney);
            adMnr.playAdLvlNrInterfaceBtn(lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().refreshTextMoney);
            MoveSideX(-moveSideX);
            setBool(false);
            animator.SetBool("show1FL", true);
            animator.SetBool("showAllFL", false);
        }
        else{

            //MoveSideX(moveSideX);
            animatorStartShowAll();
            tap1th = true;
        }
    }



    IEnumerator moveSideBack(float _moveSideBackTime){

        Debug.Log("moveSideBack... start");
        yield return new WaitForSeconds(_moveSideBackTime);
        //MoveSideX(-moveSideX);
        animatorStartHide2();
        //setBool(false);
        Debug.Log("moveSideBack... end");
    }



    void MoveSideX(float _moveSideX){

        float tmpPosX = gameObject.transform.localPosition.x + _moveSideX;
        gameObject.transform.localPosition = new Vector2(tmpPosX, gameObject.transform.localPosition.y);
        Debug.Log("MoveSideX...");
    }


    public void endHide(){

        gameObject.transform.localPosition = new Vector2(-1000f, gameObject.transform.localPosition.y);
        //gameObject.SetActive(false);
    }


    void setBool(bool _FL){

        tap1th = _FL;
        tap2th = _FL;
    }



    //animations
    public void animatorStartShow1(){

        animator.SetBool("show1FL", true);
    }

    public void animatorEndShow1(){

        setBool(false);
        animator.SetBool("show1FL", false);
    }



    public void animatorStartShowAll(){

        animator.SetBool("showAllFL", true);
    }

    public void animatorEndShowAll(){

        moveSideXcourotine = StartCoroutine(moveSideBack(moveSideBackTime));
        //animator.SetBool("showAllFL", false);
    }



    public void animatorStartHide2(){

        animator.SetBool("hide2FL", true);
    }

    public void animatorEndHide2(){

        animator.SetBool("hide2FL", false);
        animator.SetBool("showAllFL", false);
        setBool(false);
        checkAdReady();
    }



    void setTxtHwMn(){

        int tmpTxtHwMnInt = gameMgrObj.GetComponent<GameMngr>().getAdvPriceSmall();
        txtHwMn.text = "+" + tmpTxtHwMnInt.ToString() + "$";
    }


    public void refresh(){

        setTxtHwMn();
        //moveHide();
        checkAdReady();
    }



    void checkAdReady(){

        if(adMnr.checkAdReady()){

            animator.SetBool("show1FL", true);
        }
        else{

            endHide();
        }
    }


}

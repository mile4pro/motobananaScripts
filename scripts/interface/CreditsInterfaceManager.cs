using UnityEngine;




public class CreditsInterfaceManager : MonoBehaviour {


    [SerializeField]
    Animator    animator;

    [SerializeField]
    GameObject  topGroundObj, MainMenuObj;

    [SerializeField]
    GameObject  btnOnOtherOptionObj;
    int         btnOnOtherOptionCountClick = 0;



    public void animatorShowStart(){

        gameObject.SetActive(true);
        topGroundObj.SetActive(true);
        animator.SetBool("showFL", true);
        btnOnOtherOptionCountClick = 0;
    }



    public void animatorShowEnd(){

        topGroundObj.SetActive(false);
        animator.SetBool("showFL", false);
    }



    public void animatorHideStart(){

        topGroundObj.SetActive(true);
        animator.SetBool("hideFL", true);
    }



    public void animatorHideEnd(){

        topGroundObj.SetActive(false);
        gameObject.SetActive(false);
        animator.SetBool("hideFL", false);
        MainMenuObj.GetComponent<MainMenuManager>().animatorStartShowMenu();
    }



    public void btnOnOtherOptionClick(){

        btnOnOtherOptionCountClick += 1;
        //Debug.Log("btnOnOtherOptionCountClick: " + btnOnOtherOptionCountClick);
        if(btnOnOtherOptionCountClick > 6){
            btnOnOtherOptionObj.SetActive(true);
            //Debug.Log("btn option other on...");
        }
    }

}

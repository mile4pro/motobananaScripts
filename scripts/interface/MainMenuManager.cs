using UnityEngine;

public class MainMenuManager : MonoBehaviour {

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject  raceInterfaceObj, lvlNrInterfaceObj, topgroundObj,
                playerDataObj, creditsInterfaceObj;

    bool        creditsBtnFL = false;



    void Start(){

        //Screen.SetResolution (640, 480, true);
        //Debug.developerConsoleVisible = true;
        //Debug.LogError("I am an Error");
        checkSoundsAll();
        animatorStartShowMenu();
    }



    public void animatorStartStartBtn(){

        topgroundObj.SetActive(true);
        animator.SetBool("startBtnFL", true);
        raceInterfaceObj.GetComponent<RaceInterfaceManager>().setOff();
    }



    public void animatorEndStartBtn(){

        animator.SetBool("startBtnFL", false);
        topgroundObj.SetActive(false);
        gameObject.SetActive(false);
        //raceInterfaceObj.SetActive(true);
        //raceInterfaceObj.GetComponent<RaceInterfaceManager>().animatorStartFadeOut();
        if (creditsBtnFL){

            creditsBtnFL = false;
            creditsInterfaceObj.GetComponent<CreditsInterfaceManager>().animatorShowStart();
        }
        else{

            lvlNrInterfaceObj.SetActive(true);
            lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().animatorStartShow();
        }
    }



    public void creditsBtn(){

        creditsBtnFL = true;
        animatorStartStartBtn();
    }



    public void animatorStartShowMenu(){

        //raceInterfaceObj.SetActive(false);
        gameObject.SetActive(true);
        topgroundObj.SetActive(true);
        animator.SetBool("menuShowFL", true);
    }



    public void animatorEndShowMenu(){

        //raceInterfaceObj.SetActive(false);
        topgroundObj.SetActive(false);
        animator.SetBool("menuShowFL", false);
    }



    public void appQuit(){

        Application.Quit();
    }



    void checkSoundsAll(){

        bool tmpSoundsAllFL = playerDataObj.GetComponent<PlayerData>().getOptSoundSoundsAll();
        Debug.Log("tmpSoundsAllFL: " + tmpSoundsAllFL);
        if (tmpSoundsAllFL){AudioListener.volume = 1f;}
        else {AudioListener.volume = 0f;}
    }

}

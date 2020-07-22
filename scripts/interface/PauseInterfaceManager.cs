using UnityEngine;

public class PauseInterfaceManager : MonoBehaviour {

    [SerializeField]
    GameObject  lvlNrInterfaceObj,
                raceInterfaceObj,
                gameMngrObj,
                playerDataObj,
                topgroundObj;

    [SerializeField]
    Animator    animator;

    AdvManager advMgr;

    PlayerData playerData;




    void Start(){

        advMgr = gameMngrObj.GetComponent<GameMngr>().getAdvMgr();
        playerData = playerDataObj.GetComponent<PlayerData>();
    }



    public void continueBtn(){

        gameObject.SetActive(false);
        Time.timeScale = 1;
        setSoundsMute(false);
    }



    public void animatorStartEndBtn(){
        //gameMngrObj.GetComponent<GameMngr>().setOffAllCars();
        Time.timeScale = 1;
        topgroundObj.SetActive(true);
        animator.SetBool("disappearFL", true);
        setSoundsMute(true);
    }



    public void animatorEndEndBtn(){

        animator.SetBool("endBtnFL", false);
        topgroundObj.SetActive(false);
        gameObject.SetActive(false);
        raceInterfaceObj.GetComponent<RaceInterfaceManager>().resetRaceData();
        raceInterfaceObj.SetActive(false);
        gameMngrObj.GetComponent<GameMngr>().forceEndLvl();
        lvlNrInterfaceObj.SetActive(true);
        lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().animatorStartShow();
    }



    public void animatorStartRestartBtn(){

        Time.timeScale = 1;
        setSoundsMute(true);
        topgroundObj.SetActive(true);
        animator.SetBool("restartFL", true);
        advMgr.checkAdv();
    }



    public void animatorEndRestartBtn(){

        animator.SetBool("restartFL", false);
        topgroundObj.SetActive(false);
        gameObject.SetActive(false);
        raceInterfaceObj.GetComponent<RaceInterfaceManager>().resetRaceData();
        raceInterfaceObj.SetActive(false);
        gameMngrObj.GetComponent<GameMngr>().forceEndLvl();
        gameMngrObj.GetComponent<GameMngr>().loadLvlRestart();
        gameMngrObj.GetComponent<GameMngr>().raceInterfaceStart();
    }



    void setSoundsMute(bool _FL){

        if (playerData.getOptSoundSoundsAll()){
            if(_FL){
                AudioListener.volume = 0f;
            }
            else{
                AudioListener.volume = 1f;
            }
        }
    }


}

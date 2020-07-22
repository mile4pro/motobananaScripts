using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;

public class RaceInterfaceManager : MonoBehaviour {

    [SerializeField]
    Animator        animator;

    [SerializeField]
    GameObject      topgroundObj, countingDownObj, pauseInterfaceObj,
                    upBellObj, middleBellObj;

    MiddleBellManager   middleBellMngr;

    [SerializeField]
    SpriteAtlas     soundBtnAtlas;
    [SerializeField]
    GameObject      soundBtnObj, playerDataObj;

    [SerializeField]
    GameObject      complimentObj;


    void Start(){

        middleBellMngr = middleBellObj.GetComponent<MiddleBellManager>();
        middleBellMngr.setAnimator(animator);

    }



    public void animatorStartFadeOut(int _actualTrack = 0){

        topgroundObj.SetActive(true);
        animator.SetBool("fadeOutFL", true);
        refreshTextHwMnMoney();
        checkSoundsBtnOption();
        ComplimentManager.resetCollisionCounter(_actualTrack, this);
    }



    public void animatorEndFadeOut(){

        animator.SetBool("fadeOutFL", false);
        topgroundObj.SetActive(false);
        countingDownObj.GetComponent<CountingDownManager>().animatorStartCounting();
    }



    public void gameDevReset(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    public void pauseBtn(){

        Time.timeScale = 0;
        pauseInterfaceObj.SetActive(true);
        //pauseInterfaceObj.GetComponent<PauseInterfaceManager>().setSoundsMute(true);
    }



    public void setTextNrLevel(int _nrLvl){

        upBellObj.GetComponent<UpBellManager>().setTextNrLevel(_nrLvl);
    }



    void refreshTextHwMnMoney(){

        upBellObj.GetComponent<UpBellManager>().refreshTextHwMnMoney();
    }



    public void setBeginTextHwMnLap(int _hwMn){

        //middleBellMngr.setTextHwMnLap(_hwMn);
        middleBellObj.GetComponent<MiddleBellManager>().setBeginTextHwMnLap(_hwMn);
    }



    public void setBeginTextPosition(int _hwMn, int _position){

        //middleBellMngr.setTextHwMnLap(_hwMn);
        middleBellObj.GetComponent<MiddleBellManager>().setBeginTextPosition(_hwMn, _position);
    }



    public MiddleBellManager getMiddleBell(){

        //return middleBellMngr;
        return middleBellObj.GetComponent<MiddleBellManager>();
    }



    public void setRunTimersFL(bool _FL){

        middleBellMngr.setRunTimersFL(_FL);
    }



    public void animatorEndPulseTimeLap(){

        //Debug.Log("1animatorEndPulseTimeLap");
        //Debug.Log("2animatorEndPulseTimeLap");
        //Debug.Log("3animatorEndPulseTimeLap");
        middleBellMngr.endAnimationPulseTimerLap();
        animator.SetBool("pulseLapTimeFL", false);
    }



    public bool resetRaceData(){

        middleBellMngr.setRunTimersFL(false);
        middleBellMngr.resetTimers();
        return true;
    }



    public void soundsBtn(){

        Debug.Log("soundBtn...");
        bool tmpSoundsAllFL = playerDataObj.GetComponent<PlayerData>().getOptSoundSoundsAll();
        playerDataObj.GetComponent<PlayerData>().setOptSoundSoundsAll(!tmpSoundsAllFL);
        checkSoundsBtnOption();
    }



    void checkSoundsBtnOption(){

        if (playerDataObj.GetComponent<PlayerData>().getOptSoundSoundsAll()){
            AudioListener.volume = 1f;
            soundBtnObj.GetComponent<Image>().sprite = soundBtnAtlas.GetSprite("soundButtonON");
        }
        else{
            AudioListener.volume = 0f;
            soundBtnObj.GetComponent<Image>().sprite = soundBtnAtlas.GetSprite("soundButtonOFF");
        }
    }



    public void setRaceInterfacePosition(TrackManager _trackMgr){

        getMiddleBell().setRaceInterfacePosition(_trackMgr);
        _trackMgr.setCountingDownPosition(countingDownObj);
    }



    public GameObject getComplimentObj(){

        return complimentObj;
    }



    public void setOff(){

        complimentObj.GetComponent<ComplimentManager>().setOffComplimentText();
        gameObject.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RaceVisualEffectsManager : MonoBehaviour {

    [SerializeField]
    GameObject MetaParticleEffectLeftObj, MetaParticleEffectRightObj;
    ParticleSystem metaParticleEffectLeft, metaParticleEffectRight;

    [SerializeField]
    GameObject MetaCameraObj;
    Camera metaCamera;

    bool    firstFL = true, timeDeltaFL = true, playerFL = false,
            metaCameraFL = true, playerFirstFL = false;

    float   timeDelta = 0.2f, timeFirst = 0f,
            timeMetaCameraStart = 0f, timeMetaCameraLenght = 0.05625f;     //timeMetaCameraLenght this is 2.25sec because 2.25sec*0.025(time slow motion scale)

    [SerializeField]
    GameObject  textTimeDifferenceaObj;
    Text        textTimeDifference;
    float       timeDifference = 99f;

    [SerializeField]
    GameObject  RaceInterfaceObj;

    [SerializeField]
    AudioSource fireWorks01;


    void Start(){

            metaParticleEffectLeft = MetaParticleEffectLeftObj.GetComponent<ParticleSystem>();
            metaParticleEffectRight = MetaParticleEffectRightObj.GetComponent<ParticleSystem>();

            metaCamera = MetaCameraObj.GetComponent<Camera>();
            textTimeDifference = textTimeDifferenceaObj.GetComponent<Text>();
    }



    public bool playMetaVisualParticleEffect(float _timeRace, bool _playerFL){

        if (firstFL){

            firstFL = false;

            timeFirst = _timeRace;
            playerFL = _playerFL;
            if (playerFL) {playerFirstFL = true;}

            StartCoroutine(playMetaParticleEffect());
            return true;
        }
        else if (metaCameraFL){

            if(!playerFL) {playerFL = _playerFL;}
            if(playerFL) {
                timeDifference = _timeRace - timeFirst;
                if(timeDifference < timeDelta){
                    metaCameraOn();
                }
                else{
                    metaCameraFL = false;
                }
            }
        }

        return false;
    }



    public void setFirstFL(bool _FL){

        firstFL = _FL;
        resetMetaEffectValues();
    }



    void resetMetaEffectValues(){

        timeDeltaFL = true;
        playerFL = false;
        timeFirst = 0f;

        metaCameraFL = true;
        timeMetaCameraStart = 0f;

        timeDifference = 99f;

        playerFirstFL = false;
    }



    void metaCameraOn(){

        MetaCameraObj.SetActive(true);
        RaceInterfaceObj.SetActive(false);
        metaCameraFL = false;
        setTextTimeDifference();
        metaCamera.depth = 0f;
        Time.timeScale = 0.025f;
        Time.fixedDeltaTime *= 0.025f;
        StartCoroutine(metaCameraOff());
    }


    IEnumerator metaCameraOff(){

        yield return new WaitForSeconds(timeMetaCameraLenght);
        MetaCameraObj.SetActive(false);
        RaceInterfaceObj.SetActive(true);
        metaCamera.depth = -2f;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.015f;
    }



    IEnumerator playMetaParticleEffect(){

        yield return new WaitForSeconds(.225f);
        metaParticleEffectLeft.Play();
        metaParticleEffectRight.Play();
        fireWorks01.Play();
    }



    void setTextTimeDifference(){

        int tmpMiliS = (int)(timeDifference*1000);
        if (tmpMiliS == 0) {tmpMiliS = 1;}

        string tmpSign = "+";
        Color tmpFontColorGreen = new Color(0f, 0.5f, 0f, 1f);   //green
        Color tmpFontColorRed = new Color(0.75f, 0f, 0f, 1f);   //red
        textTimeDifference.color = tmpFontColorRed;

        if (playerFirstFL){

            tmpSign = "-";
            textTimeDifference.color = tmpFontColorGreen;
        }

        textTimeDifference.text = tmpSign + "00:00:" + tmpMiliS.ToString("000");
    }



    public void setRaceInterfacePosition(TrackManager _trackMgr){

        _trackMgr.setMetaEffectPosition(MetaParticleEffectLeftObj, MetaParticleEffectRightObj, MetaCameraObj);
    }


}

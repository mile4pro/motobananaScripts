using System.Collections;
using UnityEngine;
using UnityEngine.U2D;



public class GrandpaManager : MonoBehaviour {


    [SerializeField]
    SpriteAtlas     grandpaAtlas;

    [SerializeField]
    GameObject      eyeBckrObj, eyesObj, eyeLObj, eyeRObj,
                    eyebrownLObj, eyebrownRObj,
                    bodyObj, eyelidsObj;

    SpriteRenderer  eyelidsSR;
    Sprite[]        eyelids;
    int             eyelidsFrameNr = 0,
                    eyelidsFrameDirection = 1;
    bool            eyelidsAnimationFrameFL = true, randomBlinkFL = true, blinkFL;

    int             targetIndex = 0, bumniIndex = 0;
    GameObject[]    opponent;
    float           tmpEyePosX, tmpEyePosY,
                    randomTargetWaitTime = 5f,
                    maxEyeDistanceDelta, eyeSpeed = 0.05f;
    bool            randomTargetFL = true;
    Vector3         targetEyePosition;


	void Start () {

        eyelidsSR = eyelidsObj.GetComponent<SpriteRenderer>();
        loadSpritesImg();
        loadEyeLidsAnimationArr();
	}



	void Update () {

        if (blinkFL && eyelidsAnimationFrameFL){

            StartCoroutine(eyelidsAnimation());
        }
        else if (randomBlinkFL){

            StartCoroutine(randomBlink());
        }

        lookTarget();

        if(randomTargetFL){

            StartCoroutine(randomTarget());
        }
	}



    IEnumerator eyelidsAnimation(){

        eyelidsAnimationFrameFL = false;
        yield return new WaitForSeconds(0.04f);

        if (eyelidsFrameNr > eyelids.Length - 1){
            eyelidsFrameNr = eyelids.Length - 1;
            eyelidsFrameDirection = -1;
        }
        else if(eyelidsFrameNr < 0){
            blinkFL = false;
            eyelidsFrameNr = 0;
            eyelidsFrameDirection = 1;
        }

        //Debug.Log("eyelidsAnimation frame: " + eyelidsFrameNr);

        eyelidsSR.sprite = eyelids[eyelidsFrameNr];
        eyelidsFrameNr += eyelidsFrameDirection;

        eyelidsAnimationFrameFL = true;

    }



    IEnumerator randomBlink(){
        randomBlinkFL = false;
        yield return new WaitForSeconds(1f);
        if(Random.value < 0.15){
            blinkFL = true;
        }
        randomBlinkFL = true;
    }



    void loadSpritesImg(){

        eyeBckrObj.GetComponent<SpriteRenderer>().sprite = grandpaAtlas.GetSprite("eyeBckr");
        eyeLObj.GetComponent<SpriteRenderer>().sprite = grandpaAtlas.GetSprite("eyeL");
        eyeRObj.GetComponent<SpriteRenderer>().sprite = grandpaAtlas.GetSprite("eyeR");
        eyebrownLObj.GetComponent<SpriteRenderer>().sprite = grandpaAtlas.GetSprite("brL");
        eyebrownRObj.GetComponent<SpriteRenderer>().sprite = grandpaAtlas.GetSprite("brR");
        bodyObj.GetComponent<SpriteRenderer>().sprite = grandpaAtlas.GetSprite("body");
        eyelidsObj.GetComponent<SpriteRenderer>().sprite = grandpaAtlas.GetSprite("brQ1");
    }



    void loadEyeLidsAnimationArr(){

        eyelids = new Sprite[5];

        for (int i=0; i<eyelids.Length; i++){

            int tmpNr = i + 1;
            eyelids[i] = grandpaAtlas.GetSprite("brQ" + tmpNr.ToString());
        }
    }



    public void setOpponent(GameObject[] _opponent){

        if(opponent != null){opponent = null;}
        opponent = _opponent;
        targetIndex = opponent.Length - 1;
        bumniIndex = targetIndex;
    }



    void lookTarget(){

        //Debug.Log("opponent[targetIndex].localPosition.x: " + opponent[targetIndex].transform.localPosition.x);
        tmpEyePosX = opponent[targetIndex].transform.localPosition.x * 0.0085f;
        tmpEyePosY = (opponent[targetIndex].transform.localPosition.y * (-0.00286f)) + 0.305f;
        targetEyePosition = new Vector3(tmpEyePosX, tmpEyePosY, eyesObj.transform.localPosition.z);
        maxEyeDistanceDelta = eyeSpeed * Time.deltaTime;
        eyesObj.transform.localPosition = Vector3.MoveTowards(eyesObj.transform.localPosition, targetEyePosition, maxEyeDistanceDelta);
    }



    IEnumerator randomTarget(){

        randomTargetFL = false;
        yield return new WaitForSeconds(randomTargetWaitTime);
        if(Random.value < 0.25f){
            targetIndex = Random.Range(0, bumniIndex);
        }
        if(targetIndex != bumniIndex){
            randomTargetWaitTime = 0.5f;
        }
        else{
            randomTargetWaitTime = 5f;
        }
        randomTargetFL = true;
    }



    public void randomRotation(){

        float tmpRotZ = gameObject.transform.localRotation.eulerAngles.z;
        tmpRotZ += Random.Range(0f, 10f);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, tmpRotZ));
    }


}

using UnityEngine;



public class ComplimentManager : MonoBehaviour
{


    static int  collHwMnBands = 0, collHwMnOpponents = 0,
                sensorsHwMn = 0, sensorNr = 0, sensorLastColl = 0,
                sensorLastNr = -1, sensorNoCollisionCount = 0,
                trackNr = 0;

    static Animator animator;

    static RaceInterfaceManager raceIntMgr;

    [SerializeField]
    GameObject  compliment01text01Obj, compliment01text02Obj,
                compliment02text01Obj, compliment02text02Obj;




    void Start(){

        animator = GetComponent<Animator>();
        animatorComplimet01Start();
        animatorComplimet02Start();
        //setOffComplimentText();
        //gameObject.SetActive(false);
    }



    static public void addHwMnCollision(string _type){

        if (_type == "opponent"){

            collHwMnOpponents += 1;
            //Debug.Log("collHwMnOpponents: " + collHwMnOpponents);
        }
        else if (_type == "band"){

            collHwMnBands += 1;
            //Debug.Log("collHwMnBands: " + collHwMnBands);
        }

        sensorLastColl = sensorNr;
        sensorNoCollisionCount = 0;
    }



    static public void resetCollisionCounter(int _trackNr, RaceInterfaceManager _raceIntMgr){

        trackNr = _trackNr;
        //raceIntMgr = _raceIntMgr;
        //animator = _raceIntMgr.getComplimentObj().GetComponent<Animator>();
        _raceIntMgr.getComplimentObj().GetComponent<ComplimentManager>().setOffComplimentText();
        collHwMnBands = 0;
        collHwMnOpponents = 0;
        sensorLastColl = 0;
        sensorsHwMn = 0;
        sensorNr = 0;
        sensorLastNr = -1;
        sensorNoCollisionCount = 0;
    }



    static public void checkSensor(int _sensorsHwMn, int _sensorNr){

        sensorsHwMn = _sensorsHwMn;

        if(_sensorNr > sensorNr || (sensorLastNr > sensorsHwMn-3 && _sensorNr < 1)){

            sensorLastNr = sensorNr;
            sensorNr = _sensorNr;
            sensorNoCollisionCount += 1;
        }

        checkCompliment();
        //Debug.Log(  "sensorsHwMn: " + _sensorsHwMn + ", sensorNr: " + sensorNr
        //            + ", sensorLastColl: " + sensorLastColl
        //            + ", trackNr: " + trackNr);
        //Debug.Log(  "sensorNoCollisionCount: " + sensorNoCollisionCount);
        //animator = raceIntMgr.getComplimentObj().GetComponent<Animator>();
        //animatorComplimet01Start();
    }



    static void checkCompliment(){

        if (sensorNoCollisionCount > sensorsHwMn && sensorNr == 1){

            animatorComplimet01Start();
            sensorNoCollisionCount = 1;
        }
        if (sensorNr == 7 && trackNr == 1 && sensorNoCollisionCount > 4 && sensorLastNr == 6){

            animatorComplimet02Start();
        }
    }



//animator
    static void animatorComplimet01Start(){

        //Debug.Log("...compliment START...");
        animator.SetBool("compliment01FL", true);
    }

    public void animatorComplimet01End(){

        animator.SetBool("compliment01FL", false);
    }


    static void animatorComplimet02Start(){

        //Debug.Log("...compliment START...");
        animator.SetBool("compliment02FL", true);
    }

    public void animatorComplimet02End(){

        animator.SetBool("compliment02FL", false);
    }




    public void setOffComplimentText(){

        gameObject.SetActive(true);
        animatorComplimet01End();
        animatorComplimet02End();
        animator.Rebind();
        compliment01text01Obj.SetActive(false);
        compliment01text02Obj.SetActive(false);
        compliment02text01Obj.SetActive(false);
        compliment02text02Obj.SetActive(false);
    }

}

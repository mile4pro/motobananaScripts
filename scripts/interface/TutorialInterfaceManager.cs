using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TutorialInterfaceManager : MonoBehaviour {

    [SerializeField]
    GameObject  backgroundObj, page1Obj, page2Obj,
                txtPage1Line1Obj, txtPage1Line2Obj;

    bool showTutorialFL = true;


    public void tutorialSetOn(int _nrTrack){
        //Debug.Log("tutorialSetOn...");
        if (showTutorialFL){
            gameObject.SetActive(true);
            StartCoroutine(delayShowTotorial(_nrTrack));
            showTutorialFL = false;
        }
    }

    public void tutorialSetOff(){
        backgroundObj.SetActive(false);
        page1Obj.SetActive(false);
        page2Obj.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }


    public void tutorialChangePage1toPage2(){
        page1Obj.SetActive(false);
        page2Obj.SetActive(true);
    }

    IEnumerator delayShowTotorial(int _nrTrack){
        //Debug.Log("delayShowTotorial...");
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        backgroundObj.SetActive(true);
        page1Obj.SetActive(true);
        page2Obj.SetActive(false);
        setTextTutorial(_nrTrack);
    }



    void setTextTutorial(int _nrTrack){

        switch (_nrTrack){

            case 0:
                setTxtOn1Page("TOUCH & HOLD = turn left", "NOT TOUCH = turn right");
                break;

            case 1:
                setTxtOn1Page("Tap Fast = Drive Straight", "Drive Straight = Fast Speed");
                break;

            case 2:
                setTxtOn1Page("banana contains\nvitamins A, C, E, K and B", "eat banana every day");
                break;

            default:
                setTxtOn1Page("TOUCH & HOLD = turn left", "NOT TOUCH = turn right");
                break;
        }
    }



    void setTxtOn1Page(string _txtLine1, string _txtLine2){

        txtPage1Line1Obj.GetComponent<Text>().text = _txtLine1;
        txtPage1Line2Obj.GetComponent<Text>().text = _txtLine2;
    }



}

using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;




public class TrophyMapInterfaceManager : MonoBehaviour {


    [SerializeField]
    SpriteAtlas     trophyMapAtlas;

    [SerializeField]
    GameObject      trophyButtons, topGroundObj,
                    playerDataObj, animationInformationInterfaceObj,
                    lvlNrInterfaceObj;

    [SerializeField]
    Animator        animator;

    bool    backBtnFL = false;

    int trophyLvlNr = 0;

    [SerializeField]
    GameObject      trophyMapBckrObj;
    [SerializeField]
    Sprite[]     trophyMapBckr;




	void Start () {

        //setTrophyMap();
	}



    void setTrophyMap(){

        setTrophyMapBckr( playerDataObj.GetComponent<PlayerData>().getActualTrack() );
        hideAllTrophyImg("trophyLock");
        setTrophyImgButtons( playerDataObj.GetComponent<PlayerData>().getMaxLvl() );
        //setTrophyImgButtons(101);
    }



    void setTrophyImgButtons(int _maxLvl){

        int tmpHwMn = (int)(Mathf.Min((_maxLvl-1), 100) / 10);
        //int tmpNrTrophyButtons = trophyButtons.transform.childCount;
        for (int i=0; i<tmpHwMn; i++){
            int tmpSpriteNr = (i+1)*10;
            string tmpSpriteNameNr = tmpSpriteNr.ToString("000");
            string tmpSpriteName = "trophyLvl" + tmpSpriteNameNr;
            trophyButtons.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = trophyMapAtlas.GetSprite(tmpSpriteName);
            trophyButtons.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = true;
        }
    }



    void hideAllTrophyImg(string _spriteName){

        int tmpNrTrophyButtons = trophyButtons.transform.childCount;
        for (int i=0; i<tmpNrTrophyButtons; i++){
            trophyButtons.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = trophyMapAtlas.GetSprite(_spriteName);
            trophyButtons.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
        }
    }



    public void backBtn(){

        backBtnFL = true;
        animatorHideStart();
    }



//******************
//animator

    public void animatorShowStart(){

        gameObject.SetActive(true);
        topGroundObj.SetActive(true);
        animator.SetBool("showFL", true);
        setTrophyMap();
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
        animator.SetBool("hideFL", false);
        gameObject.SetActive(false);
        if(backBtnFL){
            backBtnFL = false;
            lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().animatorStartShow();
        }
        else{
            animationInformationInterfaceObj.SetActive(true);
            animationInformationInterfaceObj.GetComponent<AnimationInformationInterfaceManager>().showAnmiationInformation(trophyLvlNr, true);
        }
    }



//***********************************
// trophy BUTTONS

    public void trophyLvl010(){

        trophyLvlNr = 11;
        animatorHideStart();
    }



    public void trophyLvl020(){

        trophyLvlNr = 21;
        animatorHideStart();
    }



    public void trophyLvl030(){

        trophyLvlNr = 31;
        animatorHideStart();
    }



    public void trophyLvl040(){

        trophyLvlNr = 41;
        animatorHideStart();
    }



    public void trophyLvl050(){

        trophyLvlNr = 51;
        animatorHideStart();
    }



    public void trophyLvl060(){

        trophyLvlNr = 61;
        animatorHideStart();
    }



    public void trophyLvl070(){

        trophyLvlNr = 71;
        animatorHideStart();
    }



    public void trophyLvl080(){

        trophyLvlNr = 81;
        animatorHideStart();
    }



    public void trophyLvl090(){

        trophyLvlNr = 91;
        animatorHideStart();
    }



    public void trophyLvl100(){

        trophyLvlNr = 101;
        animatorHideStart();
    }



    void setTrophyMapBckr(int _trackNr){

        trophyMapBckrObj.GetComponent<SpriteRenderer>().sprite = trophyMapBckr[_trackNr];
    }
    


}

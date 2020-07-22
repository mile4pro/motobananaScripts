using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

public class LvlNrInterfaceTrackBtnMnr : MonoBehaviour {

    [SerializeField]
    GameObject lvlNrInterfaceObj;
    LvlNrInterfaceManager lvlNrInterfaceMnr;

    [SerializeField]
    GameObject textNrTrack;

    int numberTracks = 3;

    [SerializeField]
    SpriteAtlas     trackImgAtlas;
    [SerializeField]
    GameObject      trackBtnLockerImg;

    [SerializeField]
    GameObject      playBtnObj, tapMoreStuffTrackImgObj;




    void Start(){

        refreshActualTrackNr();
    }



    void Update(){

        if(trackBtnLockerImg.activeSelf){
            lockerImgCheckScale();
        }
    }



    public void trackBtn(){

        lvlNrInterfaceMnr = lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>();

        int tmpActualNrTrack = lvlNrInterfaceMnr.getActualNrTrack();

        tmpActualNrTrack += 1;

        if (tmpActualNrTrack > (numberTracks-1)) {

            tmpActualNrTrack = 0;
        }

        lvlNrInterfaceMnr.setActualNrTrack(tmpActualNrTrack);

        tmpActualNrTrack += 1;
        textNrTrack.GetComponent<Text>().text = tmpActualNrTrack.ToString();

        lvlNrInterfaceMnr.refreshLvlInfo();

        if(checkUnlockTrack(tmpActualNrTrack, lvlNrInterfaceMnr)){

            //lvlNrInterfaceMnr.shopStuffTrackActive(tmpActualNrTrack);
        };

    }



    public void refreshActualTrackNr(){

        lvlNrInterfaceMnr = lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>();
        int tmpActualNrTrack = lvlNrInterfaceMnr.getActualNrTrack();
        lvlNrInterfaceMnr.setActualNrTrack(tmpActualNrTrack);
        tmpActualNrTrack += 1;
        textNrTrack.GetComponent<Text>().text = tmpActualNrTrack.ToString();

        checkUnlockTrack(tmpActualNrTrack, lvlNrInterfaceMnr);
    }



    bool checkUnlockTrack(int _nr, LvlNrInterfaceManager _lvlNrInterfaceMnr){       //shpActiveTrack = track03, shp1003 = 2

        int tmpState = 0;
        string tmpNrInvName = "shp" + _nr.ToString("1000");
        tmpState = _lvlNrInterfaceMnr.checkShopState(tmpNrInvName);

        string tmpTrackImageName = "track" + _nr.ToString("00");
        GetComponent<Image>().sprite = trackImgAtlas.GetSprite(tmpTrackImageName);

        if(tmpState < 1 && _nr > 1){

            trackBtnLockerImg.SetActive(true);
            lockerImgSetScale(new Vector3(1.3f, 1.3f, 1.3f));
            playBtnObj.GetComponent<Button>().interactable = false;
            tapMoreStuffTrackImgObj.SetActive(true);
            return false;
        }
        else{
            trackBtnLockerImg.SetActive(false);
            playBtnObj.GetComponent<Button>().interactable = true;
            tapMoreStuffTrackImgObj.SetActive(false);
            return true;
        }
    }



    void lockerImgSetScale(Vector3 _scale){

        trackBtnLockerImg.transform.localScale = _scale;
    }

    void lockerImgCheckScale(){

        float tmpStep = Time.deltaTime * 2f;

        if (trackBtnLockerImg.transform.localScale.x > 1){
            trackBtnLockerImg.transform.localScale -= new Vector3(tmpStep, tmpStep, tmpStep);
        }
        else {
            trackBtnLockerImg.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

}

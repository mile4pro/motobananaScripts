using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMngr : MonoBehaviour {

    [SerializeField]
    GameObject playerPre, opponentPre;

    [SerializeField]
    GameObject tracksAllObj;

    [SerializeField]
    GameObject raceInterfaceObj;
    RaceInterfaceManager    raceIntMngr;

    [SerializeField]
    GameObject playerDataObj;
    PlayerData playerData;

    [SerializeField]
    GameObject advertisementObj;

    GameObject[] track, opponent;

    GameObject player;

    Vector3[] startingPlaces;
    Quaternion[] startingPlacesRotation;

    int actualTrack = 0, actualLvl = 0, hwMnLap = 0, hwMnBananas = 0;

    bool    isPlayerFL, firstOpponentFL = true;

    [SerializeField]
    GameObject tutorialInterfaceobj;
    //bool loadOKFL;
    //BananasTypes bananasTypes;

    Texture2D actualTrackRoadFacture;

    bool    optionsDustOnRoadFL, optionsTraceOfTiresFL, optionsTraceOfTiresColorLightFL,
            optionsSoundsFL;

    [SerializeField]
    GameObject HelperGameObjectsObj;
    HelperGameObjectsManager helperGameObjectsMgr;

    [SerializeField]
    GameObject MainCameraObj;




    void Start(){

        raceIntMngr = raceInterfaceObj.GetComponent<RaceInterfaceManager>();

        playerData = playerDataObj.GetComponent<PlayerData>();

        loadTracksArr();

        helperGameObjectsMgr = HelperGameObjectsObj.GetComponent<HelperGameObjectsManager>();
        //bananasTypes = new BananasTypes();

        /*if (loadTracksArr()){
            if (loadTrack(0)){
               startingPlaces = track[actualTrack].GetComponent<TrackManager>().getStartingPlaces();
               loadBananas(11, 6);
           }
        }*/
    }



    void Update(){

        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //"R" reset game
        }
    }




    public bool loadLvl(int _nrTrack, int _nrLvl){

        actualLvl = _nrLvl;

        checkGraphicsSettings();

        //ComplimentManager.resetCollisionCounter(_nrTrack, raceIntMngr);

        if (loadTrack(_nrTrack)){

           startingPlaces = track[actualTrack].GetComponent<TrackManager>().getStartingPlaces();
           startingPlacesRotation = track[actualTrack].GetComponent<TrackManager>().getStartingPlacesRotation();

           setMainCameraAudioListener(false);

           loadBananas2(_nrLvl);
           helperGameObjectsMgr.setFirstFL(true);

           loadAudience(_nrLvl, _nrTrack, opponent);

           raceIntMngr.setBeginTextHwMnLap(hwMnLap);
           raceIntMngr.setBeginTextPosition(hwMnBananas, hwMnBananas);
           raceIntMngr.getMiddleBell().setPlaceList(opponent);

           raceIntMngr.setRaceInterfacePosition(track[actualTrack].GetComponent<TrackManager>());
           helperGameObjectsMgr.setRaceInterfacePosition(track[actualTrack].GetComponent<TrackManager>());

           if(_nrLvl == 1){
               tutorialInterfaceobj.GetComponent<TutorialInterfaceManager>().tutorialSetOn(_nrTrack);
           }
       }

        return true;
    }



    public bool loadLvlRestart(){

        return loadLvl(actualTrack, actualLvl);
    }



    bool loadTracksArr(){

        track = new GameObject[tracksAllObj.transform.childCount];
        for (int i=0; i<track.Length; i++){
            track[i] = tracksAllObj.transform.GetChild(i).gameObject;
            track[i].GetComponent<TrackManager>().setSensors();
            track[i].GetComponent<TrackManager>().setStartingPlaces();
        }
        return true;
    }



    public bool loadTrack(int _nrTrack){

        actualTrack = _nrTrack;
        track[actualTrack].SetActive(true);
        //actualTrackRoadFacture = track[actualTrack].transform.Find("roadFacture").gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        actualTrackRoadFacture = (Texture2D)track[actualTrack].transform.Find("images").Find("roadFacture").gameObject.GetComponent<SpriteRenderer>().sprite.texture;
        //if (!optionsTraceOfTiresFL){actualTrackRoadFacture.Apply();}
        //Debug.Log("GameMngr actualTrackRoadFacture: " + actualTrackRoadFacture);
        //startingPlaces = track[_nrTrack].GetComponent<TrackManager>().getActualStartingPlaces();
        return true;
    }



    bool loadAudience(int _nrLvl, int _nrTrack, GameObject[] _opponent){

        track[actualTrack].GetComponentInChildren<AudienceManager>().makeFans(_nrLvl, _nrTrack, _opponent);
        return true;
    }
    /*public bool loadBananas(int _hwMnOpponents, int _playerStartPlace){

        //startingPlaces = track[actualTrack].GetComponent<TrackManager>().getActualStartingPlaces();

        if (_playerStartPlace > -1){
            player = Instantiate(playerPre);
            player.transform.position = startingPlaces[_playerStartPlace];
            isPlayerFL = true;
        }
        else{
            isPlayerFL = false;
        }
        //player.GetComponent<AutoManager3>().setBananasTypes(bananasTypes);

        opponent = new GameObject[_hwMnOpponents];
        for (int i=0; i<_hwMnOpponents; i++){
            opponent[i] = Instantiate(opponentPre);
            opponent[i].GetComponent<EnemyManager3>().setCurves(track[actualTrack].GetComponent<TrackManager>().getSensors());
            //opponent[i].GetComponent<EnemyManager3>().setBananasTypes(bananasTypes);

            int tmpStartPosition = i;
            if (_playerStartPlace > -1 && tmpStartPosition >= _playerStartPlace){
                tmpStartPosition += 1;
            }

            opponent[i].transform.position = startingPlaces[tmpStartPosition];
        }

        return true;
    }*/




    /*public bool loadBananas2(int _hwMnOpponents, int _playerStartPlace){

        opponent = new GameObject[_hwMnOpponents+1];

        for (int i=0; i<_hwMnOpponents+1; i++){

            opponent[i] = Instantiate(opponentPre);
            opponent[i].GetComponent<EnemyManager4>().setCurves(track[actualTrack].GetComponent<TrackManager>().getSensors());
            opponent[i].transform.position = startingPlaces[i];

            if (_playerStartPlace == i){
                opponent[i].GetComponent<EnemyManager4>().setDriverPlayerFL(true);
                opponent[i].GetComponent<EnemyManager4>().setBananaType(0);
            }
        }

        return true;
    }*/




    /*public bool loadBananas(int _hwMnOpponents){

        //startingPlaces = track[actualTrack].GetComponent<TrackManager>().getActualStartingPlaces();

            player = Instantiate(playerPre);
            player.transform.position = startingPlaces[_hwMnOpponents];
            isPlayerFL = true;

        //player.GetComponent<AutoManager3>().setBananasTypes(bananasTypes);

        opponent = new GameObject[_hwMnOpponents];
        for (int i=0; i<_hwMnOpponents; i++){
            opponent[i] = Instantiate(opponentPre);
            opponent[i].GetComponent<EnemyManager3>().setCurves(track[actualTrack].GetComponent<TrackManager>().getSensors());
            //opponent[i].GetComponent<EnemyManager3>().setBananasTypes(bananasTypes);

            int tmpStartPosition = i;

            opponent[i].transform.position = startingPlaces[tmpStartPosition];
        }

        return true;
    }*/

    public bool loadBananas2(int _nrLvl, int _playerStartPlace){

        int _hwMnOpponents = _nrLvl % 10;
        if (_hwMnOpponents == 0) _hwMnOpponents = 10;

        hwMnBananas = _hwMnOpponents + 1;

        opponent = new GameObject[_hwMnOpponents+1];
        for (int i=0; i<_hwMnOpponents+1; i++){

            opponent[i] = Instantiate(opponentPre);

            setBananaParameters(opponent[i], true);

            opponent[i].GetComponent<EnemyManager4>().setCurves(track[actualTrack].GetComponent<TrackManager>().getSensors());
            opponent[i].transform.position = startingPlaces[i];
            //opponent[i].transform.rotation = startingPlacesRotation[i];

            opponent[i].GetComponent<EnemyManager4>().setMiddleBell(raceIntMngr.getMiddleBell());
            opponent[i].GetComponent<EnemyManager4>().setBananaStartingNr(i);

            if (i == _playerStartPlace){

                opponent[i].GetComponent<EnemyManager4>().setDriverPlayerFL(true);
                setBananaParameters(opponent[i], false);

                opponent[i].GetComponent<EnemyManager4>().setAudioListener(true);
            }
            /*else{
                raceIntMngr.getMiddleBell().addToPlaceChecker(i, false);
            }*/
            opponent[i].GetComponent<EnemyManager4>().setRoadFacture(actualTrackRoadFacture);

            opponent[i].GetComponent<EnemyManager4>().setGraphicsOptionsSetings(optionsDustOnRoadFL, optionsTraceOfTiresFL, optionsTraceOfTiresColorLightFL);
            opponent[i].GetComponent<EnemyManager4>().setAudioMotorFL(optionsSoundsFL);

        }

        firstOpponentFL = true;

        return true;

    }




    public bool loadBananas2(int _nrLvl){

        int _hwMnOpponents = _nrLvl % 10;
        if (_hwMnOpponents == 0) _hwMnOpponents = 10;

        hwMnBananas = _hwMnOpponents + 1;

        opponent = new GameObject[_hwMnOpponents+1];

        //float tmpDeltaTraceOfTiresColorDark = playerData.getDeltaTraceOfTiresColorDarkRandom();

        for (int i=0; i<_hwMnOpponents+1; i++){

            opponent[i] = Instantiate(opponentPre);

            setBananaParameters(opponent[i], true);

            EnemyManager4 tmpEnemyMgr = opponent[i].GetComponent<EnemyManager4>();

            tmpEnemyMgr.setCurves(track[actualTrack].GetComponent<TrackManager>().getSensors());
            opponent[i].transform.position = startingPlaces[i];
            opponent[i].transform.rotation = startingPlacesRotation[i];

            tmpEnemyMgr.setMiddleBell(raceIntMngr.getMiddleBell());
            tmpEnemyMgr.setBananaStartingNr(i);

            if (i == _hwMnOpponents){
                tmpEnemyMgr.setDriverPlayerFL(true);
                //opponent[i].GetComponent<EnemyManager4>().setBananaTypeNr(0);
                setBananaParameters(opponent[i], false);
                //opponent[i].GetComponent<EnemyManager4>().setBananaType(0);
                //raceIntMngr.getMiddleBell().addToPlaceChecker(i, true);
                opponent[i].GetComponent<EnemyManager4>().setAudioListener(true);
                //opponent[i].layer = 10;
            }
            /*else{
                raceIntMngr.getMiddleBell().addToPlaceChecker(i, false);
            }*/

            tmpEnemyMgr.setRoadFacture(actualTrackRoadFacture);

            tmpEnemyMgr.setGraphicsOptionsSetings(optionsDustOnRoadFL, optionsTraceOfTiresFL, optionsTraceOfTiresColorLightFL);
            tmpEnemyMgr.setAudioMotorFL(optionsSoundsFL);

            tmpEnemyMgr.setHelperGameObjectsMgr(helperGameObjectsMgr);

            float tmpDeltaTraceOfTiresColorDark = playerData.getDeltaTraceOfTiresColorDarkRandom();
            tmpEnemyMgr.setDeltaTraceOfTiresColorDark(tmpDeltaTraceOfTiresColorDark);
        }

        firstOpponentFL = true;

        return true;
    }




    public bool setOnAllCars(){

        //loadBananas(11, 1);
        float tmpStartTime = Time.time;

        for (int i=0; i<opponent.Length; i++){
            opponent[i].GetComponent<EnemyManager4>().setOnFL(true, tmpStartTime);
            opponent[i].GetComponent<EnemyManager4>().setStartDustColor();
        }

        if (isPlayerFL) {
            //player.GetComponent<AutoManager4>().setOnFL(true);
            //player.GetComponent<EnemyManager4>().setOnFL(true, tmpStartTime);
        }

        raceIntMngr.setRunTimersFL(true);

        return true;
    }



    public bool statsAddStartRaceCount(){

        return playerData.addStartRaceCount();
    }



    public bool statsEndRace(int _playerPlace){

        playerData.addEndRaceCount();
        playerData.addPlaceRaceCount(_playerPlace);

        if (!playerData.getLvlRaceCountFL(actualLvl)){

            playerData.addLvlRaceCount(actualLvl);
            if (_playerPlace < 2){
                playerData.setLvlRaceCountFL(actualLvl, true);
                return false;
            }
        }

        return true;
    }



    public void setActualtrack(int _nrTrack){
        actualTrack = _nrTrack;
    }

    public int getActualtrack(){
        return actualTrack;
    }







    public void raceInterfaceStart(){

        raceInterfaceObj.SetActive(true);
        //ComplimentManager.resetCollisionCounter(actualTrack, raceIntMngr);
        //raceInterfaceObj.GetComponent<RaceInterfaceManager>().animatorStartFadeOut();
        raceIntMngr.animatorStartFadeOut(actualTrack);
        raceIntMngr.setTextNrLevel(actualLvl);
    }





//endLvl

    public bool destroyAllCars(){

        for (int i=0; i<opponent.Length; i++){
            opponent[i].GetComponent<EnemyManager4>().destroyTmpObj();
            Destroy(opponent[i]);
        }

        if (isPlayerFL) {
            Destroy(player);
        }

        return true;
    }



    public bool setOffAllCars(){

        float tmpTime = Time.time;

        for (int i=0; i<opponent.Length; i++){
            opponent[i].GetComponent<EnemyManager4>().setOnFL(false, tmpTime);
        }

        if (isPlayerFL) {
            //player.GetComponent<AutoManager4>().setOnFL(false);
            //player.GetComponent<EnemyManager4>().setOnFL(false, tmpTime);
        }

        return true;
    }



    public bool forceEndLvl(){

        if (optionsTraceOfTiresFL){actualTrackRoadFacture.Apply(); Debug.Log("road facture update...");}
        setOffAllCars();
        destroyAllCars();
        track[actualTrack].GetComponentInChildren<AudienceManager>().clearFansList();
        track[actualTrack].SetActive(false);
        return true;
    }



    bool setBananaParameters(GameObject banan, bool isOpponentFL){

        EnemyManager4 bananMngr = banan.GetComponent<EnemyManager4>();

        float   tmpLvlTypeNr = Mathf.Floor((actualLvl-1)/10) + 2f*(float)actualTrack;

        float   tmpEnginePower = Mathf.Min(3.9f, 1.9f + (tmpLvlTypeNr * 1/6f)),    // 1/6
                tmpAngularSpeed = Mathf.Min(175f, 100f + (tmpLvlTypeNr * (10/6))),    // 10/6
                tmpSkillMin = Mathf.Max(0.4f, 0.75f - (tmpLvlTypeNr * 0.035f)),    //.4f, .75f, .26/6f
                tmpSkillMax = Mathf.Max(0.05f, 0.35f - (tmpLvlTypeNr * 0.035f)),     //.05f, .35f, .5f/6f
                tmpMass = Mathf.Min(1f, 1f + (tmpLvlTypeNr*(0.25f/6f)));

        tmpLvlTypeNr = Mathf.Floor((actualLvl-1)/10);

        if (isOpponentFL){
            //EnemyManager4 bananMngr = banan.GetComponent<EnemyManager4>();
            bananMngr.setMotorParameters(tmpEnginePower, tmpAngularSpeed, tmpSkillMin, tmpSkillMax, tmpMass);

            if (actualLvl<71){
                if (firstOpponentFL){
                    bananMngr.setBananaTypeNr((int)((tmpLvlTypeNr%7)+1));
                    firstOpponentFL = false;
                }
                else{
                    bananMngr.setBananaTypeNr(Random.Range(1, (int)((tmpLvlTypeNr%7)+2)));
                }
            }

            else{
                bananMngr.setBananaTypeNr(Random.Range(1, 8));
            }

            //return true;
        }


        else{   //is player
            //tmpLvlTypeNr = Mathf.Floor((actualLvl-1)/10);

            tmpEnginePower = Mathf.Min(4.25f, 2f + ((float)(playerData.getEnginePowerLvl()-1)* 0.075f));     //1.975f ... *0.05f
            tmpAngularSpeed = Mathf.Min(200f, 100f + ((tmpEnginePower-2f) * 50));   // (tmpEnginePower-2f) * 50
            //tmpSkillMin = Mathf.Max(0.4f, 0.75f - (tmpLvlTypeNr * 0.26f/6));
            //tmpSkillMax = Mathf.Max(0.05f, 0.35f - (tmpLvlTypeNr * 0.05f));
            tmpMass = Mathf.Min(1.125f, 1f + ((float)(playerData.getMassLvl()-1)* 0.005f));
            float tmpMassScaleSize = ((tmpMass - 1f) * 2f) + 1f;
            banan.transform.localScale = new Vector3(tmpMassScaleSize, tmpMassScaleSize, tmpMassScaleSize);
            //Debug.Log("tmpMass: " + tmpMass + ", tmpMassScaleSize: " + tmpMassScaleSize);
            //EnemyManager4 bananMngr = banan.GetComponent<EnemyManager4>();
            bananMngr.setMotorParameters(tmpEnginePower, tmpAngularSpeed, tmpSkillMin, tmpSkillMax, tmpMass);
            bananMngr.setBananaTypeNr(0);

            string tmpNewImageName = playerData.shopStuffGetActive("look");
            if(tmpNewImageName != "look01"){

                bananMngr.checkNewLookImageFromShop(true, tmpNewImageName);
            }

            //return true;
        }

        float tmpLvlTypeNr10 = actualLvl%10;
        if (tmpLvlTypeNr10 == 0) {tmpLvlTypeNr10 = 10;}
        //Debug.Log("tmpLvlTypeNr10: " + tmpLvlTypeNr10);
        int tmpHwMnLap = (int)Mathf.Ceil(tmpLvlTypeNr10/2f);
        //Debug.Log("hwMnLap: " + tmpHwMnLap);
        hwMnLap = tmpHwMnLap;
        bananMngr.setHwMnLap(tmpHwMnLap);

        //bananMngr.setStartDustColor();
        //Debug.Log("tmpEnginePower: " + tmpEnginePower);

        bananMngr.setDustParticleMaterial(playerData.shopStuffGetActive("smoke"));

        return true;

    }



    public AdvManager getAdvMgr(){
        return advertisementObj.GetComponent<AdvManager>();
    }


    public int getHwMnBananas(){
        return hwMnBananas;
    }


    public int getAdvPriceSmall(){
        return playerData.getAdvPriceSmall();
    }


//options setings
    bool checkGraphicsSettings(){
        optionsDustOnRoadFL = playerData.getOptGraDustOnRoad();
        optionsTraceOfTiresFL = playerData.getOptGraTraceOfTires();
        optionsTraceOfTiresColorLightFL = playerData.getOptGraTraceOfTiresColorLight();
        optionsSoundsFL = playerData.getOptSoundSounds();
        return true;
    }



    public void setMainCameraAudioListener(bool _FL){
        if (MainCameraObj.GetComponent<AudioListener>()) {
            MainCameraObj.GetComponent<AudioListener>().enabled = _FL;
        }
    }

}

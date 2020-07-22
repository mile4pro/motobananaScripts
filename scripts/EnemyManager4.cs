using UnityEngine;
using UnityEngine.U2D;
using System.Collections;



public class EnemyManager4 : MonoBehaviour {


    [SerializeField]
    SpriteAtlas     bananasAtlas, lookRaceAtlas;

    [SerializeField]
    GameObject      particleDustObj;
    ParticleBananaDustManager particleDustMgr;
    bool            particleDustFL;
    float           particleDustTmpTime;   // particleDustNumber;

    Texture2D       roadFacture;

    SpriteRenderer  spriteRenderer;

    GameObject[]    curves;

    GameObject      curveTargetObj, tmpObj;

    int             bananTypeNr = 1, hwMnLap, bananaStartingNr;

    Rigidbody2D     rb;

    bool            onFL,
                    turnFL,
                    turnBlockInertiaFl,
                    spriteChangeTurnFL,
                    loadTrackSensorsOK,
                    nitroClickFL = true,
                    driverPlayerFL;

    BananaMotorManager  motorMngr;
    float           beginMotorEnginePower,
                    beginMotorAngularSpeed,
                    beginMotorSkillMin,
                    beginMotorSkillMax,
                    beginRB2DMass;

    float           nitro = 0.025f,
                    nitroLastTime = 0f,
                    nitroTimeMax = 0.125f;

    int             nitroStep = 0,
                    nitroMaxStep = 10;

    public int      curveTargetNr = 0;

    string          spriteNameLeft, spriteNameRight, spriteNameNeutral;
    Sprite          spriteTurnLeft, spriteTurnRight, spriteNeutral;

    public float testAngle;

    BananaRaceData raceData;

    MiddleBellManager middleBell;

    [SerializeField]
    GameObject      rearWheelsLeftObj, rearWheelsRightObj;
    bool            roadFactureUpdateFL = true, randomTraceOfTiresColorFL = false;
    float           maxTraceOfTiresColorDark = 0.75f, maxTraceOfTiresColorLight = 0.95f,
                    deltaTraceOfTiresColorDark = 0f, beginTraceOfTiresColorDark = 0.85f;

    bool            optionsDustOnRoadFL, optionsTraceOfTiresFL, optionsTraceOfTiresColorLightFL;

    [SerializeField]
    GameObject      audioMotorObj;
    AudioMotorManager   audioMotorMgr;
    bool            audioMotorFL;

    float   timeTurnDelta;
    bool    timeTurnDeltaFL = true;

    HelperGameObjectsManager helperGameObjectsMgr;

    [SerializeField]
    AudioListener   audioListener;

    [SerializeField]
    GameObject      audioCollisionObj, particleCollisionObj;
    AudioCollisionManager   audioCollision;
    ParticleBananaCollisionManager  particleCollMnr;

    bool    EndRaceFL = false;

    bool newLookImageFL = false;
    string newLookImageName = "";




    void Start(){
        rb = GetComponent<Rigidbody2D>();
        tmpObj = new GameObject();
        spriteRenderer = GetComponent<SpriteRenderer>();

        /*bananTypeNr = Random.Range(1, 8);
        cashingSpriteNames(bananTypeNr);
        cashingSprites();
        spriteRenderer.sprite = spriteNeutral;*/
        //setBananaType(Random.Range(1, 8));
        //if (driverPlayerFL) setBananaType(0);
        setBananaType(bananTypeNr);

        raceData = new BananaRaceData();

        motorMngr = new BananaMotorManager( beginMotorEnginePower,
                                            beginMotorAngularSpeed,
                                            beginMotorSkillMin,
                                            beginMotorSkillMax);

        rb.mass = beginRB2DMass;
        raceData.setHwMnLap(hwMnLap);

        particleDustMgr = particleDustObj.GetComponent<ParticleBananaDustManager>();
        particleCollMnr = particleCollisionObj.GetComponent<ParticleBananaCollisionManager>();

        audioMotorMgr = audioMotorObj.GetComponent<AudioMotorManager>();
        audioCollision = audioCollisionObj.GetComponent<AudioCollisionManager>();

        if(newLookImageFL){cashingSpritesNewlookImage();}
    }



    bool cashingSpriteNames(int _nrBanana){

        spriteNameLeft = BananasTypes.getBananSpriteName(_nrBanana, "turnLeft");
        spriteNameRight = BananasTypes.getBananSpriteName(_nrBanana, "turnRight");
        spriteNameNeutral = BananasTypes.getBananSpriteName(_nrBanana, "neutral");

        return true;
    }



    bool cashingSprites(){

        spriteTurnLeft = bananasAtlas.GetSprite(spriteNameLeft);
        spriteTurnRight = bananasAtlas.GetSprite(spriteNameRight);
        spriteNeutral = bananasAtlas.GetSprite(spriteNameNeutral);

        return true;
    }



    public bool setBananaType(int _typeNr){
        cashingSpriteNames(_typeNr);
        cashingSprites();
        spriteRenderer.sprite = spriteNeutral;
        particleCollisionObj.GetComponent<ParticleBananaCollisionManager>().setParticleColor(_typeNr);
        return true;
    }



    //new look images from shop
    public void checkNewLookImageFromShop(bool _FL, string _name){

        newLookImageFL = _FL;
        newLookImageName = _name;
    }

    bool cashingSpritesNewlookImage(){

        spriteTurnLeft = lookRaceAtlas.GetSprite(newLookImageName + "Left");
        spriteTurnRight = lookRaceAtlas.GetSprite(newLookImageName + "Right");
        spriteNeutral = lookRaceAtlas.GetSprite(newLookImageName + "Neutral");

        spriteRenderer.sprite = spriteNeutral;

        return true;
    }





    void FixedUpdate () {

            if (onFL){
                if (rb.velocity.magnitude < motorMngr.maxSpeed) {
                    rb.AddForce(transform.up * motorMngr.enginePower);
                }

                if (turnFL){
                     rb.angularVelocity = motorMngr.maxAngularSpeed;
                }
                else {
                    rb.angularVelocity = -motorMngr.maxAngularSpeed * motorMngr.angularTurnRightFactor;
                }
            }
    }



    void Update(){

        if (!driverPlayerFL){
            driverCPU();
        }
        else{
            driverPLAYER();
            if(optionsTraceOfTiresFL && optionsTraceOfTiresColorLightFL){
                if(roadFactureUpdateFL) StartCoroutine(roadFactureUpdate());      //<-- tu włączasz rysowanie się śladów na torze przez koła pojazdów
            }
        }

        if (optionsDustOnRoadFL && onFL) {checkParticleDust(turnFL);}
        else if (!particleDustMgr.checkParticleIsStopped() && !optionsDustOnRoadFL) {particleDustMgr.stopParticle();}

        checkTimeTurnDelta();
        if (audioMotorFL && onFL) {audioMotorMgr.checkAudioMotor(timeTurnDelta, beginMotorEnginePower);}
        else if (audioMotorFL) {audioMotorMgr.audioMotorBeforeStartRace(beginMotorEnginePower);}

    }




    void driverCPU(){

        if (loadTrackSensorsOK){
            enemyBrain();
        }

        if (onFL){

            if (turnFL && spriteChangeTurnFL){
                spriteRenderer.sprite = spriteTurnLeft;
                spriteChangeTurnFL = false;
            }
            else if (!turnFL && !spriteChangeTurnFL){
                spriteRenderer.sprite = spriteTurnRight;
                spriteChangeTurnFL = true;
            }


            motorMngr.checkMotorChanges();
        }
    }



    void driverPLAYER(){

        if (onFL){
            if (Input.GetMouseButton(0)){
                turnFL = true;
                if (spriteChangeTurnFL){
                    spriteRenderer.sprite = spriteTurnLeft;
                    spriteChangeTurnFL = false;
                }
            }
            else {
                turnFL = false;
                if (!spriteChangeTurnFL){
                    spriteRenderer.sprite = spriteTurnRight;
                    spriteChangeTurnFL = true;
                }
            }

            nitroManager();
            motorMngr.checkMotorChanges();
        }
    }




    public bool setCurves(GameObject[] _trackSensors){
        curves = _trackSensors;
        curveTargetObj = curves[0];
        loadTrackSensorsOK = true;
        return true;
    }



    void OnTriggerEnter2D(Collider2D col){

        if (col.tag == "planeRouteSensor"){

            bool raceEndFL = raceData.getEndRaceFL();
            int tmpLenght = curves.Length;

            for (int i=0; i<tmpLenght; i++){

                if (curves[i].name == col.name && col.name == curveTargetObj.name){

                    int tmpIndex;
                    if (i > tmpLenght-2){tmpIndex = 0;}
                    else {tmpIndex = i+1;}

                    curveTargetObj = curves[tmpIndex];

                    curveTargetNr = tmpIndex;

                    if (curves[0].name == col.name && !raceEndFL){
                        raceData.startLap();
                        if(driverPlayerFL){
                            middleBell.setTextWhichLap(raceData.getActualNrLap());
                            if (!raceData.getEndRaceFL() && raceData.getActualNrLap()>0){
                                middleBell.resetTimerLap();
                                middleBell.statsAddLapRaceCount();
                            }
                        }
                    }

                    int tmpValueInRace = (raceData.getActualNrLap() * tmpLenght) + i;
                    //Debug.Log("tmpValueInRace: " + tmpValueInRace);
                    int tmpActualPlace = middleBell.checkPlace(tmpValueInRace, bananaStartingNr, driverPlayerFL) + 1;
                    //Debug.Log("middleBell.checkPlace(tmpValueInRace, bananaStartingNr, driverPlayerFL) :" + middleBell.checkPlace(tmpValueInRace, bananaStartingNr, driverPlayerFL));
                    raceData.setActualPlace(tmpActualPlace);

                    break;
                }
            }

            raceEndFL = raceData.getEndRaceFL();

            if (raceEndFL){

                EndRaceFL = raceEndFL;

                if(!raceData.getEndRaceVisualEffectFL()){
                    helperGameObjectsMgr.endRace(raceData.getTimeRaceDoNotCount(), driverPlayerFL);
                    raceData.setEndRaceVisualEffectFL(true);
                }

                if (driverPlayerFL) {
                    middleBell.statsAddLapRaceCount();
                    driverPlayerFL = false;
                    middleBell.setRunTimersFL(false);
                    middleBell.showEndRaceInterface(raceData.getActualPlace());
                }
                motorMngr.randomEndRaceMotorValues();
                audioMotorMgr.setEndRaceAudioVolume();
            }

            motorMngr.randomInertia();
            motorMngr.randomSkill();

            gameObject.layer = col.gameObject.layer;

            if (gameObject.layer == 9 || gameObject.layer == 10 ) {

                spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
                particleDustMgr.setMask(false);
                particleCollMnr.setMask(false);
            }
            else if (gameObject.layer == 8) {

                spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                particleDustMgr.setMask(true);
                particleCollMnr.setMask(true);
            }

            if (driverPlayerFL){

                ComplimentManager.checkSensor(tmpLenght, curveTargetNr);
            }
        }

    }




    void OnCollisionEnter2D(Collision2D coll){

        Vector2 tmpRelVec = coll.relativeVelocity;
        audioCollision.playSoundCollision(tmpRelVec.magnitude, EndRaceFL);

        if (driverPlayerFL){

            ComplimentManager.addHwMnCollision(coll.transform.tag);

            if(coll.transform.tag == "opponent"){

                Rigidbody2D tmpRB = coll.rigidbody;
                tmpRB.AddForce(tmpRelVec * (-1*((beginRB2DMass - 1) * 200)));
            }
        }

        particleCollMnr.bananaCollision(coll);
    }




    bool enemyBrain(){

        Vector3 dirSkill = Vector3.Lerp(
                            curveTargetObj.GetComponent<RouteSensorManager>().getLeftV3(),
                            curveTargetObj.GetComponent<RouteSensorManager>().getRightV3(),
                            motorMngr.skill);

        Vector3 dir = dirSkill - transform.position;

        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;

        tmpObj.transform.position = transform.position;

        tmpObj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        turnFL = false;

        testAngle = Quaternion.Angle(transform.rotation, tmpObj.transform.rotation);


        if (testAngle > motorMngr.inertia && testAngle < 90-motorMngr.inertia && turnBlockInertiaFl){
            turnBlockInertiaFl = false;
        }

        if (testAngle < 180-motorMngr.inertia && testAngle > 90+motorMngr.inertia && !turnBlockInertiaFl){
            turnBlockInertiaFl = true;
        }

        if (turnBlockInertiaFl){
            turnFL = true;
        }

        return true;
    }



    void consoleDebug(){

    }



//player nitro
    void nitroCheck(){
        if (turnFL && nitroClickFL){
            nitroStepUp();
            nitroClickFL = false;
            //Debug.Log("nitroLEFT");
        }
        else if (!turnFL && !nitroClickFL){
            nitroStepUp();
            nitroClickFL = true;
            //Debug.Log("nitroRight");
        }
        else {
            nitroStepDown();
        }
    }


    bool nitroStepUp(){
        float tmpTime = Time.time - nitroLastTime;
        nitroLastTime = Time.time;
        if (tmpTime < nitroTimeMax && nitroStep < nitroMaxStep){
            nitroStep += 1;
            //Debug.Log("nitro STEP UP");
            return true;
        }
        return false;
    }


    bool nitroStepDown(){
        float tmpTime = Time.time - nitroLastTime;
        if (tmpTime > nitroTimeMax && nitroStep > 0){
            nitroStep -= 1;
            nitroLastTime = Time.time;
            //Debug.Log("nitro STEP DOWN");
            return true;
        }
        return false;
    }
	

    bool nitroOn(){
        if (nitroStep > 5){
            motorMngr.enginePowerChange = beginMotorEnginePower + ((nitroStep-5) * 0.015f); // ...*0.015f
            //Debug.Log("NITRO: " + motorMngr.enginePowerChange);
            return true;
        }
        else {
            motorMngr.enginePowerChange = beginMotorEnginePower;
            return false;
        }
    }


    void nitroManager(){
        nitroCheck();
        nitroOn();
    }





//start race
    public void setOnFL(bool _FL, float _time){
        onFL = _FL;
        raceData.setTimeStart(_time);
        raceData.setTimeLapStart(_time);
    }

    public bool getOnFL(){
        return onFL;
    }




//end lvl
    public bool destroyTmpObj(){
        Destroy(tmpObj);
        return true;
    }



//driver Player or CPU
    public void setDriverPlayerFL(bool _FL){
        driverPlayerFL = _FL;
    }


//load begin race parameters
    public bool setMotorParameters(float _engPwr, float _angSpd, float _skillMin, float _skillMax, float _mass){
        beginMotorEnginePower = _engPwr;
        beginMotorAngularSpeed = _angSpd;
        beginMotorSkillMin = _skillMin;
        beginMotorSkillMax = _skillMax;
        beginRB2DMass = _mass;
        return true;
    }



    public bool setBananaTypeNr(int _nr){
        bananTypeNr = _nr;
        return true;
    }



    public int getBananaTypeNr(){

        return bananTypeNr;
    }



    public void setStartDustColor(){
        particleDustMgr.startColorDust(bananTypeNr);
    }




    public bool setHwMnLap(int _hwMnLap){
        hwMnLap = _hwMnLap;
        return true;
    }


    public bool setMiddleBell(MiddleBellManager _middleBell){
        middleBell = _middleBell;
        return true;
    }

    public bool setBananaStartingNr(int _nr){
        bananaStartingNr = _nr;
        return true;
    }

    public int getBananaStartingNr(){
        return bananaStartingNr;
    }



//particle dust
    void checkParticleDust(bool _FL){

        if(particleDustFL == _FL){
            //particleDustNumber += 1;
            //particleDustNumber += (Time.time - particleDustTmpTime);
        }
        else{
            particleDustTmpTime = Time.time;
            //particleDustNumber = 0f;
            particleDustMgr.setDustRate(0f);
            particleDustMgr.setDustSize(0f);
        }
        particleDustFL = _FL;


        /*if(particleDustNumber > 25){
            float tmpNr = particleDustNumber - 25f;
            float tmpDustSize = tmpNr%10/25f;
            particleDustMgr.setDustRate(Mathf.Min(tmpNr, 15f));
            particleDustMgr.setDustSize(Mathf.Min(tmpDustSize, 1f));
        }*/
        float tmpTime = Time.time - particleDustTmpTime;
        
		particleDustMgr.setDustRate(Mathf.Min(Mathf.Max(tmpTime*10, 10f), 15f));
        particleDustMgr.setDustSize(Mathf.Min(Mathf.Max(tmpTime, .3f), 1f));

        if (optionsTraceOfTiresFL && tmpTime > 0.2) {
            float tmpDrawHwMn = (float)(tmpTime*0.1);
            drawToRoadFacture(tmpDrawHwMn);
        }
    }



//road facture
    public bool setRoadFacture(Texture2D _roadFacture){
            roadFacture = _roadFacture;
            return true;
    }
	

    void drawToRoadFacture(float _hwMn){

        Vector2 tmpPosLeFtWheel = new Vector2(rearWheelsLeftObj.transform.position.x * 100 + 360, rearWheelsLeftObj.transform.position.y * 100 + 640);
        Vector2 tmpPosRightWheel = new Vector2(rearWheelsRightObj.transform.position.x * 100 + 360, rearWheelsRightObj.transform.position.y * 100 + 640);

        Color tmpColorRoadUnderLeftWheel = roadFacture.GetPixel((int)(tmpPosLeFtWheel.x), (int)(tmpPosLeFtWheel.y));
        Color tmpColorRoadUnderRightWheel = roadFacture.GetPixel((int)(tmpPosRightWheel.x), (int)(tmpPosRightWheel.y));
		

		tmpColorRoadUnderLeftWheel.r -= tmpColorRoadUnderLeftWheel.r * _hwMn;
		tmpColorRoadUnderLeftWheel.g -= tmpColorRoadUnderLeftWheel.g * _hwMn;
		tmpColorRoadUnderLeftWheel.b -= tmpColorRoadUnderLeftWheel.b * _hwMn;

		tmpColorRoadUnderLeftWheel.r = Mathf.Max(tmpColorRoadUnderLeftWheel.r, maxTraceOfTiresColorDark);
		tmpColorRoadUnderLeftWheel.g = Mathf.Max(tmpColorRoadUnderLeftWheel.g, maxTraceOfTiresColorDark);
		tmpColorRoadUnderLeftWheel.b = Mathf.Max(tmpColorRoadUnderLeftWheel.b, maxTraceOfTiresColorDark);

		tmpColorRoadUnderRightWheel.r -= tmpColorRoadUnderRightWheel.r * _hwMn;
		tmpColorRoadUnderRightWheel.g -= tmpColorRoadUnderRightWheel.g * _hwMn;
		tmpColorRoadUnderRightWheel.b -= tmpColorRoadUnderRightWheel.b * _hwMn;

		tmpColorRoadUnderRightWheel.r = Mathf.Max(tmpColorRoadUnderRightWheel.r, maxTraceOfTiresColorDark);
		tmpColorRoadUnderRightWheel.g = Mathf.Max(tmpColorRoadUnderRightWheel.g, maxTraceOfTiresColorDark);
		tmpColorRoadUnderRightWheel.b = Mathf.Max(tmpColorRoadUnderRightWheel.b, maxTraceOfTiresColorDark);
			

        roadFacture.SetPixel((int)(tmpPosLeFtWheel.x), (int)(tmpPosLeFtWheel.y), tmpColorRoadUnderLeftWheel);
        roadFacture.SetPixel((int)(tmpPosLeFtWheel.x+1), (int)(tmpPosLeFtWheel.y), tmpColorRoadUnderLeftWheel);
        roadFacture.SetPixel((int)(tmpPosLeFtWheel.x-1), (int)(tmpPosLeFtWheel.y), tmpColorRoadUnderLeftWheel);
        roadFacture.SetPixel((int)(tmpPosLeFtWheel.x), (int)(tmpPosLeFtWheel.y+1), tmpColorRoadUnderLeftWheel);
        roadFacture.SetPixel((int)(tmpPosLeFtWheel.x), (int)(tmpPosLeFtWheel.y-1), tmpColorRoadUnderLeftWheel);

        roadFacture.SetPixel((int)(tmpPosRightWheel.x), (int)(tmpPosRightWheel.y), tmpColorRoadUnderRightWheel);
        roadFacture.SetPixel((int)(tmpPosRightWheel.x+1), (int)(tmpPosRightWheel.y), tmpColorRoadUnderRightWheel);
        roadFacture.SetPixel((int)(tmpPosRightWheel.x+1), (int)(tmpPosRightWheel.y), tmpColorRoadUnderRightWheel);
        roadFacture.SetPixel((int)(tmpPosRightWheel.x), (int)(tmpPosRightWheel.y+1), tmpColorRoadUnderRightWheel);
        roadFacture.SetPixel((int)(tmpPosRightWheel.x), (int)(tmpPosRightWheel.y-1), tmpColorRoadUnderRightWheel);
    }


    IEnumerator roadFactureUpdate(){
        roadFactureUpdateFL = false;
        yield return new WaitForSeconds(.1f);
        //Debug.Log("roadFactureUpdate(): " + Time.time);
        roadFacture.Apply();
        roadFactureUpdateFL = true;
    }


//options setings
    public bool setGraphicsOptionsSetings(bool _dustFL, bool _traceFL, bool _traceLightFL){
        optionsDustOnRoadFL = _dustFL;
        optionsTraceOfTiresFL = _traceFL;
        optionsTraceOfTiresColorLightFL = _traceLightFL;

        return true;
    }


//time delta turn function
    void checkTimeTurnDelta(){
        if (timeTurnDeltaFL == turnFL){
            timeTurnDelta += Time.deltaTime;
        }
        else{
            //Debug.Log("timeTurnDelta: " + timeTurnDelta);
            timeTurnDelta = 0f;
            timeTurnDeltaFL = turnFL;
        }
    }


    public void setAudioMotorFL(bool _FL){
        audioMotorFL = _FL;
    }



    public bool setHelperGameObjectsMgr(HelperGameObjectsManager _hgom){
        helperGameObjectsMgr = _hgom;
        return true;
    }


    public void setAudioListener(bool _FL){
        audioListener.enabled = _FL;
    }



    public bool setDeltaTraceOfTiresColorDark(float _hwMn){

        deltaTraceOfTiresColorDark = _hwMn;
        //Debug.Log("deltaTraceOfTiresColorDark = " + deltaTraceOfTiresColorDark);
        maxTraceOfTiresColorDark = beginTraceOfTiresColorDark - deltaTraceOfTiresColorDark;
        //Debug.Log("maxTraceOfTiresColorDark = " + maxTraceOfTiresColorDark);
        return true;
    }



    public void setDustParticleMaterial(string _name){

        if(particleDustMgr != null){
            particleDustMgr.setDustMaterialName(_name);
        }
        else{
            particleDustObj.GetComponent<ParticleBananaDustManager>().setDustMaterialName(_name);
        }
    }

}

using UnityEngine;
using UnityEngine.UI;

public class OptionsInterfaceManager : MonoBehaviour {


    [SerializeField]
    GameObject  mainOptionsObj, graphicsObj, soundObj, otherObj;

    [SerializeField]
    GameObject  tggDustOnRoadObj,
                tggTraceOfTiresObj, tggTraceOfTiresLabelTxtObj,
                sliderTraceOfTiresLightColorObj, sliderTraceOfTiresLightColorTXTLightObj, sliderTraceOfTiresLightColorTXTDarkObj,
                tggSoundsAllObj, tggSoundsObj, tggMusicObj,
                tggMoreStatsObj;

    [SerializeField]
    GameObject  playerDataObj;
    PlayerData  playerData;




    void Start(){
        playerData = playerDataObj.GetComponent<PlayerData>();
        updateOptionSettings();
    }




    public void optionsInterfaceOn(){

        gameObject.SetActive(true);
        updateOptionSettings();
    }



    void optionsInterfaceOff(){

        gameObject.SetActive(false);
    }



    public void pressBackButton(){
        if (mainOptionsObj.activeSelf){
            optionsInterfaceOff();
        }
        else if (graphicsObj.activeSelf){
            graphicsInterfaceOff();
        }
        else if (soundObj.activeSelf){
            soundInterfaceOff();
        }
        else if (otherObj.activeSelf){
            otherInterfaceOff();
        }
    }



    public void graphicsInterfaceOn(){
        mainOptionsObj.SetActive(false);
        graphicsObj.SetActive(true);
    }

    void graphicsInterfaceOff(){
        mainOptionsObj.SetActive(true);
        graphicsObj.SetActive(false);
    }



    public void soundInterfaceOn(){
        mainOptionsObj.SetActive(false);
        soundObj.SetActive(true);
    }

    void soundInterfaceOff(){
        mainOptionsObj.SetActive(true);
        soundObj.SetActive(false);
    }



    public void otherInterfaceOn(){
        mainOptionsObj.SetActive(false);
        otherObj.SetActive(true);
    }

    void otherInterfaceOff(){
        mainOptionsObj.SetActive(true);
        otherObj.SetActive(false);
    }



    public void toggleDustOnRoad(){

        bool tmpFL = tggDustOnRoadObj.GetComponent<Toggle>().isOn;

        if (!tmpFL) {toogleTraceOfTiresInteractableOff();}
        else {toogleTraceOfTiresInteractableOn();}

        playerData.setOptGraDustOnRoad(tmpFL);
    }



    public void toggleTraceOfTires(){

        bool tmpFL = tggTraceOfTiresObj.GetComponent<Toggle>().isOn;

        if (!tmpFL){sliderTraceOfTiresLightColorInteractableOff();}
        else {sliderTraceOfTiresLightColorInteractableOn();}

        //Debug.Log("toggleTraceOfTires: " + tmpFL);
        playerData.setOptGraTraceOfTires(tmpFL);
    }



    public void sliderTraceOfTiresLightColor(){

        int tmpNr = (int)sliderTraceOfTiresLightColorObj.GetComponent<Slider>().value;
        //Debug.Log("sliderTraceOfTiresLightColor: " + tmpNr);
        bool tmpFL = false;
        if (tmpNr != 0) {tmpFL = true;}
        playerData.setOptGraTraceOfTiresColorLight(tmpFL);
    }



    public void toggleMoreStats(){

        bool tmpFL = tggMoreStatsObj.GetComponent<Toggle>().isOn;
        playerData.setOptMoreStats(tmpFL);
    }



    void updateOptionSettings(){

        if (playerData == null){
            playerData = playerDataObj.GetComponent<PlayerData>();
        }

        tggDustOnRoadObj.GetComponent<Toggle>().isOn = playerData.getOptGraDustOnRoad();

        if (playerData.getOptGraTraceOfTires()){
            tggTraceOfTiresObj.GetComponent<Toggle>().isOn = true;
            toogleTraceOfTiresInteractableOn();
            sliderTraceOfTiresLightColorInteractableOn();
        }
        else if(playerData.getOptGraDustOnRoad()){
            toogleTraceOfTiresInteractableOn();
            tggTraceOfTiresObj.GetComponent<Toggle>().isOn = false;
            sliderTraceOfTiresLightColorInteractableOff();
        }
        else{
            toogleTraceOfTiresInteractableOff();
            sliderTraceOfTiresLightColorInteractableOff();
        }
        //tggTraceOfTiresObj.GetComponent<Toggle>().isOn = playerData.getOptGraTraceOfTires();

        float tmpTraceColorValue = 0;
        if (playerData.getOptGraTraceOfTiresColorLight()) {tmpTraceColorValue = 1;}
        sliderTraceOfTiresLightColorObj.GetComponent<Slider>().value = tmpTraceColorValue;

        tggSoundsObj.GetComponent<Toggle>().isOn = playerData.getOptSoundSounds();
        checkSoundsAll();

        tggMoreStatsObj.GetComponent<Toggle>().isOn = playerData.getOptMoreStats();
    }



    void checkSoundsAll(){

        bool tmpSoundsAllFL = playerData.getOptSoundSoundsAll();
        tggSoundsAllObj.GetComponent<Toggle>().isOn = tmpSoundsAllFL;
        if(tmpSoundsAllFL){AudioListener.volume = 1f;}
        else {AudioListener.volume = 0f;}
    }



    void toogleTraceOfTiresInteractableOff(){

        tggTraceOfTiresObj.GetComponent<Toggle>().isOn = false;
        playerData.setOptGraTraceOfTires(false);
        tggTraceOfTiresObj.GetComponent<Toggle>().interactable = false;
        tggTraceOfTiresObj.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        tggTraceOfTiresLabelTxtObj.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);

    }



    void toogleTraceOfTiresInteractableOn(){

        tggTraceOfTiresObj.GetComponent<Toggle>().interactable = true;
        tggTraceOfTiresObj.transform.localScale = new Vector3(1f, 1f, 1f);
        tggTraceOfTiresLabelTxtObj.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
    }



    void sliderTraceOfTiresLightColorInteractableOff(){

        sliderTraceOfTiresLightColorObj.GetComponent<Slider>().interactable = false;
        sliderTraceOfTiresLightColorObj.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        sliderTraceOfTiresLightColorTXTLightObj.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
        sliderTraceOfTiresLightColorTXTDarkObj.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
    }



    void sliderTraceOfTiresLightColorInteractableOn(){

        sliderTraceOfTiresLightColorObj.GetComponent<Slider>().interactable = true;
        sliderTraceOfTiresLightColorObj.transform.localScale = new Vector3(1f, 1f, 1f);
        sliderTraceOfTiresLightColorTXTLightObj.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
        sliderTraceOfTiresLightColorTXTDarkObj.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
    }



//***************************************
//music and sounds

    public void toggleSounds(){

        bool tmpFL = tggSoundsObj.GetComponent<Toggle>().isOn;
        playerData.setOptSoundSounds(tmpFL);
    }

    public void toggleSoundsAll(){

        bool tmpFL = tggSoundsAllObj.GetComponent<Toggle>().isOn;
        playerData.setOptSoundSoundsAll(tmpFL);
        checkSoundsAll();
    }



    public void developResetBtn(){

        playerData.developResetBtn();
    }


}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;



public class ShopManager : MonoBehaviour {

    [SerializeField]
    GameObject  bumniImgObj, upgradesObj, nrLvlObj, playBtnObj,
                tapForLvlInfoImageObj, tapBumniForMoreStuffImageObj,
                backgroundLvlInterfaceImgObj,
                backgroundShopImgObj,
                trackBtnObj, developResetBtnObj;

    List<ShopStuff> shelfTrack, shelfLook, shelfSmoke;

    [SerializeField]
    SpriteAtlas     trackAtlas, lookAtlas, smokeAtlas;

    [SerializeField]
    GameObject      shelfPrefab;

    GameObject      shelfTrackObj, shelfLookObj, shelfSmokeObj;

    [SerializeField]
    GameObject      PlayerDataObj;
    PlayerData      playerData;

    [SerializeField]
    GameObject      lvlNrInterfaceObj;

    LvlNrInterfaceManager lvlNrInterfaceMnr;


    void Start(){

        playerData = PlayerDataObj.GetComponent<PlayerData>();
        lvlNrInterfaceMnr = lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>();

        makeShelfs();

        shelfTrackObj = Instantiate(shelfPrefab, transform, false);
        shelfTrackObj.transform.localPosition = new Vector3(0f, 75f, 0f);
        shelfTrackObj.GetComponent<ShopShelfManager>().loadShelf(shelfTrack, trackAtlas, playerData, false, this);

        shelfLookObj = Instantiate(shelfPrefab, transform, false);
        shelfLookObj.transform.localPosition = new Vector3(0f, -202f, 0f);
        shelfLookObj.GetComponent<ShopShelfManager>().loadShelf(shelfLook, lookAtlas, playerData, true, this);

        shelfSmokeObj = Instantiate(shelfPrefab, transform, false);
        shelfSmokeObj.transform.localPosition = new Vector3(0f, -480f, 0f);
        shelfSmokeObj.GetComponent<ShopShelfManager>().loadShelf(shelfSmoke, smokeAtlas, playerData, true, this);
    }



    public void bumniShopBtn(bool _FL){

        setOnLvlInterfaceStuff(_FL);
    }



    public void setOnLvlInterfaceStuff(bool _FL){

        backgroundLvlInterfaceImgObj.SetActive(_FL);

        bumniImgObj.SetActive(_FL);
        upgradesObj.SetActive(_FL);
        nrLvlObj.SetActive(_FL);
        playBtnObj.SetActive(_FL);
        tapForLvlInfoImageObj.SetActive(_FL);
        tapBumniForMoreStuffImageObj.SetActive(_FL);

        trackBtnObj.SetActive(_FL);

        setOnShopStuff(!_FL);

        refreshShelves();
    }



    void setOnShopStuff(bool _FL){

        backgroundShopImgObj.SetActive(_FL);
        gameObject.SetActive(_FL);
    }



    void makeShelfTrack(){

        shelfTrack = new List<ShopStuff>();

        shelfTrack.Add(new ShopStuff("track", "track01", 0, 1001, "track01"));
        shelfTrack.Add(new ShopStuff("track", "track02", 1000, 1002, "track02"));
        shelfTrack.Add(new ShopStuff("track", "track03", 2500, 1003, "track03"));
    }



    void makeShelfLook(){

        shelfLook = new List<ShopStuff>();

        shelfLook.Add(new ShopStuff("look", "Normal", 0, 2001, "look01"));
        shelfLook.Add(new ShopStuff("look", "Gentleman", 750, 2002, "look02"));
        shelfLook.Add(new ShopStuff("look", "Nerd", 3000, 2003, "look03"));
        shelfLook.Add(new ShopStuff("look", "Bandit", 5500, 2004, "look04"));
        shelfLook.Add(new ShopStuff("look", "Pirate", 8500, 2005, "look05"));
    }



    void makeShelfSmoke(){

        shelfSmoke = new List<ShopStuff>();

        shelfSmoke.Add(new ShopStuff("smoke", "Cartoon", 0, 3001, "smoke01"));
        shelfSmoke.Add(new ShopStuff("smoke", "Natural", 1250, 3002, "smoke02"));
        shelfSmoke.Add(new ShopStuff("smoke", "Dirty", 3750, 3003, "smoke03"));
        shelfSmoke.Add(new ShopStuff("smoke", "Candy", 6500, 3004, "smoke04"));
    }



    void makeShelfs(){

        makeShelfTrack();
        makeShelfLook();
        makeShelfSmoke();
    }



    public bool clickBlock(ShopStuff _shopStuff){

        //Debug.Log("Click: " + _shopStuff.getNameTitle());

        if(_shopStuff.getPrice() > playerData.getMoney()){

            lvlNrInterfaceMnr.animatorMoneyNoEnoughStart();
            return false;
        }
        else{

            playerData.takeMoney(_shopStuff.getPrice());
            lvlNrInterfaceMnr.refreshTextMoney();
            return true;
        }
    }



    public void devResetBlockData(){

        shelfTrackObj.GetComponent<ShopShelfManager>().devResetBlockData();
        shelfLookObj.GetComponent<ShopShelfManager>().devResetBlockData();
        shelfSmokeObj.GetComponent<ShopShelfManager>().devResetBlockData();
        Destroy(shelfTrackObj);
        Destroy(shelfLookObj);
        Destroy(shelfSmokeObj);
        Start();
    }



    void refreshShelves(){

        //shelfTrackObj.GetComponent<ShopShelfManager>().refreshShelf();
        //shelfLookObj.GetComponent<ShopShelfManager>().refreshShelf();
        //shelfSmokeObj.GetComponent<ShopShelfManager>().refreshShelf();
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;



public class ShopBlockManager : MonoBehaviour {

    Vector3 targetPos;

    bool moveFL, animationFL, animationBuyFL, animationActiveOnFL, animationActiveOffFL;

    float step = 1000f;

    int deltaX = 154;

    [SerializeField]
    GameObject  buttonObj, canvasObj, tapPriseObj, tapActiveObj, titleObj, lockerObj,
                canvasImgObj, tabPriseImgObj, tabActiveImgObj, titleImgObj, lockerImgObj,
                tabPriseTxtObj, titleTxtObj, tabActiveTxtObj;

    Image       buttonImage, canvasImage, tabPriseImage, tabActiveImage, titleImage, lockerImage;

    Button      button;

    Text        tabPriseTxt, tabActiveTxt, titleTxt;

    [SerializeField]
    ShopBlockDataManager    blockData;

    List<ShopStuff> shopStuff;
    int listIndex;
    SpriteAtlas spriteAtlas;

    PlayerData playerData;

    int state;      //0 to buy, 1 owned, 2 active

    ShopManager shopManager;
    ShopShelfManager shopShelfManager;


    void Start(){

        buttonImage = buttonObj.GetComponent<Image>();
        button = buttonObj.GetComponent<Button>();

        canvasImage = canvasImgObj.GetComponent<Image>();
        tabPriseImage = tabPriseImgObj.GetComponent<Image>();
        tabActiveImage = tabActiveImgObj.GetComponent<Image>();
        titleImage = titleImgObj.GetComponent<Image>();
        lockerImage = lockerImgObj.GetComponent<Image>();

        tabPriseTxt = tabPriseTxtObj.GetComponent<Text>();
        titleTxt = titleTxtObj.GetComponent<Text>();
        tabActiveTxt = tabActiveTxtObj.GetComponent<Text>();

        checkIfShowBlock();
        //devTest();
    }



    void Update(){


        if(moveFL){

            checkIfShowBlock();

            if(transform.localPosition != targetPos){

                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, step * Time.deltaTime);
            }
            else{

                moveFL = false;
                if(transform.localPosition.x > 2*deltaX) {

                    transform.localPosition = new Vector3 (-2 * deltaX, 0, 0);
                    loopPositionStuffOnList(-1, 5);
                }
                else if (transform.localPosition.x < -2*deltaX){

                    transform.localPosition = new Vector3 (2 * deltaX, 0, 0);
                    loopPositionStuffOnList(1, 5);
                }
                //Debug.Log("on position");
            }
        }


        if(animationFL){

            if(animationBuyFL){

                tapPriseObj.transform.localPosition = Vector3.MoveTowards(tapPriseObj.transform.localPosition, new Vector3(-30, 100, 0), 300f * Time.deltaTime);

                setAlphaImage((100 - tapPriseObj.transform.localPosition.y) / 63, tabPriseImage);
                setAlphaTxt((100 - tapPriseObj.transform.localPosition.y) / 63, tabPriseTxt);

                lockerObj.transform.localPosition = Vector3.MoveTowards(lockerObj.transform.localPosition, new Vector3(-97, -6, 0), 300f * Time.deltaTime);
                setAlphaImage((97 + lockerObj.transform.localPosition.x) / 63, lockerImage);

                if(lockerObj.transform.localPosition.x == -97f && tapPriseObj.transform.localPosition.x == 100f){
                    animationBuyFL = false;
                    animationFL = false;
                }
            }

            if(animationActiveOnFL){

                tapActiveObj.transform.localPosition = Vector3.MoveTowards(tapActiveObj.transform.localPosition, new Vector3(-30, 37, 0), 300f * Time.deltaTime);

                float tmpAlpha = (100 - tapActiveObj.transform.localPosition.y) / 63;
                if(!button.interactable) {
                    tmpAlpha = 0;
                }
                setAlphaImage(tmpAlpha, tabActiveImage);
                setAlphaTxt(tmpAlpha, tabActiveTxt);

                if(tapActiveObj.transform.localPosition.y == 37f){
                    animationActiveOnFL = false;
                    animationFL = false;
                }
            }

            if(animationActiveOffFL){

                tapActiveObj.transform.localPosition = Vector3.MoveTowards(tapActiveObj.transform.localPosition, new Vector3(-30, 100, 0), 300f * Time.deltaTime);

                float tmpAlpha = (100 - tapActiveObj.transform.localPosition.y) / 63;
                if(!button.interactable) {
                    tmpAlpha = 0;
                }
                setAlphaImage(tmpAlpha, tabActiveImage);
                setAlphaTxt(tmpAlpha, tabActiveTxt);

                if(tapActiveObj.transform.localPosition.y == 100f){
                    animationActiveOnFL = false;
                    animationFL = false;
                    tapActiveObj.SetActive(false);
                }
            }

        }


        if(tapActiveObj.activeSelf && state < 2 && !animationFL){

            setActiveOff();
        }

    }



    public void setTargetPos(int _direction, int _deltaX){

        deltaX = _deltaX;
        targetPos.x = transform.localPosition.x + (_direction * _deltaX);
        //Debug.Log("transform.localPosition: " + transform.localPosition +"\ntargetPos: " + targetPos);
        moveFL = true;
    }



    public bool getMoveFL(){

        return moveFL;
    }



    public void setButtonImage(Sprite _sprite){

        if (buttonImage != null){
            buttonImage.sprite = _sprite;
        }
        else{
            buttonObj.GetComponent<Image>().sprite = _sprite;
        }
    }



//********************************************************

    void devTest(){

        setPrice(7901);
        setAlphaImage(0.5f, buttonImage);
    }

//*********************************************************



    void setPrice(int _hwMn){

        if (tabPriseTxt != null){
            tabPriseTxt.text = _hwMn.ToString() + "$";
        }
        else{
            tabPriseTxtObj.GetComponent<Text>().text = _hwMn.ToString() + "$";
        }
    }



    void setTitle(string _title){

        if(titleTxt != null){
            titleTxt.text = _title;
        }
        else{
            titleTxtObj.GetComponent<Text>().text = _title;
        }
    }



    void setAlphaImage(float _hwMn, Image _img){

        _img.color = new Color(1f, 1f, 1f, _hwMn);
    }



    void setAlphaTxt(float _hwMn, Text _txt){

        _txt.color = new Color(1f, 1f, 1f, _hwMn);
    }



    void checkIfShowBlock(){

        float tmpAlpha = 1f - (Mathf.Abs(transform.localPosition.x) - deltaX) / deltaX;

        if (transform.localPosition.x < -deltaX || transform.localPosition.x > deltaX){

            setAlphaImagesAll(tmpAlpha);
            button.interactable = false;
        }
        else{

            setAlphaImagesAll(1f);
            button.interactable = true;
        }
    }



    void setAlphaImagesAll(float _hwMn){

        setAlphaImage(_hwMn, buttonImage);
        setAlphaImage(_hwMn, canvasImage);
        setAlphaImage(_hwMn, tabPriseImage);
        setAlphaImage(_hwMn, tabActiveImage);
        setAlphaImage(_hwMn, titleImage);
        setAlphaImage(_hwMn, lockerImage);

        setAlphaTxt(_hwMn, tabPriseTxt);
        setAlphaTxt(_hwMn, titleTxt);
        setAlphaTxt(_hwMn, tabActiveTxt);
    }



    public void setShopStuff(List<ShopStuff> _shopStuff, int _listIndex, SpriteAtlas _spriteAtlas, PlayerData _playerData, bool _showTitleFL, ShopManager _shopManager, ShopShelfManager _shopShelfManager){

        shopShelfManager = _shopShelfManager;
        shopManager = _shopManager;
        playerData = _playerData;
        shopStuff = _shopStuff;
        listIndex = _listIndex;
        spriteAtlas = _spriteAtlas;


        loadShopStuff();

        if(!_showTitleFL){

            titleObj.SetActive(false);
        }

        checkStuffState();
    }



    void loadShopStuff(){

        setButtonImage( spriteAtlas.GetSprite( shopStuff[listIndex].getNameImage() ) );
        setPrice( shopStuff[listIndex].getPrice() );
        setTitle( shopStuff[listIndex].getNameTitle() );

        checkStuffState();
    }



    public void setSpriteAtlas(SpriteAtlas _spriteAtlas){

        spriteAtlas = _spriteAtlas;
    }



    void loopPositionStuffOnList(int _dir, int _shelfSize){
        //Debug.Log("IN listIndex: " + listIndex);
        int tmpStepMove = _dir * _shelfSize;
        listIndex += tmpStepMove;
        //Debug.Log("+stepMove listIndex: " + listIndex);
        while(listIndex < 0){

            listIndex += shopStuff.Count;
            //Debug.Log("<0 listIndex: " + listIndex);
        }
        while(listIndex > shopStuff.Count - 1){

            listIndex -= shopStuff.Count;
            //Debug.Log(">COUNT listIndex: " + listIndex);
        }

        loadShopStuff();
    }



    int checkStuffState(){

        string tmpName = "shp" + shopStuff[listIndex].getInvNr().ToString();
        state = playerData.shopStuffGetState(tmpName);

        if(state>1){
            tapActiveObj.SetActive(true);
            tapActiveObj.transform.localPosition =  new Vector3(-30, 37, 0);
        }
        else{
            tapActiveObj.SetActive(false);
        }
        if(state>0){
            tapPriseObj.SetActive(false);
            lockerObj.SetActive(false);
        }
        else{
            tapPriseObj.SetActive(true);
            lockerObj.SetActive(true);
        }

        return state;
    }



    public void setStuffState(int _nr){

        state = _nr;
        string tmpName = "shp" + shopStuff[listIndex].getInvNr().ToString();
        playerData.shopStuffSetState(tmpName, state);
    }



    public void blockButton(){

        if(checkStuffState() == 0){

            if ( shopManager.clickBlock(shopStuff[listIndex]) ){
                animationFL = true;
                animationBuyFL = true;
                setStuffState(1);
            }
        }
        else if(checkStuffState() == 1){

            animationFL = true;
            animationActiveOnFL = true;
            animationActiveOffFL = false;
            shopShelfManager.activeOffAllStuff();

            setStuffState(2);
            playerData.shopStuffSetActive(shopStuff[listIndex].getNameType(), shopStuff[listIndex].getNameImage());

            tapActiveObj.transform.localPosition =  new Vector3(-30, 100, 0);
            setAlphaImage(0, tabActiveImage);
            setAlphaTxt(0, tabActiveTxt);

            tapActiveObj.SetActive(true);
        }

    }



    public void setActiveOff(){

        animationFL = true;
        animationActiveOffFL = true;
        setStuffState(1);
        resetTabActive();
    }



    public void setOffIfActive(){

        if(checkStuffState() == 2){
            setActiveOff();
        }
    }



    public void devResetBlockData(){

        setStuffState(0);

        tapPriseObj.transform.localPosition =  new Vector3(-30, 37, 0);
        setAlphaImage(1, tabPriseImage);
        setAlphaTxt(1, tabPriseTxt);
        lockerObj.transform.localPosition = new Vector3(-34, -6, 0);
        setAlphaImage(1, lockerImage);
        animationFL = false;
        animationBuyFL = false;

        tapPriseObj.SetActive(true);
        tapActiveObj.SetActive(false);

        resetTabActive();

        animationFL = false;
        animationBuyFL = false;
        animationActiveOnFL = false;
        animationActiveOffFL = false;

        if(shopStuff[listIndex].getPrice() == 0){

            setStuffState(2);
        }

        checkStuffState();
    }



    void resetTabActive(){

        tapActiveObj.transform.localPosition =  new Vector3(-30, 37, 0);
        setAlphaImage(1, tabActiveImage);
        setAlphaTxt(1, tabActiveTxt);
    }




}

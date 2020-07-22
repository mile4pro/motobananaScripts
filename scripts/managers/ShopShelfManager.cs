using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;



public class ShopShelfManager : MonoBehaviour {

    [SerializeField]
    GameObject blockPrefab, blocksObj, nextBtnObj, preBtnObj;
    GameObject[] block;
    ShopBlockManager[] blockMgr;

    int deltaX = 154;

    bool activeButtonsFL = true;

    [SerializeField]
    SpriteAtlas     spriteAtlas;

    List<ShopStuff> shopStuff;

    PlayerData      playerData;

    /*void Start(){

        makeBlocks(5);
    }*/



    void Update(){

        if(!activeButtonsFL){
            if (checkIfBlocksMove()){
                setActiveButtons(true);
            };
        }
    }



    void makeBlocks(int _hwMn, bool _showTitleFL, ShopManager _shopManager){

        block = new GameObject[_hwMn];
        blockMgr = new ShopBlockManager[_hwMn];
        int tmpDeltaX;

        //string tmpNameType = shopStuff[0].getNameType();
        int tmpStuffIndex;

        for (int i = 0; i < _hwMn; i++){

            tmpDeltaX = ((-2)* deltaX) + (i * deltaX);
            //block[i] = Instantiate(blockPrefab, new Vector3(tmpDeltaX, 0, 0), new Quaternion(0, 0, 0, 1), transform);
            block[i] = Instantiate(blockPrefab, blocksObj.transform, false);
            block[i].transform.localPosition = new Vector3(tmpDeltaX, 0, 0);
            blockMgr[i] = block[i].GetComponent<ShopBlockManager>();

            /*int tmpNr = i + 1;
            blockMgr[i].setButtonImage(spriteAtlas.GetSprite("look" + tmpNr.ToString("00")));*/
            tmpStuffIndex = i;
            if(tmpStuffIndex > shopStuff.Count-1){
                tmpStuffIndex = i - shopStuff.Count;
            }
            //blockMgr[i].setButtonImage( spriteAtlas.GetSprite( shopStuff[tmpStuffIndex].getNameImage() ) );
            blockMgr[i].setShopStuff(shopStuff, tmpStuffIndex, spriteAtlas, playerData, _showTitleFL, _shopManager, this);
        }
    }



    public void nextBtn(){

        moveBlocks(-1);
        setActiveButtons(false);
    }

    public void preBtn(){

        moveBlocks(1);
        setActiveButtons(false);
    }



    void moveBlocks(int _dir){

        foreach (ShopBlockManager block in blockMgr)
            {
                block.setTargetPos(_dir, deltaX);
                //block.transform.localPosition = Vector3.MoveTowards(blockObj.transform.localPosition, target.position, step);
            }
    }



    void setActiveButtons(bool _FL){

            nextBtnObj.GetComponent<Button>().interactable = _FL;
            preBtnObj.GetComponent<Button>().interactable = _FL;
            activeButtonsFL = _FL;

    }



    bool checkIfBlocksMove(){

        foreach (ShopBlockManager block in blockMgr){

            if(block.getMoveFL()){

                return false;
            }
        }
        return true;
    }



    public void loadShelf(List<ShopStuff> _shopStuff, SpriteAtlas _spriteAtlas, PlayerData _playerData, bool _showTitleFL, ShopManager _shopManager){

        playerData = _playerData;
        shopStuff = _shopStuff;
        spriteAtlas = _spriteAtlas;

        checkActiveStuffBeginGame();

        makeBlocks(5, _showTitleFL, _shopManager);

    }



    public void activeOffAllStuff(){

        foreach(ShopBlockManager block in blockMgr){

            block.setOffIfActive();
        }
    }



    public void devResetBlockData(){

        foreach(ShopBlockManager block in blockMgr){

            block.devResetBlockData();
        }
        nextBtn();
    }



    void checkActiveStuffBeginGame(){

        bool tmpFL = true;
        foreach (ShopStuff stuff in shopStuff){
            string tmpName = "shp" + stuff.getInvNr().ToString();
            int tmpState = playerData.shopStuffGetState(tmpName);
            if(tmpState == 2){
                tmpFL = false;
                break;
            }
        }
        if(tmpFL){
            string tmpName = "shp" + shopStuff[0].getInvNr().ToString();
            playerData.shopStuffSetState(tmpName, 2);
        }
    }



    public void refreshShelf(){
        activeOffAllStuff();
        checkActiveStuffBeginGame();
        Debug.Log("refreshShelf ...OK");
    }

}

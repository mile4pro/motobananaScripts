using UnityEngine;
using UnityEngine.U2D;
using System.Collections.Generic;


public class AudienceManager : MonoBehaviour {


    [SerializeField]
    SpriteAtlas     fansAtlas;

    [SerializeField]
    GameObject      fanBigPre, fanSmallPre,
                    grandpaPre,
                    parentObj;

    GameObject      tmpGrandpa;

    List<GameObject>    fansList;

    Sprite          stand;

    int typeFansRange;




    string getBigFanSpriteName(int _nrFan){
        return BananasTypesFans.getFanBigName(_nrFan);
    }


    bool setSprite(Sprite _sprite, string _nameSprite){
        _sprite = fansAtlas.GetSprite(_nameSprite);
        return true;
    }



    public bool clearFansList(){

        foreach (GameObject fan in fansList)
            {
                Destroy(fan);
            }
        fansList.Clear();

        if(tmpGrandpa != null){

            Destroy(tmpGrandpa);
        }

        return true;
    }



    public bool makeFans(int _lvl, int _trackNr, GameObject[] _opponent){

        if (fansList != null) {clearFansList();}
        else {fansList = new List<GameObject>();}

        //typeFansRange = ((int)_lvl/10) + 2;
        typeFansRange = (int)Mathf.Min(((_lvl/10) + 2), 8);

        Debug.Log("audience _trackNr: " + _trackNr);

        switch (_trackNr)
            {
            case 0:
                fansDownStand0(_lvl);
                fansKidsRight(_lvl);
                fansKidsLeft(_lvl);
                break;

            case 1:
                makeFansTrack2(_lvl);
                break;

            case 2:
                makeGrandpa(_lvl, _opponent);
                makeFansTrack3(_lvl);
                break;

            default:
                Debug.Log("audience _trackNr problem....");
                break;
        }

        return true;
    }



    bool fansDownStand0(int _lvl){
        float stepX = Random.Range(.01f, .3f);
        //stand0
        for(float tmpPosX=-3.4f+stepX; tmpPosX<3.4; tmpPosX+=stepX){

            float tmpPosY = Random.Range(-6.025f, -5.975f);
            float tmpRotateZ = 0.1f - (0.2f * (Mathf.Abs(tmpPosX-3.4f)/6.8f));

            if(Random.value*100 < _lvl){
                GameObject tmpObj = Instantiate(fanBigPre, new Vector3(tmpPosX, tmpPosY, 0.0f), new Quaternion(0, 0 , tmpRotateZ, 1), gameObject.transform);
                //tmpObj.AddComponent<Sprite>();
                //Quaternion.identity
                //string tmpNameSprite = getBigFanSpriteName(Random.Range(0, typeFansRange));
                //setSprite(tmpObj.GetComponent<SpriteRenderer>().sprite, tmpNameSprite);

                tmpObj.GetComponent<SpriteRenderer>().sprite = fansAtlas.GetSprite(BananasTypesFans.getFanBigName(Random.Range(0, typeFansRange)));

                fansList.Add(tmpObj);
            }

            stepX = Random.Range(.4f, .5f);

        }
        return true;
    }
    /*public bool makeFans(int _lvl){

        if (fansList != null) {clearFansList();}
        else {fansList = new List<GameObject>();}

        int typeFansRange = 4;

        float stepX = Random.Range(.4f, .5f);
        //stand0
        for(float tmpPosX=-3.4f+stepX; tmpPosX<3.4; tmpPosX+=stepX){

            GameObject tmpObj = new GameObject("stand0Fan" + tmpPosX);
            tmpObj.AddComponent<Sprite>();
            string tmpNameSprite = getBigFanSpriteName(Random.Range(0, typeFansRange));
            setSprite(tmpObj.GetComponent<Sprite>(), tmpNameSprite);

            float tmpPosY = Random.Range(-6.025f, -5.975f);
            tmpObj.transform.position = new Vector3(tmpPosX, tmpPosY, 0.0f);

            fansList.Add(tmpObj);
        }

    }*/


    bool fansKidsRight(int _lvl){
        float stepY = Random.Range(0.01f, 0.1f);

        for(float tmpPosY=-5f+stepY; tmpPosY<5; tmpPosY+=stepY){

            float tmpPosX = Random.Range(3.6f, 3.7f);
            float tmpRotateZ = 1.5f - (1f * (Mathf.Abs(tmpPosY-5f)/10f));

            if(Random.value*100 < _lvl && (tmpPosY > 1 || tmpPosY < -2)){
                GameObject tmpObj = Instantiate(fanSmallPre, new Vector3(tmpPosX, tmpPosY, 0.0f), new Quaternion(0, 0 , tmpRotateZ, 1), gameObject.transform);

                tmpObj.GetComponent<SpriteRenderer>().sprite = fansAtlas.GetSprite(BananasTypesFans.getFanSmallName(Random.Range(0, typeFansRange)));

                fansList.Add(tmpObj);
            }

            stepY = Random.Range(.4f, .6f);

        }
        return true;
    }


    bool fansKidsLeft(int _lvl){
        float stepY = Random.Range(0.01f, 0.1f);

        for(float tmpPosY=-5f+stepY; tmpPosY<5; tmpPosY+=stepY){

            float tmpPosX = Random.Range(-3.66f, -3.55f);
            float tmpRotateZ = -1.5f + (1f * (Mathf.Abs(tmpPosY-5f)/10f));

            if(Random.value*100 < _lvl){
                GameObject tmpObj = Instantiate(fanSmallPre, new Vector3(tmpPosX, tmpPosY, 0.0f), new Quaternion(0, 0 , tmpRotateZ, 1), gameObject.transform);

                tmpObj.GetComponent<SpriteRenderer>().sprite = fansAtlas.GetSprite(BananasTypesFans.getFanSmallName(Random.Range(0, typeFansRange)));

                fansList.Add(tmpObj);
            }

            stepY = Random.Range(.4f, .6f);

        }
        return true;
    }



    bool makeGrandpa(int _lvl, GameObject[] _opponent){

        //GameObject tmpObj = Instantiate(grandpaPre, new Vector3(tmpPosX, tmpPosY, 0.0f), new Quaternion(0, 0 , tmpRotateZ, 1), gameObject.transform);
        if(tmpGrandpa != null) {tmpGrandpa = null;}
        tmpGrandpa = Instantiate(grandpaPre, gameObject.transform);
        tmpGrandpa.GetComponent<GrandpaManager>().setOpponent(_opponent);
        tmpGrandpa.GetComponent<GrandpaManager>().randomRotation();
        return true;
    }




    bool makeOneFan(    int _lvl,
                        int _hwSize,        //_hwSize 0=big 1=small,
                        float _posX, float _spreadPosX,
                        float _posY, float _spreadPosY,
                        float _rotateZ, float _spreadRotateZ,
                        int _orderInLayer)
    {

        if(Random.value*100 < _lvl){

            float tmpPosX = _posX + Random.Range(-_spreadPosX, _spreadPosX);
            float tmpPosY = _posY + Random.Range(-_spreadPosY, _spreadPosY);
            float tmpRotateZ = (_rotateZ + Random.Range(-_spreadRotateZ, _spreadRotateZ)) / 90f;
            Debug.Log("makeOneFam tmpRotateZ: " + tmpRotateZ);
            GameObject tmpObj = null;

            if(_hwSize == 0){

                tmpObj = Instantiate(fanBigPre, new Vector3(tmpPosX, tmpPosY, 0.0f), new Quaternion(0, 0 , tmpRotateZ, 1), gameObject.transform);
                tmpObj.GetComponent<SpriteRenderer>().sprite = fansAtlas.GetSprite(BananasTypesFans.getFanBigName(Random.Range(0, typeFansRange)));
                tmpObj.GetComponent<SpriteRenderer>().sortingOrder = _orderInLayer;
            }
            else if (_hwSize == 1){

                 tmpObj = Instantiate(fanSmallPre, new Vector3(tmpPosX, tmpPosY, 0.0f), new Quaternion(0, 0 , tmpRotateZ, 1), gameObject.transform);
                 tmpObj.GetComponent<SpriteRenderer>().sprite = fansAtlas.GetSprite(BananasTypesFans.getFanSmallName(Random.Range(0, typeFansRange)));
                 tmpObj.GetComponent<SpriteRenderer>().sortingOrder = _orderInLayer;
            }

            fansList.Add(tmpObj);
        }

        return true;
    }


//*************************************
    //******************
    //track 2
    void makeFansTrack2(int _lvl){

        int tmpLayerNr = 0;

        makeOneFan(_lvl, 1, -2.617f, 0.1f, -3.364f, 0.05f, -75f, 5f, 2);    //fall tree left
        makeOneFan(_lvl, 1, -1.364f, 0.1f, -3.364f, 0.05f, 80f, 5f, 2);    //fall tree left

        makeOneFan(_lvl, 1, 0.075f, 0.1f, -3.122f, 0.05f, 75f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, -1.12f, 0.1f, -0.212f, 0.05f, 75f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, -2.895f, 0.1f, -2.218f, 0.05f, -75f, 5f, tmpLayerNr);

        makeOneFan(_lvl, 1, 0.726f, 0.1f, -1.236f, 0.05f, -87f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, 0.726f, 0.1f, -1.764f, 0.05f, -87f, 5f, tmpLayerNr);

        makeOneFan(_lvl, 1, 0.764f, 0.1f, 0.9f, 0.05f, -90f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, 0.764f, 0.1f, 1.3f, 0.05f, -90f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, 0.764f, 0.1f, 1.7f, 0.05f, -90f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, 0.764f, 0.1f, 2.1f, 0.05f, -90f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, 0.764f, 0.1f, 2.5f, 0.05f, -90f, 5f, tmpLayerNr);
        //makeOneFan(_lvl, 1, 0.613f, 0.1f, 2.9f, 0.05f, -70f, 5f, 1);
        makeOneFan(_lvl, 1, 0.35f, 0.1f, 3.1f, 0.05f, -30f, 5f, tmpLayerNr);

        makeOneFan(_lvl, 1, -0.48f, 0.1f, 3.39f, 0.05f, 81f, 5f, tmpLayerNr);    //midle up right corner

        makeOneFan(_lvl, 1, -3.9f, 0.1f, -5.52f, 0.05f, -81f, 5f, tmpLayerNr);    //left down outside corner
        makeOneFan(_lvl, 1, -3.05f, 0.1f, -6.3f, 0.05f, -20f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, 2.75f, 0.1f, -6.3f, 0.05f, 7.5f, 5f, tmpLayerNr);

        makeOneFan(_lvl, 1, 3.8f, 0.1f, -4.9f, 0.05f, 85f, 5f, tmpLayerNr);    //right outside down
        makeOneFan(_lvl, 1, 3.8f, 0.1f, -4.55f, 0.05f, 85f, 5f, tmpLayerNr);    //right outside down
        //makeOneFan(_lvl, 1, 3.8f, 0.1f, -4.2f, 0.05f, 85f, 5f, 1);    //right outside down
        makeOneFan(_lvl, 1, 3.8f, 0.1f, -3.9f, 0.05f, 85f, 5f, tmpLayerNr);    //right outside down
        makeOneFan(_lvl, 1, 3.8f, 0.1f, -3.55f, 0.05f, 85f, 5f, tmpLayerNr);    //right outside down

        makeOneFan(_lvl, 1, 3.78f, 0.1f, 0.25f, 0.05f, 90f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 0.6f, 0.05f, 95f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 0.95f, 0.05f, 100f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 1.3f, 0.05f, 105f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 1.65f, 0.05f, 110f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 2f, 0.05f, 115f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 2.35f, 0.05f, 120f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 2.7f, 0.05f, 125f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 3.05f, 0.05f, 127.5f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 3.4f, 0.05f, 130f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 3.75f, 0.05f, 132.5f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 4.1f, 0.05f, 135f, 5f, tmpLayerNr);    //right outside middle
        makeOneFan(_lvl, 1, 3.78f, 0.1f, 4.45f, 0.05f, 137.5f, 5f, tmpLayerNr);    //right outside middle

        makeOneFan(_lvl, 1, -4f, 0.1f, 5f, 0.05f, -115f, 5f, tmpLayerNr);    //left up outside
        makeOneFan(_lvl, 1, -4f, 0.1f, 4.65f, 0.05f, -112.5f, 5f, tmpLayerNr);    //left up outside
        makeOneFan(_lvl, 1, -4f, 0.1f, 4.3f, 0.05f, -110f, 5f, tmpLayerNr);    //left up outside
        makeOneFan(_lvl, 1, -4f, 0.1f, 3.95f, 0.05f, -107.5f, 5f, tmpLayerNr);    //left up outside

        makeOneFan(_lvl, 1, -2.626f, 0.1f, 1.757f, 0.05f, -90f, 5f, tmpLayerNr);    //firs up left knife

        makeOneFan(_lvl, 1, -4f, 0.1f, 0f, 0.05f, -90f, 5f, tmpLayerNr);    //left up outside
        makeOneFan(_lvl, 1, -4f, 0.1f, -0.35f, 0.05f, -90f, 5f, tmpLayerNr);    //left up outside
        makeOneFan(_lvl, 1, -4f, 0.1f, -0.7f, 0.05f, -90f, 5f, tmpLayerNr);    //left up outside
        makeOneFan(_lvl, 1, -3.9f, 0.1f, -1.05f, 0.05f, -90f, 5f, tmpLayerNr);    //left up outside

        makeOneFan(_lvl, 1, -3.5f, 0.1f, -1.75f, 0.05f, -80f, 5f, tmpLayerNr);    //left up outside

        makeOneFan(_lvl, 0, -0.3f, 0.1f, -6.4f, 0.05f, 0f, 5f, tmpLayerNr);    //big down
        makeOneFan(_lvl, 0, 0.2f, 0.1f, -6.4f, 0.05f, 0f, 5f, tmpLayerNr);    //big down
    }



//*************************************
    //*******************
    //track 3
    void makeFansTrack3(int _lvl){

        int tmpLayerNr = 0;

        makeOneFan(_lvl, 1, -3.55f, 0.1f, -2.35f, 0.05f, -102.5f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, -3.6f, 0.1f, -2.7f, 0.05f, -97.5f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, -3.65f, 0.1f, -3.05f, 0.05f, -92.5f, 5f, tmpLayerNr);

        makeOneFan(_lvl, 1, -3.7f, 0.1f, -3.9f, 0.05f, -90f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, -3.675f, 0.1f, -4.25f, 0.05f, -85f, 5f, tmpLayerNr);

        makeOneFan(_lvl, 1, -3.1f, 0.1f, -5.15f, 0.05f, -50f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, -2.825f, 0.1f, -5.5f, 0.05f, -37.5f, 5f, tmpLayerNr);
        makeOneFan(_lvl, 1, -2.51f, 0.1f, -5.75f, 0.05f, -32.5f, 5f, tmpLayerNr);

        makeOneFan(_lvl, 1, 2.38f, 0.025f, -0.175f, 0.025f, 50f, 2.5f, 2);  //tree right
        makeOneFan(_lvl, 1, 2.57f, 0.01f, 0.187f, 0.01f, 37.5f, 1f, 2);  //tree right

        makeOneFan(_lvl, 1, -2.14f, 0.07f, 5.15f, 0.05f, -204f, 2f, tmpLayerNr);  //left up
        makeOneFan(_lvl, 1, -2.5f, 0.07f, 5.0f, 0.05f, -189f, 2f, tmpLayerNr);  //left up
        makeOneFan(_lvl, 1, -2.85f, 0.07f, 4.75f, 0.05f, -174f, 2f, tmpLayerNr);  //left up
        makeOneFan(_lvl, 1, -3.2f, 0.07f, 4.4f, 0.05f, -159f, 2f, tmpLayerNr);  //left up
        makeOneFan(_lvl, 1, -3.412f, 0.07f, 3.93f, 0.05f, -150f, 2f, tmpLayerNr);  //left up

        makeOneFan(_lvl, 1, 3.47f, 0.025f, 2.25f, 0.025f, 135f, 1.5f, tmpLayerNr);  //right pool up

        makeOneFan(_lvl, 1, 1.51f, 0.025f, 5.534f, 0.025f, 360f, 1.5f, tmpLayerNr);  //up center

        makeOneFan(_lvl, 1, 3.663f, 0.025f, -3.702f, 0.025f, 70f, 2.5f, tmpLayerNr);  //right down

        makeOneFan(_lvl, 1, -3.4f, 0.025f, 2.356f, 0.025f, -220f, 2.5f, tmpLayerNr);  //left uppper pool
    }
}

using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;



public class AnimationInformationInterfaceManager : MonoBehaviour {


    [SerializeField]
    Animator    animator;

    [SerializeField]
    GameObject  lvlNrInterfaceObj, trophyMapInterfaceObj;

    [SerializeField]
    GameObject  imgBckrDotObj, imgSplashObj, imgBigBananaObj,
                imgStripesObj, imgDotsObj,
                cupsObj, cupImgObj, cupBumniObj,
                txtNewCompetitorObj;

    [SerializeField]
    SpriteAtlas     bigBananasAnimInfInt, cupsAnimInfInt;

    [SerializeField]
    GameObject      buttonObj;

    bool trophyMapFL = false;



    public void showAnmiationInformation(int _nrMaxLvl, bool _trophyMapFL){

        trophyMapFL = _trophyMapFL;
        showAnmiationInformation(_nrMaxLvl);
    }



    public void showAnmiationInformation(int _nrMaxLvl){

        int tmpLvl = _nrMaxLvl - 1;
        bool tmpShowFL = true;

        switch (tmpLvl)
        {
            case 10:        //red

                setColorAndBanana(255f, 0f, 10f, tmpLvl);
                break;


            case 20:        //brown

                setColorAndBanana(255f, 225f, 0f, tmpLvl);
                break;


            case 30:        //aqua

                setColorAndBanana(93f, 203f, 198f, tmpLvl);
                break;


            case 40:        //darkBr

                setColorAndBanana(176f, 76f, 0f, tmpLvl);
                break;


            case 50:        //violet

                setColorAndBanana(235f, 0f, 255f, tmpLvl);
                break;


            case 60:        //gray

                setColorAndBanana(255f, 255f, 255f, tmpLvl);
                break;


            case 70:        //cup bronze

                setColorAndBanana(255f, 116f, 0f, tmpLvl);
                cupImgObj.GetComponent<SpriteRenderer>().sprite =  cupsAnimInfInt.GetSprite("cupBronze");
                setTextTitle("Bronze Banana Cup\nis yours!");
                break;


            case 80:        //cup silver

                setColorAndBanana(255f, 255f, 255f, tmpLvl);
                cupImgObj.GetComponent<SpriteRenderer>().sprite =  cupsAnimInfInt.GetSprite("cupSilver");
                setTextTitle("Silver Banana Cup\nis yours!");
                break;


            case 90:        //cup gold

                setColorAndBanana(253f, 215f, 29f, tmpLvl);
                //setColorAndBanana(251f, 236f, 80f, tmpLvl);
                cupImgObj.GetComponent<SpriteRenderer>().sprite =  cupsAnimInfInt.GetSprite("cupGold");
                setTextTitle("Gold Banana Cup\nis yours!");
                break;


            case 100:        //Bumni king

                setColorAndBanana(255f, 255f, 0f, tmpLvl);
                setTextTitle("YOU ARE\nmotoBanana King!");
                break;


            default:

                tmpShowFL = false;
                break;
        }


        if (tmpShowFL) {animatorNewCompetitorShowStart();}
        else {animatorAnimationInformationInterfaceHideEnd();}

    }




    void setColorAndBanana(float _r, float _g, float _b, int _tmpLvl){

        float tmpR, tmpG, tmpB;
        tmpR = _r/256f;
        tmpG = _g/256f;
        tmpB = _b/256f;

        Color tmpColor = new Color(tmpR, tmpG, tmpB);

        //tmpColor.a = 0.2235f;
        imgBckrDotObj.GetComponent<SpriteRenderer>().color = tmpColor;

        imgSplashObj.GetComponent<SpriteRenderer>().color = tmpColor;


        if (_tmpLvl < 70){

            imgBigBananaObj.SetActive(true);
            cupsObj.SetActive(false);
            cupBumniObj.SetActive(false);
            setActiveBckrImgAnimation(true);
            string tmpBananaName = "big" + BananasTypes.getBananasTypes()[(_tmpLvl/10)+1];
            imgBigBananaObj.GetComponent<SpriteRenderer>().sprite =  bigBananasAnimInfInt.GetSprite(tmpBananaName);
            setTextTitle("New Competitor arrived!");
        }
        else if (_tmpLvl < 100){

            imgBigBananaObj.SetActive(false);
            cupsObj.SetActive(true);
            cupBumniObj.SetActive(false);
            setActiveBckrImgAnimation(true);
        }
        else {

            imgBigBananaObj.SetActive(false);
            cupsObj.SetActive(false);
            cupBumniObj.SetActive(true);
            setActiveBckrImgAnimation(false);
        }


        int tmpNrStripes = imgStripesObj.transform.childCount;
        for (int i=0; i<tmpNrStripes; i++){
            imgStripesObj.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
        }

        int tmpNrDots = imgDotsObj.transform.childCount;
        for (int i=0; i<tmpNrDots; i++){
            imgDotsObj.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
        }


    }



    void setTextTitle(string _text){

        txtNewCompetitorObj.GetComponent<Text>().text = _text;
    }



    void setActiveBckrImgAnimation(bool _FL){

        imgBckrDotObj.SetActive(_FL);
        imgSplashObj.SetActive(_FL);
        imgDotsObj.SetActive(_FL);
        imgStripesObj.SetActive(_FL);
    }



    void animatorNewCompetitorShowStart(){

        buttonObj.GetComponent<Button>().interactable = false;
        animator.SetBool("newCompetitorShowFL", true);
    }



    public void animatorNewCompetitorShowEnd(){

        animator.SetBool("newCompetitorShowFL", false);
        buttonObj.GetComponent<Button>().interactable = true;
    }



    void animatorAnimationInformationInterfaceHideStart(){

        animator.SetBool("animInfoInteHideFL", true);
    }



    public void animatorAnimationInformationInterfaceHideEnd(){

        animator.SetBool("animInfoInteHideFL", false);

        if (trophyMapFL){

            trophyMapFL = false;
            trophyMapInterfaceObj.GetComponent<TrophyMapInterfaceManager>().animatorShowStart();
        }
        else{

            startLvlNrInterface();
        }

        gameObject.SetActive(false);
    }



    public void pressButtonMenu(){

        animatorAnimationInformationInterfaceHideStart();
        buttonObj.GetComponent<Button>().interactable = false;
    }



    void startLvlNrInterface(){

        lvlNrInterfaceObj.SetActive(true);
        lvlNrInterfaceObj.GetComponent<LvlNrInterfaceManager>().animatorStartShow();
    }
}

using UnityEngine;
using UnityEngine.U2D;

public class TransitionBananasManager : MonoBehaviour {

    [SerializeField]
    SpriteAtlas     bigBananasAtlas;

    [SerializeField]
    GameObject      bigBananasObj;

    [SerializeField]
    Animator animator;

    SpriteRenderer[]    bigBananasRendererArr;

    string[] bigBananasSpriteNameArr;



    void Start(){
        initBigBananasSpriteNameArr();
        initBigBananasRendererArr();
        mixBananasSprites();
    }



    bool initBigBananasSpriteNameArr(){

        bigBananasSpriteNameArr = new string[8];
        bigBananasSpriteNameArr[0] = "bigBlue";
        bigBananasSpriteNameArr[1] = "bigBrown";
        bigBananasSpriteNameArr[2] = "bigDark";
        bigBananasSpriteNameArr[3] = "bigDarkBrown";
        bigBananasSpriteNameArr[4] = "bigGreen";
        bigBananasSpriteNameArr[5] = "bigRed";
        bigBananasSpriteNameArr[6] = "bigViolet";
        bigBananasSpriteNameArr[7] = "bigYellow";

        return true;
    }



    bool initBigBananasRendererArr(){

        bigBananasRendererArr = new SpriteRenderer[bigBananasObj.transform.childCount];
        for (int i=0; i<bigBananasRendererArr.Length; i++){
            bigBananasRendererArr[i] = bigBananasObj.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
            bigBananasRendererArr[i].sprite = bigBananasAtlas.GetSprite(bigBananasSpriteNameArr[i]);
        }

        return true;
    }



    public void animatorStartBigBananaTransistor(){
        gameObject.SetActive(true);
        float tmpRotateZ = Random.Range(0, 360);
        Vector3 tmpRotateVec3 = new Vector3(0, 0, tmpRotateZ);
        bigBananasObj.transform.Rotate(tmpRotateVec3);
        if(bigBananasRendererArr != null){mixBananasSprites();};

        animator.SetBool("bigBananasTransitionFL", true);

    }


    public void animatorEndBigBananaTransistor(){
        
        animator.SetBool("bigBananasTransitionFL", false);
        gameObject.SetActive(false);
    }



    bool mixBananasSprites(){

        int tmpRandomIndex;
        int tmpArrLenght = bigBananasRendererArr.Length;
        Sprite tmpSprite;

        for (var i = 0; i < tmpArrLenght; i++){
            tmpRandomIndex = Random.Range(0, tmpArrLenght - 1);
            tmpSprite = bigBananasRendererArr[i].sprite;
            bigBananasRendererArr[i].sprite = bigBananasRendererArr[tmpRandomIndex].sprite;
            bigBananasRendererArr[tmpRandomIndex].sprite = tmpSprite;
        }

        return true;
    }



}

using UnityEngine;

public class CountingDownManager : MonoBehaviour {

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject gameMngrObj;

    [SerializeField]
    AudioSource codo3, codo2, codo1, codoGo;


    public void animatorStartCounting(){
        gameObject.SetActive(true);
        animator.SetBool("countingFL", true);
        //Debug.Log("animatorStartCounting()");
    }

    public void animatorEndCounting(){
        animator.SetBool("countingFL", false);
        gameObject.SetActive(false);
    }

    public void setOnAllCars(){

        GameMngr gameMngr = gameMngrObj.GetComponent<GameMngr>();
        gameMngr.setOnAllCars();
        gameMngr.statsAddStartRaceCount();
    }


//sounds counting down
    public void playCodo3(){
        codo3.Play();
    }
    public void playCodo2(){
        codo2.Play();
    }
    public void playCodo1(){
        codo1.Play();
    }
    public void playCodoGo(){
        codoGo.Play();
    }
}

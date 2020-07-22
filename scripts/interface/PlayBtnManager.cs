using UnityEngine;

public class PlayBtnManager : MonoBehaviour {

    [SerializeField]
    Animator animator;

    public void animatorStartHideBtn(){
        gameObject.SetActive(true);
        animator.SetBool("hideFL", true);
    }

    public void animatorEndHideBtn(){
        animator.SetBool("hideFL", false);
        gameObject.SetActive(false);
    }


}

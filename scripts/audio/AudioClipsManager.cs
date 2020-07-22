using UnityEngine;



public class AudioClipsManager : MonoBehaviour {

    [SerializeField]
    AudioSource     click01, upgrade01;




    public void playClick01(){

        click01.Play();
    }


    public void playUpgrade01(){

        upgrade01.Play();
    }
}

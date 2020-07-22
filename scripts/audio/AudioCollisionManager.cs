using UnityEngine;

public class AudioCollisionManager : MonoBehaviour {

    [SerializeField]
    AudioClip[]     filesSundCollision;

    [SerializeField]
    AudioSource[]   audioCollision;

    int     audioHwMn, randomNr;



    void Start(){

        audioHwMn = filesSundCollision.Length;
        loadAudioCollision();
    }



    void loadAudioCollision(){

        for (int i = 0; i < audioHwMn; i++){
            audioCollision[i].clip = filesSundCollision[i];
            audioCollision[i].pitch = .25f;
        }
    }



    public void playSoundCollision(float _impactForce, bool _endRaceFL){

        if (_impactForce > 0.5f){

            randomNr = (int)(Random.value * audioHwMn);
            if(randomNr == 4) {randomNr = 3;}

            audioCollision[randomNr].volume = _impactForce/7.5f;
            if(_endRaceFL) {audioCollision[randomNr].volume *= 0.2f;}

            audioCollision[randomNr].Play();
        }
    }


}

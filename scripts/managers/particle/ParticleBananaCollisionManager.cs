using UnityEngine;



public class ParticleBananaCollisionManager : MonoBehaviour {

    [SerializeField]
    ParticleSystem particleSystem;

    ParticleSystem.MainModule mainModule;
    ParticleSystem.EmissionModule emissionModule;
    ParticleSystemRenderer rendererModule;


    void Start(){

        mainModule = particleSystem.main;
        emissionModule = particleSystem.emission;
        rendererModule = gameObject.GetComponent<ParticleSystemRenderer>();
    }



    public void setParticleColor(int _bananaType){

        switch (_bananaType)
        {

            case 1:        //green

                setColor(0f, 255f, 10f);
                break;

            case 2:        //red

                setColor(255f, 0f, 10f);
                break;


            case 3:        //brown

                setColor(255f, 225f, 0f);
                break;


            case 4:        //aqua

                setColor(93f, 203f, 198f);
                break;


            case 5:        //darkBr

                setColor(176f, 76f, 0f);
                break;


            case 6:        //violet

                setColor(235f, 0f, 255f);
                break;


            case 7:        //gray

                setColor(255f, 255f, 255f);
                break;

            default:
                break;
        }
    }



    void setColor(float _r, float _g, float _b){

        float tmpR, tmpG, tmpB;
        tmpR = _r/256f;
        tmpG = _g/256f;
        tmpB = _b/256f;

        Color tmpColor = new Color(tmpR, tmpG, tmpB);

        particleSystem.startColor = tmpColor;
    }



    public void bananaCollision(Collision2D _coll){

        Vector2 tmpRelVec = _coll.relativeVelocity;
        //Debug.Log("tmpRelVec: " + tmpRelVec);
        if(tmpRelVec.magnitude > 2.25f && !particleSystem.isPlaying){
            //Debug.Log("boooooooom!!!...");
            transform.position = _coll.GetContact(0).point;

            mainModule.startSpeed = tmpRelVec.magnitude*3f;
            mainModule.duration = tmpRelVec.magnitude/2.5f;
            emissionModule.SetBurst(0, new ParticleSystem.Burst(0.0f, (short)(tmpRelVec.magnitude)));
            particleSystem.Play();
        }
    }



    public void setMask(bool _FL){

        if (!_FL){

            rendererModule.maskInteraction = SpriteMaskInteraction.None;
        }
        else{

            rendererModule.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
    }

}

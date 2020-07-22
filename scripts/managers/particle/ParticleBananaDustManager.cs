using UnityEngine;

public class ParticleBananaDustManager : MonoBehaviour {

    [SerializeField]
    ParticleSystem particleSystem;

    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.MainModule mainModule;
    ParticleSystemRenderer rendererModule;

    [SerializeField]
    Material[] dustMaterial;
    string dustMaterialName = "smoke01";


    void Start(){

        emissionModule = particleSystem.emission;
        mainModule = particleSystem.main;
        rendererModule = gameObject.GetComponent<ParticleSystemRenderer>();
        setDustMaterial(dustMaterialName);
    }



    public void setDustRate(float _hwMn){

        emissionModule.rateOverDistance = _hwMn;

    }



    public void setDustSize(float _sizeFactor){

        mainModule.startSize = new ParticleSystem.MinMaxCurve(0.575f*_sizeFactor, 1.75f*_sizeFactor);

    }


    public void setDustStartColor(float _r, float _g, float _b, float _a){

        mainModule.startColor = new Color(_r, _g, _b, _a);
    }



    public void startColorDust(int _nrTypeBanana){

        switch (_nrTypeBanana)
      {
            case 1:     //green banana

                setDustStartColor(0f, 1f, 0f, 1f);
            break;


            case 2:     //red banana

                setDustStartColor(1f, 0f, 0f, 1f);
            break;


            case 3:     //brown banana

                //setDustStartColor(0.6328125f, 0.6328125f, 0.12890625f, 1f);       //old
                setDustStartColor(0.5660378f, 0.5622642f, 0f, 1f);
            break;


            case 4:     //aqua banana

                setDustStartColor(0.365f, 0.793f, 0.773f, 1f);
            break;


            case 5:     //dark brown banana

                setDustStartColor(0.6875f, 0.296875f, 0f, 1f);
            break;


            case 6:     //violet banana

                setDustStartColor(0.91796875f, 0f, 1f, 1f);
            break;


            case 7:     //gray banana

                setDustStartColor(0.25f, 0.25f, 0.25f, 1f);
            break;


            default:    //bumni

                setDustStartColor(1f, 1f, 0f, 1f);
            break;
      }
    }



    public void stopParticle(){

        particleSystem.Stop();
    }




    public bool checkParticleIsStopped(){

        return particleSystem.isStopped;
    }



    public void setMask(bool _FL){

        if (!_FL){

            rendererModule.maskInteraction = SpriteMaskInteraction.None;
        }
        else{

            rendererModule.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
    }



    public void setDustMaterialName(string _string){

        dustMaterialName = _string;
    }



    void setDustMaterial(string _name){

        if(rendererModule != null){
            rendererModule = gameObject.GetComponent<ParticleSystemRenderer>();
        }

        switch(_name){

            case "smoke01":
                rendererModule.material = dustMaterial[0];
                break;


            case "smoke02":
                rendererModule.material = dustMaterial[1];
                break;


            case "smoke03":
                rendererModule.material = dustMaterial[2];
                break;


            case "smoke04":
                rendererModule.material = dustMaterial[3];
                break;

            default:
                Debug.Log("set dust material particle problem...");
                break;
        }
    }


}

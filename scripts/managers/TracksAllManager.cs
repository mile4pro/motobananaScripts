using UnityEngine;



public class TracksAllManager : MonoBehaviour {

    [SerializeField]
    GameObject track01;

    GameObject[] actualTrackSensors;
    Vector3[] actualStartingPlaces;



    void Start(){

        setActualTrackSensors();
        setActualStartingPlaces();
    }


    public void setActualTrackSensors(){

        actualTrackSensors = track01.GetComponent<TrackManager>().getSensors();
    }

    public GameObject[] getActualTrackSensors(){

        return actualTrackSensors;
    }


    public void setActualStartingPlaces(){

        actualStartingPlaces = track01.GetComponent<TrackManager>().getStartingPlaces();
    }

    public Vector3[] getActualStartingPlaces(){

        return actualStartingPlaces;
    }



}

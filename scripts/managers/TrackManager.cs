using UnityEngine;

public class TrackManager : MonoBehaviour {

    [SerializeField]
    GameObject sensorsObj, startingPlacesObj;

    GameObject[] sensors;

    Vector3[] startingPlaces;

    Quaternion[] startingPlacesRotation;

    [SerializeField]
    GameObject  posLapNrInfo, posPositionNrInfo, posRaceTimeInfo,
                posLapTimeInfo, posCountingDown,
                posMetaParticleEffectLeft, posMetaParticleEffectRight,
                posMetaCamera;



    public bool setSensors(){

        sensors = new GameObject[sensorsObj.transform.childCount];
        for (int i=0; i<sensors.Length; i++){
            sensors[i] = sensorsObj.transform.GetChild(i).gameObject;
        }

        return true;
    }



    public bool setStartingPlaces(){

        startingPlaces = new Vector3[startingPlacesObj.transform.childCount];
        startingPlacesRotation = new Quaternion[startingPlacesObj.transform.childCount];

        for (int i=0; i<startingPlaces.Length; i++){
            startingPlaces[i] = startingPlacesObj.transform.GetChild(i).position;
            startingPlacesRotation[i] = startingPlacesObj.transform.GetChild(i).rotation;
        }

        return true;
    }



    public GameObject[] getSensors(){

        return sensors;
    }



    public Vector3[] getStartingPlaces(){

        return startingPlaces;
    }



    public Quaternion[] getStartingPlacesRotation(){

        return startingPlacesRotation;
    }



    public void setMiddleBellElementsPosition(  GameObject _lapNrInfo,
                                                GameObject _positionNrInfo,
                                                GameObject _raceTimeInfo,
                                                GameObject _lapTimeInfo){

        setTransformGameObject(_lapNrInfo, posLapNrInfo);
        setTransformGameObject(_positionNrInfo, posPositionNrInfo);
        setTransformGameObject(_raceTimeInfo, posRaceTimeInfo);
        setTransformGameObject(_lapTimeInfo, posLapTimeInfo);
    }



    void setTransformGameObject(GameObject _goFor, GameObject _goFrom){

        RectTransform tmpRt = _goFor.GetComponent<RectTransform>();

        //Debug.Log("_goFrom.transform.position: " + _goFrom.transform.position);

        Vector3 tmpPos = _goFrom.transform.position,
                tmpScale = _goFrom.transform.localScale;

        Quaternion tmpRotation = _goFrom.transform.rotation;

        tmpRt.localPosition = tmpPos;
        tmpRt.localRotation = tmpRotation;
        tmpRt.localScale = tmpScale;
    }



    void setTransformGameObjectTransform(GameObject _goFor, GameObject _goFrom){

        Vector3 tmpPos = _goFrom.transform.position,
                tmpScale = _goFrom.transform.localScale;

        Quaternion tmpRotation = _goFrom.transform.rotation;

        _goFor.transform.position = tmpPos;
        _goFor.transform.rotation = tmpRotation;
        _goFor.transform.localScale = tmpScale;
    }



    public void setCountingDownPosition(GameObject _countingDownObj){

        setTransformGameObject(_countingDownObj, posCountingDown);
    }



    public void setMetaEffectPosition(GameObject _metaParticleEffectLeftObj, GameObject _metaParticleEffectRightObj, GameObject _metaCameraObj){

        setTransformGameObjectTransform(_metaParticleEffectLeftObj, posMetaParticleEffectLeft);
        setTransformGameObjectTransform(_metaParticleEffectRightObj, posMetaParticleEffectRight);
        setTransformGameObjectTransform(_metaCameraObj, posMetaCamera);
    }


}

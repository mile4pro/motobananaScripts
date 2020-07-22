using UnityEngine;

public class LvlManager : MonoBehaviour {


    public static int[] setLvl(int _nrTrack, int _nrLvl){

            int[] tmpArr = new int[3];

            tmpArr[0] = _nrLvl-1;          //hw mn opponent

            tmpArr[1] = (int)(((float)_nrLvl)/10f);    //nr start place player

            tmpArr[2] = _nrLvl;        //hw mn lap

            return tmpArr;
    }


}

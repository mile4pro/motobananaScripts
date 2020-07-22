using UnityEngine;
using UnityEngine.UI;


public class DebugCanvasInterfaceManager : MonoBehaviour {


    [SerializeField]
    GameObject txtDebug01Obj;
    Text txtDebug01;
    string txtDebugAll;


    void Start(){

        txtDebug01 = txtDebug01Obj.GetComponent<Text>();
    }



    public void setTxtDebug(string _txt, bool _clearFL = false){

        if (_clearFL){txtDebugAll = "";}
        txtDebugAll += ("\n" + _txt);
        txtDebug01.text = txtDebugAll;
    }


}

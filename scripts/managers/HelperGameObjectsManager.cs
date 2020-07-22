using UnityEngine;



public class HelperGameObjectsManager : MonoBehaviour {

        [SerializeField]
        GameObject RaceVisualEffectsObj;
        RaceVisualEffectsManager raceVisualEffectsMgr;


        void Start(){

            raceVisualEffectsMgr = RaceVisualEffectsObj.GetComponent<RaceVisualEffectsManager>();
        }



//******************************************
//race visual effect manager

        public RaceVisualEffectsManager getRaveVisualEffectMgr(){

            return raceVisualEffectsMgr;
        }



        public bool endRace(float _timeRace, bool _playerFL){

            bool tmpFL = false;
            raceVisualEffectsMgr.playMetaVisualParticleEffect(_timeRace, _playerFL);
            return tmpFL;
        }



        public void setFirstFL(bool _FL){

            raceVisualEffectsMgr.setFirstFL(_FL);
        }



        public void setRaceInterfacePosition(TrackManager _trackMgr){

            raceVisualEffectsMgr.setRaceInterfacePosition(_trackMgr);
        }


}

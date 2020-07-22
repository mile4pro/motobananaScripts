using UnityEngine;



public class ShopBlockDataManager : MonoBehaviour {

        int     state,          //0 on buy,   1 owned,   2 acive
                price,
                invNr;

        string  name;



        public void setState(int _state){

            state = _state;
        }



        public void setPrice(int _price){

            price = _price;
        }



        public void setInvNr(int _invNr){

            invNr = _invNr;
        }



        public void setName(string _name){

            name = _name;
        }



        public int getState(){

            return state;
        }



        public int getPrice(){

            return price;
        }



        public int getInvNr(){

            return invNr;
        }



        public string getName(){

            return name;
        }


}

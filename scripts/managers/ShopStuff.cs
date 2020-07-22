using System.Collections.Generic;



public class ShopStuff {


    public ShopStuff(   string _nameType,
                        string _nameTitle,
                        int _price,
                        int _invNr,
                        string _nameImage){

        nameType = _nameType;
        nameTitle = _nameTitle;
        price = _price;
        invNr = _invNr;
        nameImage = _nameImage;
    }

    int typeNr, invNr, price, state;
    string nameType, nameTitle, nameImage;



    public int getTypeNr(){

        return typeNr;
    }



    public int getInvNr(){

        return invNr;
    }



    public int getPrice(){

        return price;
    }



    public string getNameType(){

        return nameType;
    }



    public string getNameTitle(){

        return nameTitle;
    }



    public string getNameImage(){

        return nameImage;
    }



    public void setState(int _nr){

        state = _nr;
    }



    public int getState(){

        return state;
    }


}

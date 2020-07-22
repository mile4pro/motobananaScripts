

public class BananasTypes {

    //static string[] bananas = {"Yellow", "Aqua", "Brown", "Green", "Red", "DarkBr", "Violet", "Gray"};
    static string[] bananas = {"Yellow", "Green", "Red", "Brown", "Aqua", "DarkBr", "Violet", "Gray"};



    /*public BananasTypes(){
        initBananasTypesArr();
    }

    1. Green
    2. Red
    A potem obojętnie
    3. Brown
    4.Aqua
    5.DarkBR
    6.Violet
    i ostatni
    7.Gray

    public bool initBananasTypesArr(){

        bananas[0] = "Yellow";
        bananas[1] = "Aqua";
        bananas[2] = "Brown";
        bananas[3] = "Green";
        bananas[4] = "Red";

        return true;
    }*/



    public static string[] getBananasTypes(){
        return bananas;
    }



    public static string getBananSpriteName(int _nrType, string _sideName){

                switch(_sideName)
                {

                    case "turnLeft":
                        return (bananas[_nrType] + "LeftBanan");
                    break;

                    case "turnRight":
                        return (bananas[_nrType] + "RightBanan");
                    break;

                    default:
                        return (bananas[_nrType] + "NeutralBanan");
                    break;
                }
    }



    public static string getBananaName(int _nrBanana){
        return bananas[_nrBanana];
    }

}

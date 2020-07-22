

public class BananasTypes {

    static string[] bananas = {"Yellow", "Green", "Red", "Brown", "Aqua", "DarkBr", "Violet", "Gray"};

	
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

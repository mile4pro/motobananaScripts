

public class BananasTypesFans {


    static string[] fansBig = {     "fanBigYellow",
                                    "fanBigGreen",
                                    "fanBigRed",
                                    "fanBigBrown",
                                    "fanBigAqua",
                                    "fanBigDarkBrown",
                                    "fanBigViolet",
                                    "fanBigGrey"
                                };

    static string[] fansSmall = {   "fanSmallYellow",
                                    "fanSmallGreen",
                                    "fanSmallRed",
                                    "fanSmallBrown",
                                    "fanSmallAqua",
                                    "fanSmallDarkBrown",
                                    "fanSmallViolet",
                                    "fanSmallGray"
                                };



    public static string[] getFansBigTypes(){
        return fansBig;
    }

    public static string[] getFansSmallTypes(){
        return fansSmall;
    }

    public static string getFanBigName(int _nrFan){
        if (_nrFan > fansBig.Length-1) {_nrFan = fansBig.Length-1;}
        return fansBig[_nrFan];
    }

    public static string getFanSmallName(int _nrFan){
        if (_nrFan > fansSmall.Length-1) {_nrFan = fansSmall.Length-1;}
        return fansSmall[_nrFan];
    }

}

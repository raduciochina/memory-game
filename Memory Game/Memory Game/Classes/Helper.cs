using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Memory_Game_App.Classes
{
    public static class StringExtensions
    {
        public static int ToInteger(this string numberString)
        {
            int result = 0;
            if (int.TryParse(numberString, out result))
                return result;
            return 0;
        }
    }


    public static class Utils
    {
        public static bool IsConnectedToInternet()
        {
            return CrossConnectivity.Current.IsConnected;
        }
    }
}
//The Helper.cs file is composed of two classes: StringExtension and Utils.
//    The StringExtension class contains a ToIntenger() extension method to convert a valid numerical string value into an integer type.
//    The Utils class on the other hand contains an IsConnectedToInternet() method to verify internet connectivity. We will be using these methods later in our application.
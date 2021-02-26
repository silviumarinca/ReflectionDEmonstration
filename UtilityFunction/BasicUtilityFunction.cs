using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityFunction
{

    [Information(Description = "This class contains basic utility functions")]
   public class BasicUtilityFunction
    {
        [Information(Description = "This method return welcomeMessage")]
        public string WriteWelcomeMessage()
        {
            return "Welcome to basic utility functions class ";
        }
        [Information(Description = "This method return Integer sum")]
        public int IntegerPlusInteger(int operand1, int operand2)
        {
            return operand1 + operand2;
        }
        [Information(Description = "This method return concatenation strings")]
        public string ConcatThreeStrings(string string1, string string2, string string3) {

            return string1 + " " + string2 + " " + string3;
        }
        [Information(Description = "This method return string length")]

        public int GetStringLength(string stringValue)
        {
            return stringValue.Length;
        }
    }
}

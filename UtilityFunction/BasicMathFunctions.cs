using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityFunction
{
    [Information(Description = "This class does some basic math operations")]
  public  class BasicMathFunctions
    {
        [Information(Description = "This method divide two numbers")]
        public double DivideOperation(double number1, double number2)
        {
            return number1 / number2;
        }
        [Information(Description = "This method multiplies two numbers")]
        public double MultiplyOperation(double number1, double number2)
        {
            return number1 / number2;
        }
    }
}

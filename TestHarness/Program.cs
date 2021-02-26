using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            const string TargetAssemblyFileName = "UtilityFunction.dll";
            const string TargetNamespace = "UtilityFunction";

          Assembly assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,TargetAssemblyFileName));
            List<System.Type> classes=assembly.GetTypes()
                .Where(t => t.Namespace == TargetNamespace 
                && HasInformationAttribute(t)).ToList();
            while (true)
            {
                WritePromptToScreen("Please press the number key associated to the class you wish to test");

                Type classChoice = ReturnProgramElementReferenceFromList(classes);
                DisplayElementDescription(ReturnIformationCustomAttribute(classChoice));
                object classInstance = Activator.CreateInstance(classChoice);

                Console.Clear();
                WriteHeadingtoScreen($"Class: {classChoice}");
                WritePromptToScreen("Please enter the number associated with the method you wish");


                List<MethodInfo> methods = classChoice.GetMethods()
                    .Where(t => HasInformationAttribute(t)).ToList();

                DisplayProgramElementList<MethodInfo>(methods);
                MethodInfo methodChoice = ReturnProgramElementReferenceFromList(methods);

                if (methodChoice != null)
                {
                    Console.Clear();
                    WriteHeadingtoScreen($"Class '{classChoice}' Method: '{methodChoice.Name}' ");
                    DisplayElementDescription(ReturnIformationCustomAttribute(methodChoice));
                    ParameterInfo[] parameters = methodChoice.GetParameters();
                    object result = GetResult(classInstance, methodChoice, parameters);
                    WriteResultToScreen(result);
                }
                Console.WriteLine();
                WritePromptToScreen("Please press space to end app");

                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    return;
                }
            }
          
        }
        private static string InformationAttributeTypeName = "UTILITYFUNCTION.INFORMATIONATTRIBUTE";
        const    string InformationAttributePropertyName = "Description";
        private static string ReturnIformationCustomAttribute(MemberInfo info)
        {
            

            foreach ( var attrib in info.GetCustomAttributes())
            {
                Type typeAttribute = attrib.GetType();
                if (typeAttribute.Name == InformationAttributePropertyName)
                {
                    PropertyInfo propertyInfo= typeAttribute.GetProperty(InformationAttributePropertyName);
                    if (propertyInfo != null)
                    {
                        object s = propertyInfo.GetValue(attrib, null);
                        if (s != null)
                        {
                            return s.ToString();
                        }

                    }
                }

            }
            return null;

        }
        private static bool HasInformationAttribute(MemberInfo mi)
        {
           
            foreach (var attrib in mi.GetCustomAttributes())
            {
                Type typeattrib = attrib.GetType();
                if (typeattrib.ToString().ToUpper() == InformationAttributeTypeName)
                {
                    return true;
                }
            }return false;
        }

        private static void DisplayElementDescription(string element)
        {
            if (element != null)
            {
              
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(element);
                Console.ResetColor();
                Console.WriteLine();

            }

        }
        private static void WriteResultToScreen(object result)
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Result: {result}");
            Console.ResetColor();
            Console.WriteLine();
        }
   

        private static object[] ReturnPArameterValueInputAsObjectArray(ParameterInfo[] parameters)
        {
            object[] paramValues = new object[parameters.Length];
            int itemCount = 0;
            foreach (ParameterInfo parameterInfo in parameters)
            {
                WritePromptToScreen($"Please enter a value for the parameter name {parameterInfo.Name}");
                if (parameterInfo.ParameterType == typeof(string))
                {
                    string inputString = Console.ReadLine();
                    paramValues[itemCount] = inputString;

                }
                else
                if(parameterInfo.ParameterType == typeof(int)){

                    int inputString =Int32.Parse( Console.ReadLine());
                    paramValues[itemCount] = inputString;

                }else
                if (parameterInfo.ParameterType == typeof(double))
                {

                    double inputString = double.Parse(Console.ReadLine());
                    paramValues[itemCount] = inputString;

                }
                itemCount++;

            }
            return paramValues;
        }
        private static object GetResult(Object classInstance, MethodInfo info,ParameterInfo[] parameeters) {

            object result = null;
            if (parameeters.Length == 0)
            {
                result = info.Invoke(classInstance, null);
            }
            else
            {
                var paramValueArray = ReturnPArameterValueInputAsObjectArray(parameeters);
                result = info.Invoke(classInstance, paramValueArray);
            }
            return result;

        }
        public static void DisplayProgramElementList<T>(List<T>list)
        {
            int count = 0;
            foreach (var item in list)
            {
                count++;
                Console.WriteLine($"{count}. {item}");
            }

        }
        public static T ReturnProgramElementReferenceFromList<T>(List<T> list)
        {
            ConsoleKey consoleKey= Console.ReadKey().Key;
            switch (consoleKey)
            {
                case ConsoleKey.D1:
                    return list[0];
                   
                case ConsoleKey.D2:
                    return list[1];
                   
                case ConsoleKey.D3:
                    return list[2];
                  
                case ConsoleKey.D4:
                    return list[3];
                    


            }
            return default(T);
        }

        private static void WriteHeadingtoScreen(string heading)
        {
            Console.WriteLine(heading);
            Console.WriteLine(new string('-', heading.Length));
            Console.WriteLine();
        }

        private static void WritePromptToScreen(string promptText)
        {

            Console.WriteLine(promptText);
        }
    }
}

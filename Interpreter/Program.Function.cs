using Bits_Script_Interpreter.Interpreter.Log;
using Bits_Script_Interpreter.Program.Variable;
using System.Collections.Generic;

namespace Bits_Script_Interpreter.Program.Function
{
    static class Func
    {
        //Function List
        private static Dictionary<string, Function> functionList = new Dictionary<string, Function>();

        public static Dictionary<string, Function> FunctionList
        {
            get => functionList;
        }

        //Add a function
        public static void AddFunction(Function function) 
        {
            functionList.Add(function.functionName, new Function(function));
        }

        //Return a function by its name, if the function don't exist, throw an undifined function
        public static Function GetFunction(string functionName) 
        {
            return functionList.ContainsKey(functionName) ? functionList[functionName] : new Function();
        }

        //Return True if the function Exist else return false
        public static bool Exists(string key) 
        {
            if (functionList.ContainsKey(key)) { return true; }
            else { return false; }
        }

        //Check if we do have Argument when calling the function
        public static bool HasFunctionArs(string[] lineSection, int startIndex) 
        {
            int argumentCount;
            List<string> argumentList = new List<string>();
            string currentArgument = "";

            /* Coming Later...
            for(int i = startIndex; i < lineSection.Length; i++) 
            {
                string currentSection = lineSection[i];
                if(currentSection == "()") { return true; }
                else 
                {
                    if(currentSection[0] == '(') 
                    {

                        currentArgument += currentSection;
                        currentArgument += " ";
                        break;
                    }
                }
            }
            */

            return true;
        }
    }

    class Function 
    {
        //Name, description and list of variable for our function
        public string functionName;
        private string[] functionLine;
        public Dictionary<string, Var> functionVariable = new Dictionary<string, Var>();

        //Run the Function
        public void Run() 
        {
            for(int index = 0; index < functionLine.Length; index++) 
            {
                Interpreter.Interpreter.InterpreteLine(functionLine[index], index, this, functionLine, index);
            }
        }

        //Default Constructor
        public Function() 
        {
            functionName = "None_Implemented_System_Function";
            functionLine = new string[1] { "throw None_Implemented_Except" };
        }

        //Constructor for name
        public Function(string name, string[] content) 
        {
            functionName = name;

            functionLine = new string[content.Length];
            for(int i = 0; i < functionLine.Length; i++) 
            {
                functionLine[i] = content[i];
            } 
        }

        //Constructor For Copy
        public Function(Function copy) 
        {
            functionName = copy.functionName;
            functionVariable = copy.functionVariable;

            functionLine = new string[copy.functionLine.Length];
            for(int i = 0; i < functionLine.Length; i++) 
            {
                functionLine[i] = copy.functionLine[i];
            }
        }

        public override string ToString()
        {
            string output = $"FUNCTION : {functionName} ; function content : ";
            foreach(string section in functionLine) 
            {
                output += "\n";
                output += section;
            }
            return output;
        }
    }
}

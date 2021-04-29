using Bits_Script_Interpreter.Interpreter.Log;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Program.Variable;
using System.Collections.Generic;

namespace Bits_Script_Interpreter.Program.Function
{
    static class Func
    {
        //Function List
        private static Dictionary<string, Function> functionList = new Dictionary<string, Function>();

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
    }

    class Function 
    {
        //Name, description and list of variable for our function
        public string functionName;
        private string[] functionLine;
        public Dictionary<string, Var> functionVariable = new Dictionary<string, Var>();

        //Run the code
        public void Run() 
        {
            for(int index = 0; index < functionLine.Length; index++) 
            {
                Interpreter.Interpreter.InterpreteLine(functionLine[index], index, this);
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
    }
}

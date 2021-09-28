using System.Collections.Generic;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Program.Variable;

namespace Bits_Script_Interpreter.Program.Function 
{
    static class Program_Function 
    {
        //List of function(s) in the program
        private static Dictionary<string, Calleable> functions = new Dictionary<string, Calleable>();

        //Use to add a function
        public static void AddFunction(string name, string[] arguments, string[] lines)
        {
            Debug.Log($"Added function named {name}, With argument {Interpreter_String.AssembleArray<string, string>(arguments, 0, ";")}, line content is : {Interpreter_String.AssembleArray<string, char>(lines, 0, '\n')}", true);

            if(Exists(name)) Debug.Error(true, $"Function {name} already exist in the current context.");
            else 
            {
                if(arguments == null)
                {
                    functions.Add(name, new Calleable(name, lines));
                }else
                {
                    functions.Add(name, new Calleable(name, lines, arguments));
                }
            }

            Interpreter_Core_Variable.Set(true, lines.Length);
        }

        //Use to get a function by it's name
        public static Calleable GetFunction(string name)
        {
            if(!Exists(name)) Debug.Error(true, $"Function {name} does not exist in the current context");
            else
            {
                return functions[name];
            }
            return null;
        }

        //Check if a special function exist
        public static bool Exists(string name)
        {
            return functions.ContainsKey(name);
        }

        //Use to run a function
        public static object Run(string name, string[] arguments)
        {
            Interpreter_Core_Variable.Set(false,0);
            if(Exists(name))
            {
                Calleable func = functions[name];

                if(func.argumentsName != null)
                {
                    for(int i = 0; i < arguments.Length; i++)
                    {
                        Program_Variable.AddVariable(func.argumentsName[i], arguments[i]);
                        func.scopeVariable.Add(func.argumentsName[i]);
                    }
                }

                func.Run();
                object functionResult = func.ReturnValue;

                func.Flush();
                return functionResult;
            }else Debug.Error(true, $"Cannot call a function that does not exist (function : {name})");

            return null;
        }
    }

    //Function Class
    class Calleable
    {
        string name; //Function Name
        object returnValue; //it's return value

        public List<string> scopeVariable = new List<string>(); //The list of variable created in the function scope

        public object ReturnValue //Use to get and set the return value
        {
            get {return returnValue;}
            set {
                Debug.Log($"Return value is now {value}", true);
                returnValue = value;
            }
        }

        public string[] argumentsName = null; //List of argument name

        string[] lines; //Lines of code in the function

        public bool stopRunning = false; //If the function need to srop running

        //Default constructor
        public Calleable(string name, string[] lines)
        {
            this.name = name;
            this.lines = lines;
        }

        //Better Constructor
        public Calleable(string name, string[] lines, string[] argumentsName)
        {
            this.name = name;
            this.lines = lines;
            this.argumentsName = argumentsName;
        }

        //Run the function
        public void Run()
        {
            for(int i = 0; i < lines.Length; i++)
            {
                string current =  lines[i];

                if (stopRunning) break;

                if(Interpreter_Core_Variable.Get().jmp)
                {
                    Debug.Log("FUNCTION : jmp is true", true);
                    if(Interpreter_Core_Variable.Get().id == 0)
                    {
                        Debug.Log("FUNCTION : jmp is true but id is 0, setting off", true);
                        Interpreter.Interpreter.InterpreteLine(current, lines, i, scopeVariable, true, true, name);
                        Interpreter_Core_Variable.Set(false, 0);
                    }else
                    {
                        Debug.Log($"FUNCTION : jmp is true, and id is not 0, skipping line (id is {Interpreter_Core_Variable.Get().id})", true);
                        Interpreter_Core_Variable.Set(true, Interpreter_Core_Variable.Get().id - 1);
                        continue;
                    }
                }else
                {
                    Debug.Log($"Interpreting line ({current})", true);
                    Interpreter.Interpreter.InterpreteLine(current, lines, i, scopeVariable, true, true, name);
                }
            }
        }

        //Reset the function returnValue, argument value, and variable need to be called every time a function end or stop running
        //If not called this could break the function
        public void Flush()
        {
            returnValue = null;
            Program_Variable.DeleteScopeVariable(scopeVariable);
            scopeVariable.Clear();
            Debug.Log("Flushed function sucessfully", true);
        }
    }
}
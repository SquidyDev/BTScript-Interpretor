using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Program.Variable.Type_Definer;
using Bits_Script_Interpreter.Evaluator;
using Bits_Script_Interpreter.Interpreter;
using System.Collections.Generic;
using Bits_Script_Interpreter.Evaluator.String;
using Bits_Script_Interpreter.Evaluator.Bool;
using Bits_Script_Interpreter.Program.Function;

namespace Bits_Script_Interpreter.Program.Variable
{
    static class Program_Variable
    {
        public static readonly int GLOBAL_VARIABLE_SCOPE = 0; //Decapted Scope system

        //Variable list in the program
        public static Dictionary<string, Var> programVariables = new Dictionary<string, Var>();

        //Use to get the type of variable
        public static string GetType(object value) 
        {
            return Interpreter_Type.GetStringType(value);
        }

        //Check if a variable is of a specific type
        public static bool IsType(string variable, string type)
        {
            if(Exist(variable)) 
            {
                if(GetVariable(variable, false).type == type)
                {
                    return true;
                } 
            }else Debug.Error(true, $"ERROR : variable : {variable} does not exit in this scope.");
            return false;
        }

        //Use to add a variable (malloc = memory allocation (Name taken from C function malloc))
        public static void malloc(string[] block, List<string> scopeVariable, bool scoppedMalloc) 
        {
            Debug.Log($"Variable {block[0]} is {programVariables.ContainsKey(block[0])}", true);

            if (programVariables.ContainsKey(block[0])) 
            {
                string value = ChangeVariable(block[0], block);
                Debug.Log($"Setted new value ({value}) for variable {block[0]}", true);
                return;
            }
            else 
            {
                string name = block[0];

                if(scoppedMalloc) scopeVariable.Add(name);

                object value = Interpreter_String.AssembleArray<string, char>(block, 2, ' ');
                AddVariable(name, value);
            }
        }

        //Use to remove a variable by it's name
        public static void RemoveVariable(string name) 
        {
            if (Exist(name)) 
            {
                programVariables.Remove(name);
                Debug.Log($"Removed Variable {name}.", true);
            }else 
            {
                Debug.Error(true, $"ERROR : trying to remove a non existing variable (variable {name} don't exist in the current context.)");
            }
        }

        //Remove all the variable from a scope
        public static void DeleteScopeVariable(List<string> scopeVariable) 
        {
            foreach(string variable in scopeVariable)
            {
                RemoveVariable(variable);
            }
        }

        //Use to change a variable Value
        public static string ChangeVariable(string variable, string[] block) 
        {
            string newValue = Interpreter_String.AssembleArray<string, char>(block, 2, ' ');

            if(programVariables[variable].type == "double")
            {
                newValue = Interpreter_Evaluator.Evaluate(newValue).ToString();
            }

            if(programVariables[variable].type == "string")
            {
                newValue = Interpreter_String_Evaluator.Evaluate(newValue);
            }

            if (GetType(newValue) == programVariables[variable].type)
                programVariables[variable].value = newValue;
            else
                Debug.Error(true, "ERROR : can not change the type of a variable while changing is value.");

            return newValue;
        }

        //Use to check if a variable exist
        public static bool Exist(string variableName) 
        {
            DEBUG_PRINT_VAR_LIST();
            Debug.Log($"DEBUG : Variable {variableName.Trim()} is {programVariables.ContainsKey(variableName.Trim())}", true);
            if (programVariables.ContainsKey(variableName.Trim())) { return true; }
            else return false;
        }

        //Use to Add A variable (Use malloc to add variable)
        public static void AddVariable(string name, object value) 
        {
            if(value.ToString().Split(' ')[0] == "call") 
            {
                Debug.Log("Variable assignement by function.", true);

                string[] functionArgs = value.ToString().Split(' ');
                string funcName = functionArgs[1];

                string[] functionArgument = Interpreter_String.AssembleArray<string, char>(functionArgs, 2, ' ').Split('$');

                value = Program_Function.Run(funcName, functionArgument);

                Debug.Log($"Value new value is {value}", true);
            }

            string type = GetType(value);

            if(type == "none") { Debug.Error(true, $"Error, wrong type for variable ({name})."); }

            programVariables.Add(name, new Var(name, type, value));

            if(type == "double") 
            {
                programVariables[name].value = Interpreter_Evaluator.Evaluate(programVariables[name].value.ToString());
            }

            if(type == "string")
            {
                programVariables[name].value = Interpreter_String_Evaluator.Evaluate(programVariables[name].value.ToString());
            }

            if(type == "bool")
            {
                programVariables[name].value = Interpreter_Bool_Evaluator.Evaluate(value.ToString());
            }

            Debug.Log($"DEBUG : Created variable named {name}, of type {type}, with value {value}.", true);

            DEBUG_PRINT_VAR_LIST();
        }

        //Print the list of variable
        private static void DEBUG_PRINT_VAR_LIST() 
        {
            Debug.Log("Variable list is now : ", true);

            foreach (KeyValuePair<string, Var> entry in programVariables)
            {
                Debug.Log(entry.Value.name, true);
            }
        }

        //Return a variable by it's name
        public static Var GetVariable(string key, bool stop) 
        {
            if (programVariables.ContainsKey(key)) return programVariables[key];
            else /*Debug.Error(stop, $"ERROR : Usage of a non defined variable ({key})");*/ return null;
        }
    }

    [System.Serializable]
    class Var 
    {
        /*Name, value and type of a variable*/
        public string type;
        public object value;
        public string name;

        //Return true if the variable is a string
        public bool IsString()
        {
            if(type == "string") return true;
            else return false;
        }

        //Constructor for variable
        public Var(string name, string type, object value)
        {
            this.type = type;
            this.name = name;
            this.value = value;
        }
    }
}

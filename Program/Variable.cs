using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Program.Variable.Type_Definer;
using Bits_Script_Interpreter.Evaluator;
using Bits_Script_Interpreter.Interpreter;
using System.Collections.Generic;

namespace Bits_Script_Interpreter.Program.Variable
{
    static class Program_Variable
    {
        public static Dictionary<string, Var> programVariables = new Dictionary<string, Var>();

        public static string GetType(object value) 
        {
            return Interpreter_Type.GetStringType(value);
        }

        public static void malloc(string[] block) 
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
                object value = Interpreter_String.AssembleArray<string, char>(block, 2, ' ');
                AddVariable(name, value);
            }
        }

        public static string ChangeVariable(string variable, string[] block) 
        {
            string newValue = Interpreter_String.AssembleArray<string, char>(block, 2, ' ');
            if (GetType(newValue) == programVariables[variable].type)
                programVariables[variable].value = newValue;
            else
                Debug.Error(true, "ERROR : can not change the type of a variable while changing is value.");

            return newValue;
        }

        public static bool Exist(string variableName) 
        {
            DEBUG_PRINT_VAR_LIST();
            Debug.Log($"DEBUG : Variable {variableName} is {programVariables.ContainsKey(variableName)}", true);
            if (programVariables.ContainsKey(variableName)) { return true; }
            else return true;
        }

        public static void AddVariable(string name, object value) 
        {
            string type = GetType(value);

            if(type == "none") { Debug.Error(true, $"Error, wron type for variable ({name})."); }

            programVariables.Add(name, new Var(name, type, value));

            if(type == "int") 
            {
                programVariables[name].value = Interpreter_Evaluator.Evaluate(programVariables[name].value.ToString());
            }

            Debug.Log($"DEBUG : Created variable named {name}, of type {type}, with value {value}.", true);

            DEBUG_PRINT_VAR_LIST();
        }

        private static void DEBUG_PRINT_VAR_LIST() 
        {
            Debug.Log("Variable list is now : ", true);

            foreach (KeyValuePair<string, Var> entry in programVariables)
            {
                Debug.Log(entry.Value.name, true);
            }
        }

        public static Var GetVariable(string key, bool stop) 
        {
            if (programVariables.ContainsKey(key)) return programVariables[key];
            else /*Debug.Error(stop, $"ERROR : Usage of a non defined variable ({key})");*/ return null;
        }
    }

    [System.Serializable]
    class Var 
    {
        public string type;
        public object value;
        public string name;
        public string[] accesseur;

        public bool IsString()
        {
            if(type == "string") return true;
            else return false;
        }

        //Constructor
        public Var(string name, string type, object value)
        {
            this.type = type;
            this.name = name;
            this.value = value;
        }
    }
}

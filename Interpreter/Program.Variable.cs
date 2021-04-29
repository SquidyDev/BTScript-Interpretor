using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Interpreter.Log;
using Bits_Script_Interpreter.Interpreter.Interpreter_Variable;
using Bits_Script_Interpreter.Program.Function;
using System.Collections.Generic;
using System;

namespace Bits_Script_Interpreter.Program.Variable
{
    static class ProgramVariable
    {
        public static Dictionary<string, Var> variable = new Dictionary<string, Var>();
        public static List<string> TypeList = new List<string>();
        public static string[] type = { 
            "string",
            "int",
            "double",
            "bool",
            "char"
        };

        public static void EvaluateVariable(string variableToSendEvaluation, string[] variablesToEvaluate) 
        {
            if (variable.ContainsKey(variableToSendEvaluation)) 
            {
                foreach(string variableToEvaluate in variablesToEvaluate) 
                {
                    if (!variable.ContainsKey(variableToEvaluate)) 
                    {
                        Utilities.PrintError("Error usage of an non defined variable.", false);
                        Interpreter.Interpreter_Variable.Variable.isError = true;
                        return;
                    }
                }

                string variableType = variable[variableToSendEvaluation].type;
                
                if(variableType == "char" || variableType == "bool") 
                {
                    Utilities.PrintError("Error : Cannot evaluate char or bool.", false);
                    Interpreter.Interpreter_Variable.Variable.isError = true;
                    return;
                }else if(variableType == "int") 
                {
                    int[] variablesToEvaluateToInt = new int[variablesToEvaluate.Length];
                    for(int i = 0; i < variablesToEvaluate.Length; i++) 
                    {
                        variablesToEvaluateToInt[i] = Int32.Parse(variablesToEvaluate[i]);
                    }
                }
            }
        }

        public static bool IsString(object value) 
        {
            if(Interpreter_String.GetWord(value.ToString()) == value.ToString()) { return false; }
            else { return true; }
        }

        public static bool VariableTypeMatchVariableValue(string variableType, object variableValue) 
        {
            switch (variableType) 
            {
                case "string":
                    if (IsString(variableValue)) { return true; }
                    else { return false; }
                    break;
                case "double":
                    if (Interpreter_String.ContainsSingleChar(variableValue.ToString(), '.') && !Interpreter_String.ContainsSingleChar(variableValue.ToString(), '"')) { return true; }
                    else { return false; }
                    break;
                case "int":
                    if (!IsString(variableValue)) { return true; }
                    else { return false; }
                    break;
                case "bool":
                    if(variableValue.ToString() == "true" || variableValue.ToString() == "false") { return true; }
                    else { return false; }
                    break;
                case "char":
                    if (variableValue.ToString().Length == 3 && IsString(variableValue)) { return true; }
                    else { return false; }
                    break;
            }
            return false;
        }

        public static void Init() 
        {
            foreach(string varType in type) 
            {
                TypeList.Add(varType);
            }
        }

        public static void Check() 
        {
            foreach(string t in TypeList) 
            {
                Console.WriteLine(t);
            }
        }

        public static bool IsType(string type) 
        {
            if (TypeList.Contains(type)) { return true; }
            else { return false; }
        }

        public static Var GetVariable(string key, bool isFunction, Function.Function func) 
        {
            if (isFunction) { return func.functionVariable[key];  }
            else { return variable[key]; }
        }

        public static bool Exists(string key, bool isFunction, Function.Function func) 
        {
            if (isFunction) { return func.functionVariable.ContainsKey(key); }
            else
            {
                if (variable.ContainsKey(key)) 
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
        }

        public static void AddVariable(string variableName, object variableContent, bool isFunction, Function.Function func, string variableType) 
        {
            if (isFunction) 
            {
                func.functionVariable.Add(variableName, new Var(variableContent, new string[0], variableType));
            }
            else 
            {
                variable.Add(variableName, new Var(variableContent, new string[0], variableType));
            }
        }

        public static void SetVariable(string key, object value, bool isFunction, Function.Function func, string variableType) 
        {
            if (isFunction) 
            {
                func.functionVariable[key].value = value;

                if(!VariableTypeMatchVariableValue(variableType, value)) 
                {
                    Utilities.PrintError($"Error : New value for variable {key} is not the same type as the variable type ({variableType})", false);
                }
            }
            else 
            {
                variable[key].value = value;

                if(!VariableTypeMatchVariableValue(variableType, value)) 
                {
                    Utilities.PrintError($"Error : New value for variable {key} is not the same type as the variable type ({variableType})", false);
                }
            }
        }
    }

    class Var 
    {
        public object value;
        public string[] variableArgs;
        public string type;

        public Var() 
        {
            value = null;
        }

        public Var(object value) 
        {
            this.value = value;
        }

        public Var(object value, string[] variableArgs, string type) 
        {
            this.value = value;
            this.variableArgs = variableArgs;
            this.type = type;
        }

        public bool IsType(string type)
        {
            if(this.type == type) { return true; }
            else { return false; }
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}

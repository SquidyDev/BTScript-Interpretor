using System.Collections.Generic;
using System;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Evaluator;
using Bits_Script_Interpreter.Evaluator.String;
using Bits_Script_Interpreter.Evaluator.Bool;
using Bits_Script_Interpreter.Program.Variable;

namespace Bits_Script_Interpreter.Program.Variable.Type_Definer
{
    static class Interpreter_Type
    {
        public enum BuildInType 
        {
            None,
            String,
            Double, 
            Char,
            Bool
        }

        public static bool IsString(object value) 
        {
            Debug.Log("DEBUG : Checking For String", true);

            if (Interpreter_String.RemoveQuoteMarks(value.ToString()) != value.ToString()) //Example : if base value "Hello" is not equal to process value Hello, it is a string
            {
                Debug.Log("DEBUG : Is String", true);
                return true;
            }
            return false;
        }

        public static bool IsChar(object value) 
        {
            Debug.Log("DEBUG : Checking For Char", true);

            if (Interpreter_String.HowManyTime(value.ToString(), "'".ToCharArray()[0], 2, false)) 
            {
                if(value.ToString().Length == 3) { Debug.Log("DEBUG : Is char", true); return true; }
                else { Debug.Error(true, "ERROR : char only contains one character"); return false; }
            }
            else return false;
        }

        private static BuildInType GetType(object value) 
        {
            if (IsChar(value)) { return BuildInType.Char; }
            else if (Interpreter_Evaluator.CanBeEvaluated(value.ToString())) { return BuildInType.Double; }
            else if (Interpreter_Bool_Evaluator.CanEvaluate(value.ToString())) { return BuildInType.Bool; }
            else if (Interpreter_String_Evaluator.CanBeEvaluated(value.ToString())) { return BuildInType.String; }
            else { return BuildInType.None; }
        }

        public static string GetStringType(object value) 
        {
            BuildInType type = GetType(value.ToString().Trim());

            switch(type) 
            {           
                case BuildInType.Bool:
                    return "bool";
                case BuildInType.Char:
                    return "char";
                case BuildInType.Double:
                    return "double";
                case BuildInType.String:
                    return "string";
            }

            return "none";
        }
    }
}

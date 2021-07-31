using System;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Evaluator;

namespace Bits_Script_Interpreter.Program.Variable.Type_Definer
{
    static class Interpreter_Type
    {
        public enum BuildInType 
        {
            None,
            String,
            Int,
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

        public static bool IsInt(object value) 
        {
            Debug.Log("DEBUG : Checking For int", true);

            if (!Interpreter_String.HowManyTime(value.ToString(), '.', 0, false)) { return false; }
            else 
            {
                try 
                {
                    Convert.ToInt64(value);
                    Debug.Log("DEBUG : Is Int", true);
                    return true;
                }
                catch 
                {
                    return false;
                }
            }
        }

        public static bool IsDouble(object value) 
        {
            Debug.Log("DEBUG : Checking For Double", true);

            if (Interpreter_String.HowManyTime(value.ToString(), '.', 1, false) && Interpreter_String.HowManyTime(value.ToString(), '"', 0, false) && Interpreter_String.HowManyTime(value.ToString(), "'".ToCharArray()[0], 0, false))
            {
                Debug.Log("DEBUG : Is Double", true);
                return true;
            }
            else 
            {
                return false;
            }
        }

        public static bool IsBool(object value) 
        {
            Debug.Log("DEBUG : Checking For Bool", true);

            if (value.ToString() == "false" || value.ToString() == "true") { Debug.Log("DEBUG : Is Bool", true); return true; }
            else { return false; }
        }

        private static BuildInType GetType(object value) 
        {
            if (IsString(value)) { return BuildInType.String; }
            else if (IsChar(value)) { return BuildInType.Char; }
            else if (Interpreter_Evaluator.CanBeEvaluated(value.ToString())) { return BuildInType.Int; }
            else if (IsDouble(value)) { return BuildInType.Double; }
            else if (IsBool(value)) { return BuildInType.Bool; }
            else { return BuildInType.None; }
        }

        public static string GetStringType(object value) 
        {
            BuildInType type = GetType(value);

            if (type == BuildInType.String) return "string";
            if (type == BuildInType.Bool) return "bool";
            if (type == BuildInType.Char) return "char";
            if (type == BuildInType.Int) return "int";
            if (type == BuildInType.Double) return "double";
            if (type == BuildInType.None) return "none";
            return "none";
        }
    }
}

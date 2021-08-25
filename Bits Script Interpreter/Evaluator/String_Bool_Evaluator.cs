using System.Collections.Generic;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Evaluator.String.Core;
using Bits_Script_Interpreter.Program.Variable;

namespace Bits_Script_Interpreter.Evaluator.String.Bool
{
    static class Interpreter_String_Bool_Evaluator
    {
        public static bool Evaluate(string expression)
        {
            string[] parsedEvaluation = ParseEvaluation(expression);
            Debug.Log(Interpreter_String.AssembleArray<string, char>(parsedEvaluation, 0, '\n'), true);
            bool result = Interpreter_String_Bool_Evaluator_Core.ResolveEvaluation(parsedEvaluation);
            Debug.Log($"The result of the evaluation is {result}", true);
            return result;
        }

        static string[] ParseEvaluation(string expression)
        {
            return Interpreter_String_Evaluator_Core.ParseEvaluation(expression);
        }

        static class Interpreter_String_Bool_Evaluator_Core
        {
            static bool Equal(string a, string b) {return a == b;}
            static bool NotEqual(string a, string b) {return a != b;}

            public static bool ResolveEvaluation(string[] expression)
            {
                bool result = true;

                string Operator = expression[1].Trim();

                switch (Operator)
                {
                    case "==":
                        result = Equal(expression[0].Trim(), expression[2].Trim());
                        break;
                    case "!=":
                        result = NotEqual(expression[0].Trim(), expression[2].Trim());                        
                        break;
                    default:
                        Debug.Error(true, $"ERROR : unknow operator ({Operator}) for string evaluation.");
                        break;
                }

                return result;
            }
        }
    }
}
using System.Collections.Generic;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Evaluator.String.Core;
using Bits_Script_Interpreter.Program.Variable;

namespace Bits_Script_Interpreter.Evaluator.String
{
    static class Interpreter_String_Evaluator
    {
        public static bool CanBeEvaluated(string expression)
        {
            try 
            {
                Evaluate(expression);
                Debug.Log($"Expression {expression} can be evaluated so it is a string", true);
                return true;
            }catch
            {
                Debug.Log($"Expression {expression} can't be evaluated so it is not a string", true);
                return false;
            }
        }

        public static string Evaluate(string expression)
        {
            string[] parsedExpression = ParseEvaluation(expression);

            Debug.Log(Interpreter_String.AssembleArray<string, char>(parsedExpression, 0, ' '), true);

            string result = String_Evaluator_Core.ResolveEvaluation(parsedExpression);

            Debug.Log($"The result of the evaluation is {result}", true);

            return result;
        }

        static string[] ParseEvaluation(string evaluation)
        {
            return Interpreter_String_Evaluator_Core.ParseEvaluation(evaluation);
        }

        static class String_Evaluator_Core
        {
            static string Add(string a, string b) {return a + b;}

            public static string ResolveEvaluation(string[] expr)
            {
                string last = expr[0].Trim();

                for(int i = 1; i < expr.Length; i++)
                {
                    string current = expr[i];

                    if(current.Contains('+'))
                    {
                        last = Add(last, expr[i + 1].Trim());
                        i++; 
                    }
                }

                return Interpreter_String.RemoveQuoteMarks(last);
            }
        }
    }
}
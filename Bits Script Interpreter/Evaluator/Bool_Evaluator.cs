using System;
using System.Collections.Generic;
using Bits_Script_Interpreter.Evaluator;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Program.Variable;
using Bits_Script_Interpreter.Interpreter.String;

namespace Bits_Script_Interpreter.Evaluator.Bool
{
    static class Interpreter_Bool_Evaluator
    {
        /*Check if the expression can be evealuate as a bool*/
        public static bool CanEvaluate(string expression)
        {
            try 
            {
                bool? result = Evaluate(expression);

                if(result == null) return false;

                Debug.Log($"Expression {expression} can be evaluated so it is a bool", true);
                return true;
            }catch
            {
                Debug.Log($"Expression {expression} can't be evaluated so it is not a bool", true);
                return false;
            }
        }

        /*use to replace all the variable in the operation by there value*/
        public static string[] ReplaceVariable(string[] parsed)
        {
            for(int i = 0; i < parsed.Length; i++)
            {
                string current = parsed[i];

                if(Program_Variable.Exist(current))
                {
                    parsed[i] = Program_Variable.GetVariable(current, true).value.ToString();
                }
            }

            return parsed;
        }

        /*Replace True & False by number*/
        public static string[] ReplaceBoolean(string[] parsed)
        {
            string[] output = parsed;

            for(int i = 0; i < output.Length; i++)
            {
                string current = output[i];

                if(current.Trim() == "true") output[i] = int.MaxValue.ToString();
                else if(current.Trim() == "false") output[i] = int.MinValue.ToString();
            }

            return output;
        }

        /*use to parse the evaluation*/
        public static string[] ParseEvaluation(string expression)
        {
            return ReplaceBoolean(ReplaceVariable(expression.Split(' ')));
        }

        /*Evaluate a string to return a bool?*/
        public static bool? Evaluate(string expression)
        {
            string[] parsedExpression = ParseEvaluation(expression);
            parsedExpression = Interpreter_String.RemoveAllInArray<string>(parsedExpression, "{");
            Debug.Log(Interpreter_String.AssembleArray<string, string>(parsedExpression, 0, "--"), true);

            bool? result = Bool_Evaluator_Core.ResolveEvaluation(parsedExpression);
            Debug.Log($"The result is {result}.", true);

            return result;
        }

        public static class Bool_Evaluator_Core
        {
            /*Following functions are evalution function*/
            public static bool Greater(string a, string b) { return double.Parse(a) > double.Parse(b); }
            public static bool GreaterOrEqual(string a, string b) { return double.Parse(a) >= double.Parse(b); }
            public static bool Lesser(string a, string b) { return double.Parse(a) < double.Parse(b); }
            public static bool LesserOrEqual(string a, string b) { return double.Parse(a) <= double.Parse(b); }
            public static bool Equal(string a, string b) { return double.Parse(a) == double.Parse(b); }
            public static bool NotEqual(string a, string b) { return double.Parse(a) != double.Parse(b); }

            /*Use to solve the evaluation, it check the sign and then do operation depending on what it is*/
            public static bool? ResolveEvaluation(string[] expression)
            {
                bool? result = null;

                string Operator = expression[1];

                switch(Operator)
                {
                    case ">":
                        result = Greater(expression[0], expression[2]);
                        break;
                    case "<":
                        result = Lesser(expression[0], expression[2]);
                        break;
                    case ">=":
                        result = GreaterOrEqual(expression[0], expression[2]);
                        break;
                    case "<=":
                        result = LesserOrEqual(expression[0], expression[2]);
                        break;
                    case "==":
                        result = Equal(expression[0], expression[2]);
                        break;
                    case "!=":
                        result = NotEqual(expression[0], expression[2]);
                        break;
                }

                return result;
            }
        }
    }
}
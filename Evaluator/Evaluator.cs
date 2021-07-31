using System;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Program.Variable;
using System.Collections.Generic;

namespace Bits_Script_Interpreter.Evaluator
{
    enum Type
    {
        Operator,
        Number
    }

    static class Interpreter_Evaluator
    {
        public static int Evaluate(string expression)
        {
            string[] evaluation = PreProcessEvaluation(ParseEvaluation(expression));

            int result = Interpreter_Evaluator_Core.ResolveEvaluation(evaluation);

            Debug.Log($"The resul of the evaluation is {result}", true);
            
            return result;
        }

        public static bool IsOperator(string code)
        {
            foreach (char c in code)
            {
                if(char.IsDigit(c)){return false;}
            }

            return true;
        }

        public static bool IsOperator(char code)
        {
            if(code == '*' || code == '/' || code == '+' || code == '-')    return true;

            return false;
        }

        public static bool IsPriotaryOperator(string code)
        {
            if(code == "*" || code == "/")
            {
                return true;
            }

            return false;
        }

        public static bool IsNumber(char c)
        {
            return char.IsDigit(c);
        }

        public static bool IsNumber(string c)
        {
            return int.TryParse(c, out int n);
        }

        public static bool CanBeEvaluated(string block)
        {
            foreach (char c in block)
            {
                if(!IsOperator(c) && !IsNumber(c) && c != ' ') return false;
            }

            return true;
        }

        static private class Interpreter_Evaluator_Core
        {
            public static int Add(string a, string b){ return System.Convert.ToInt32(a) + System.Convert.ToInt32(b); }
            public static int Sub(string a, string b){ return System.Convert.ToInt32(a) - System.Convert.ToInt32(b);}
            public static int Mul(string a, string b){ return System.Convert.ToInt32(a) * System.Convert.ToInt32(b);}
            public static int Div(string a, string b){ return System.Convert.ToInt32(a) / System.Convert.ToInt32(b);}

            public static int ResolveEvaluation(string[] evaluation)
            {
                int last = System.Convert.ToInt32(evaluation[0]);

                for(int i = 1; i < evaluation.Length; i++)
                {
                    string eval = evaluation[i];

                    if(IsOperator(eval))
                    {
                        switch(eval)
                        {
                            case "+":
                                 last = Add(last.ToString(), evaluation[i + 1]);
                                break;
                            case "-":
                                last = Sub(last.ToString(), evaluation[i + 1]);
                                break;
                            case "/":
                                last = Div(last.ToString(), evaluation[i + 1]);
                                break;
                            case "*":
                                last = Mul(last.ToString(), evaluation[i + 1]);
                                break;
                        }
                    }
                }

                return last;
            }
        }

        private static string[] ReplaceVariable(string[] parsedEvaluation)
        {
            for(int i = 0; i < parsedEvaluation.Length; i++)
            {
                string eval = parsedEvaluation[i];

                if(IsOperator(eval) || IsNumber(eval)) { continue; }

                parsedEvaluation[i] = Program_Variable.GetVariable(eval).value.ToString();
            }

            return parsedEvaluation;
        }

        private static string[] ParseEvaluation(string evaluation)
        {
            string[] splittedEvaluation = evaluation.Split(' ');

            return ReplaceVariable(splittedEvaluation);
        }

        private static string[] PreProcessEvaluation(string[] evaluation)
        {
            List<string> output = Interpreter_String.ArrayToList<string>(evaluation);

            for(int i = 0; i < output.Count; i++)
            {
                string current = output[i];

                if(IsOperator(current))
                {
                    if(IsPriotaryOperator(current))
                    {
                        int result = 0;

                        switch(current)
                        {
                            case "/":
                                result = Interpreter_Evaluator_Core.Div(output[i - 1], output[i + 1]);
                                output[i] = result.ToString();
                                output.RemoveAt(i + 1);
                                output.RemoveAt(i - 1);
                                break;
                            case "*":
                                result = Interpreter_Evaluator_Core.Mul(output[i - 1], output[i + 1]);
                                output[i] = result.ToString();
                                output.RemoveAt(i + 1);
                                output.RemoveAt(i - 1);
                                break;
                        }

                        Debug.Log(Interpreter_String.AssembleList<string, char>(output, 0, '\n'), true);
                        i--;
                    }
                }
            }

            return output.ToArray(); 
        }
    }
}
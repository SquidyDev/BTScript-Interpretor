using System;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Program.Variable;
using System.Collections.Generic;

/*
<summary>
    An evaluator to evaluate int expr.
    it doesn't support () actually. 
    It support variable.
</summary>
*/
namespace Bits_Script_Interpreter.Evaluator
{
    enum Type
    {
        Operator,
        Number
    }

    static class Interpreter_Evaluator
    {
        static char[] operators = 
        {
            '+',
            '-',
            '*',
            '/'
        }; 

        public static int Evaluate(string expression)
        {
            string[] evaluation = PreProcessEvaluation(ParseEvaluation(expression));

            int result = Interpreter_Evaluator_Core.ResolveEvaluation(evaluation);

            Debug.Log($"The resul of the evaluation is {result}", true);
            
            return result;
        }

        public static bool IsOperator(string code)
        {
            foreach(char c in code)
            {
                if(Interpreter_String.ArrayContains<char>(operators, c)) return true;
            }

            return false;
        }

        public static bool IsOperator(char code)
        {
            if(Interpreter_String.ArrayContains<char>(operators, code)) return true;

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

        public static bool IsVariable(string str)
        {
            return Program_Variable.Exist(str);
        }

        public static bool CanBeEvaluated(string nonSplittedBlock)
        {
            string[] splittedBlock = nonSplittedBlock.Split(' ');

            foreach(string str in splittedBlock)
            {
                if(!IsOperator(str) && !IsNumber(str) && !IsVariable(str)) return false;
            }

            Debug.Log($"Expression {nonSplittedBlock} can be evaluated !", true);

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
            List<string> output = new List<string>();

            Debug.Log(Interpreter_String.AssembleArray<string, string>(parsedEvaluation, 0, "--"), true);

            for(int i = 0; i < parsedEvaluation.Length; i++)
            {
                output.Add(parsedEvaluation[i]);

                if(IsOperator(output[i]) || IsNumber(output[i]))
                {
                    Debug.Log($"{output[i]} is a operator or a number !", true);
                    continue;
                }

                if(Program_Variable.Exist(output[i]))
                {
                    Debug.Log($"{output[i]} is a variable !", true);
                    if(Program_Variable.GetVariable(output[i], false) == null) 
                    {
                        Debug.Log($"variable was null", true);
                        continue;
                    }
                    else 
                    {
                        Debug.Log($"variable was not null", true);
                        output[i] = Program_Variable.GetVariable(output[i], false).value.ToString();
                    }
                }
            }

            return output.ToArray();
        }

        private static string[] ParseEvaluation(string evaluation)
        {
            string cleanedEvaluation = evaluation.Trim('\r', '\n');
            string[] splittedEvaluation = cleanedEvaluation.Split(' ');

            Debug.Log($"EXPRESSION WITH REPLACED VARIABLE : {Interpreter_String.AssembleArray<string, char>(ReplaceVariable(splittedEvaluation), 0, ' ')}", true);

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
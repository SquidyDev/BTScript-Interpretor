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
        /*list of double operator*/
        static char[] operators = 
        {
            '+',
            '-',
            '*',
            '/'
        }; 

        /*Evaluate an expression and return a double depending on it's result*/
        public static double Evaluate(string expression)
        {
            string[] evaluation = PreProcessEvaluation(ParseEvaluation(expression));

            double result = Interpreter_Evaluator_Core.ResolveEvaluation(evaluation);

            Debug.Log($"The resul of the evaluation is {result}", true);
            
            return result;
        }

        /*Check if a string is an operator*/
        public static bool IsOperator(string code)
        {
            foreach(char c in code)
            {
                if(Interpreter_String.ArrayContains<char>(operators, c)) return true;
            }

            return false;
        }

        /*Check if a char is an operator*/
        public static bool IsOperator(char code)
        {
            if(Interpreter_String.ArrayContains<char>(operators, code)) return true;

            return false;
        }

        /*Check if a number is a priotary operator (* ; /)*/
        public static bool IsPriotaryOperator(string code)
        {
            if(code == "*" || code == "/")
            {
                return true;
            }

            return false;
        }

        /*Check if a char is an operator*/
        public static bool IsNumber(char c)
        {
            return char.IsDigit(c);
        }

        /*Check if a string is an operator*/
        public static bool IsNumber(string c)
        {
            return double.TryParse(c, out double n);
        }

        /*Check if the variable exist*/
        public static bool IsVariable(string str)
        {
            return Program_Variable.Exist(str);
        }

        /*Check if an expression can be evaluated as a double*/
        public static bool CanBeEvaluated(string nonSplittedBlock)
        {
            try{
                double r = Evaluate(nonSplittedBlock);

                if(r == -7.86975) return false;

                Debug.Log($"The expression : {nonSplittedBlock} can be evaluated so it is a double. (result is : {Evaluate(nonSplittedBlock)})", true);

                return true;
            }catch
            {
                Debug.Log($"The expression {nonSplittedBlock} cannot be evaluated so it is not a double.", true);
                return false;
            }
        }

        static private class Interpreter_Evaluator_Core
        {
            /*Following functions are for arithmetic operation*/
            public static double Add(string a, string b){ return double.Parse(a) + double.Parse(b); }
            public static double Sub(string a, string b){ return double.Parse(a) - double.Parse(b);}
            public static double Mul(string a, string b){ return double.Parse(a) * double.Parse(b);}
            public static double Div(string a, string b){ return double.Parse(a) / double.Parse(b);}

            /*Execute arithmetic operation depending on the sign encouter*/
            public static double ResolveEvaluation(string[] evaluation)
            {
                double last = double.Parse(evaluation[0]);

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
                    }else if(double.TryParse(eval, out double d))
                    {
                        return -7.86975;
                    }
                }

                return last;
            }
        }

        /*Replace the variable by their value*/
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

        /*Parse the evaluation*/
        private static string[] ParseEvaluation(string evaluation)
        {
            string cleanedEvaluation = evaluation.Trim('\r', '\n');
            string[] splittedEvaluation = cleanedEvaluation.Split(' ');

            Debug.Log($"EXPRESSION WITH REPLACED VARIABLE : {Interpreter_String.AssembleArray<string, char>(ReplaceVariable(splittedEvaluation), 0, ' ')}", true);

            return ReplaceVariable(splittedEvaluation);
        }

        /*Use to Preprocess the evaluation (do priority sign)*/
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
                        double result = 0;

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
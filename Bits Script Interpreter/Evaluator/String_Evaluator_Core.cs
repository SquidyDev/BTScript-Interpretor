using System.Collections.Generic;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Program.Variable;
using Bits_Script_Interpreter.Interpreter.String;

namespace Bits_Script_Interpreter.Evaluator.String.Core 
{
    static class Interpreter_String_Evaluator_Core
    {
        public static string[] ParseEvaluation(string evaluation)
        {
            List<string> output = new List<string>();

            string actual = "";

            for(int i = 0; i < evaluation.Length; i++)
            {
                char current = evaluation[i];

                actual += current;

                if(current == '"')
                {
                    for(int j = i + 1; j < evaluation.Length; j++)
                    {
                        if(evaluation[j] == '"')
                        {
                            actual += evaluation[j];
                            i = j;
                           j = evaluation.Length;
                        }else
                        {
                            actual += evaluation[j];
                        }
                    }
                }

                if(current == ' ')
                {
                    if(Program_Variable.Exist(actual.Trim()))
                    {
                        actual = Program_Variable.GetVariable(actual.Trim(), true).value.ToString();
                    }

                    output.Add(actual);
                    actual = "";
                }
            }
            output.Add(actual);
            Debug.Log($"End Loop, current is {actual}", true);

            for(int i = 0; i < output.Count; i++) 
            {
                output[i] = Interpreter_String.RemoveQuoteMarks(output[i]);
            }

            return output.ToArray();
        }

        public static bool ContainString(string evaluation)
        {
            string[] splitted = evaluation.Split(' ');

            for(int i = 0; i < splitted.Length; i++)
            {
                string current = splitted[i].Trim();

                if(Program_Variable.Exist(current))
                {
                    if(Program_Variable.GetVariable(current, true).type == "string")
                    {
                        return true;
                    }
                }else
                {
                    for(int j = 0; j < current.Length; j++)
                    {
                        char currentChar = current[j];

                        if(currentChar == '"') return true;
                    }
                }
            }

            return false;
        }
    }
}